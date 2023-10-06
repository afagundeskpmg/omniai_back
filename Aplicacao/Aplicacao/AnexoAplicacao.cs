using Aplicacao.Interface;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Xml;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using SixLabors.Fonts.Tables.AdvancedTypographic;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Dominio.Validacoes;
using System.IO.Pipes;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Aplicacao
{
    public class AnexoAplicacao : BaseAplicacao<Anexo>, IAnexoAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;
        private readonly IProcessamentoAplicacao _processamentoAplicacao;

        public AnexoAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao, IProcessamentoAplicacao processamentoAplicacao) : base(repositorio, configuracao)
        {
            _repositorio = repositorio;
            _processamentoAplicacao = processamentoAplicacao;
        }

        public Resultado<Anexo> Cadastrar(int anexoTipoId, int anexoArquivoTipoId, object obj, Stream stream, string arquivoNome, Usuario usuarioLogado, Dictionary<string, object>? parametrosAdicionais , bool pularValidacoes)
        {
            var resultado = new Resultado<Anexo>();

            var anexoArquivoTipo = _repositorio.AnexoArquivoTipo.SelecionarPorId(anexoArquivoTipoId);
            var resultadoValidar = ValidarArquivoPostado(stream, arquivoNome, anexoArquivoTipoId,pularValidacoes);

            if (!resultadoValidar.Sucesso)
                resultado.Mensagem = resultadoValidar.Mensagem;
            else
            {
                if (obj == null)
                    resultado.Mensagem = "Objeto não encontrado";
                else
                {
                    AtribuirObjeto(ref obj, anexoTipoId);

                    if ((anexoTipoId == (int)AnexoTipoEnum.DocumentoPagina || anexoTipoId == (int)AnexoTipoEnum.Documento) && !usuarioLogado.Claims.Any(p => p.ClaimType == "AlterarTodosAnexos") && !usuarioLogado.Ambientes.Any(p => p.Id == ((ProjetoAnexo)obj).Projeto.AmbienteId))
                        resultado.Mensagem = "Objeto não encontrado";
                    else
                    {
                        var anexo = new Anexo(anexoTipoId, resultadoValidar.Retorno, anexoArquivoTipo, arquivoNome, usuarioLogado.Id);

                        var resultadoAtribuir = new Resultado<object>();

                        AtribuirAnexo(anexo, obj, ref resultadoAtribuir, stream, usuarioLogado, parametrosAdicionais);

                        if (!resultadoAtribuir.Sucesso)
                            resultado.Mensagem = resultadoAtribuir.Mensagem;
                        else
                        {
                            try
                            {
                                anexo.CaminhoArquivoBlobStorage = Path.Combine((string)resultadoAtribuir.Retorno, anexo.NomeArquivoAlterado);
                                anexo.BlobContainerName = SelecionarParametro(ParametroAmbienteEnum.BlobContainerNameAnexos);

                                var tags = GerarTagsParaBlobPorAnexo(anexo);

                                var conn = SelecionarParametro(ParametroAmbienteEnum.StorageConnection);
                                var resultadoBlob = EnviarArquivoBlobStorage(conn, anexo.BlobContainerName, anexo.CaminhoArquivoBlobStorage, stream, tags);

                                if (!resultadoBlob.Sucesso)
                                    resultado.Mensagem = "Houve falha ao salvar o arquivo enviado";
                                else
                                {
                                    if (anexo.Id != 0)
                                        Atualizar(anexo);
                                    else
                                        Inserir(anexo);

                                    resultado.Retorno = anexo;
                                    resultado.Sucesso = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                resultado.Sucesso = false;
                                resultado.Mensagem = "Ocorreu uma falha ao tentar salvar o arquivo enviado.";
                                Log.LogArquivo.Log(ex, null);
                            }
                        }
                    }
                }
            }

            return resultado;
        }
        private Resultado<object> EnviarArquivoBlobStorage(string conn, string container, string caminhoCompletoArquivo, Stream stream, Dictionary<string, string> tags)
        {
            var resultado = new Resultado<object>();

            try
            {
                BlobContainerClient containerClient = new BlobContainerClient(conn, container);
                containerClient.CreateIfNotExists();
                BlobClient blobClient = containerClient.GetBlobClient(caminhoCompletoArquivo);
                BlobUploadOptions options = new BlobUploadOptions() { HttpHeaders = new BlobHttpHeaders { ContentType = "text/xml" } };
                options.Tags = tags;

                stream.Position = 0;
                var result = blobClient.Upload(stream, options);

                if (!result.GetRawResponse().IsError)
                    resultado.Sucesso = true;
                else
                {
                    resultado.Mensagem = "Ocorreu uma falha na solicitação";
                    Log.LogArquivo.Log(new Exception(string.Format("Erro ao enviar o arquivo \"{0}\": {1}", caminhoCompletoArquivo, result.GetRawResponse().ToString())), null);
                }

            }
            catch (Exception ex)
            {
                Log.LogArquivo.Log(ex, string.Format("Erro ao enviar o arquivo \"{0}\"", caminhoCompletoArquivo));
                resultado.Mensagem = "Ocorreu uma falha na solicitação";
                resultado.MensagemInterna = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return resultado;
        }
        private Dictionary<string, string> GerarTagsParaBlobPorAnexo(Anexo anexo)
        {
            return new Dictionary<string, string>
                            {
                                { "NomeArquivoAlterado", Dominio.Validacoes.Nome.RemoverAcentos(anexo.NomeArquivoAlterado) },
                                { "ArquivoFormato", Dominio.Validacoes.Nome.RemoverAcentos(anexo.ArquivoFormato?.Nome) },
                                { "AnexoArquivoTipo", Dominio.Validacoes.Nome.RemoverAcentos(anexo.AnexoArquivoTipo?.Nome) },
                                { "CriadoEm", anexo.CriadoEm?.ToUniversalTime().ToString() }
                            };
        }
        public Resultado<CloudBlob> SelecionarCloudBlob(string accountName, string accountKey, string containerName, string caminhoArquivo)
        {
            var resultado = new Resultado<CloudBlob>();

            try
            {
                CloudStorageAccount account = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), true);
                var blobClient = account.CreateCloudBlobClient();
                var container = blobClient.GetContainerReference(containerName);
                var blob = container.GetBlockBlobReference(caminhoArquivo);
                var sasToken = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                {
                    Permissions = SharedAccessBlobPermissions.Read,
                    SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(10),//assuming the blob can be downloaded in 10 miinutes
                }, new SharedAccessBlobHeaders()
                {
                    ContentDisposition = "attachment; filename=" + Path.GetFileName(caminhoArquivo)
                });

                resultado.Sucesso = true;
                resultado.Retorno = blob;
            }
            catch (Exception ex)
            {
                resultado.Sucesso = false;
                resultado.Mensagem = "Erro ao tentar recuperar o arquivo do blob: " + ex.ToString();
            }

            return resultado;
        }
        private void AtribuirAnexo(Anexo anexo, object obj, ref Resultado<object> resultado, Stream stream, Usuario usuarioLogado, Dictionary<string, object> parametrosAdicionais)
        {
            switch ((AnexoTipoEnum)anexo.AnexoTipoId)
            {
                case AnexoTipoEnum.Documento:
                    AtribuirAnexoDocumento(anexo, (ProjetoAnexo)obj, ref resultado, usuarioLogado, parametrosAdicionais);
                    break;
                case AnexoTipoEnum.DocumentoPagina:
                    AtribuirAnexoDocumentoPagina(anexo, (ProjetoAnexo)obj, ref resultado, parametrosAdicionais);
                    break;
                default:
                    resultado.Mensagem = "Tipo de anexo não configurado";
                    break;
            }
        }
        public Resultado<Anexo> SelecionarParaAlteracao(int? anexoId, Usuario? usuarioLogado, string? objetoId)
        {
            var resultado = new Resultado<Anexo>();
            var resultadoValidacaoAnexo = new Resultado<Anexo>();
            var anexo = _repositorio.Anexo.SelecionarPorId(anexoId);

            if (usuarioLogado == null)
                resultadoValidacaoAnexo.Mensagem = "Você não tem permissão para acessar anexos";
            else if (anexo == null || anexo.Excluido)
                resultadoValidacaoAnexo.Mensagem = "Arquivo não encontrado";
            else if (usuarioLogado != null && !usuarioLogado.Claims.Any(x => x.ClaimType == "VisualizarTodosAnexos" || x.ClaimType == "VisualizarMeusAnexos" || x.ClaimType == "VisualizarTodosAnexosAssociados"))
                resultadoValidacaoAnexo.Mensagem = "Você não tem permissão para acessar anexos";
            else
            {
                int objetoIdInt = 0;
                int.TryParse(objetoId, out objetoIdInt);

                switch (anexo.AnexoTipoId)
                {
                    case (int)AnexoTipoEnum.Documento:
                        resultadoValidacaoAnexo = ValidarSelecaoAnexoTipoDocumento(anexo, usuarioLogado);
                        break;
                    case (int)AnexoTipoEnum.DocumentoPagina:
                        resultadoValidacaoAnexo = ValidarSelecaoAnexoTipoDocumentoPagina(anexo, usuarioLogado);
                        break;
                    default:
                        resultadoValidacaoAnexo.Mensagem = "Tipo de anexo não configurado";
                        resultadoValidacaoAnexo.Sucesso = false;
                        break;
                }
            }

            if (!resultadoValidacaoAnexo.Sucesso)
                resultado.Mensagem = resultadoValidacaoAnexo.Mensagem;
            else
            {
                resultado.Sucesso = true;
                resultado.Retorno = resultadoValidacaoAnexo.Retorno;
            }

            return resultado;
        }
        private Resultado<Anexo> ValidarSelecaoAnexoTipoDocumento(Anexo anexo, Usuario usuarioLogado)
        {
            Resultado<Anexo> resultado = new Resultado<Anexo>();

            if (anexo.Projetos == null || !anexo.Projetos.Any())
                resultado.Mensagem = "Ocorreu um erro na validação do arquivo";
            else if (
                     (usuarioLogado.Claims.Any(c => c.ClaimType == "VisualizarMeusAnexos") && anexo.CriadoPorId == usuarioLogado.Id) ||
                     (usuarioLogado.Claims.Any(c => c.ClaimType == "VisualizarTodosAnexosAssociados") && anexo.Projetos.Any(x => usuarioLogado.PertenceAoAmbiente(x.Projeto.AmbienteId))) ||
                     usuarioLogado.Claims.Any(c => c.ClaimType == "VisualizarTodosAnexos"))
            {
                resultado.Retorno = anexo;
                resultado.Sucesso = true;
            }
            else
                resultado.Mensagem = "Você não tem permissão para acessar este arquivo";

            return resultado;
        }

        private Resultado<Anexo> ValidarSelecaoAnexoTipoDocumentoPagina(Anexo anexo, Usuario usuarioLogado)
        {
            Resultado<Anexo> resultado = new Resultado<Anexo>();

            var anexoPaginaPai = anexo.AnexoPaginaPai;

            if (anexoPaginaPai == null || anexoPaginaPai.AnexoPai == null || anexoPaginaPai.AnexoPai.Projetos == null || !anexoPaginaPai.AnexoPai.Projetos.Any())
                resultado.Mensagem = "Ocorreu um erro na validação do arquivo";
            else if (
                     (usuarioLogado.Claims.Any(c => c.ClaimType == "VisualizarMeusAnexos") && anexo.CriadoPorId == usuarioLogado.Id) ||
                     (usuarioLogado.Claims.Any(c => c.ClaimType == "VisualizarTodosAnexosAssociados") && anexoPaginaPai.AnexoPai.Projetos.Any(x => usuarioLogado.PertenceAoAmbiente(x.Projeto.AmbienteId))) ||
                     usuarioLogado.Claims.Any(c => c.ClaimType == "VisualizarTodosAnexos"))
            {
                resultado.Retorno = anexo;
                resultado.Sucesso = true;
            }
            else
                resultado.Mensagem = "Você não tem permissão para acessar este arquivo";

            return resultado;
        }

        private void AtribuirObjeto(ref object obj, int anexoTipoId)
        {

            if (obj != null)
            {
                switch (anexoTipoId)
                {
                    case (int)AnexoTipoEnum.Documento:
                        obj = (ProjetoAnexo)obj;
                        break;
                    case (int)AnexoTipoEnum.DocumentoPagina:
                        obj = (ProjetoAnexo)obj;
                        break;
                    default:
                        break;
                }
            }
        }
        private void AtribuirAnexoDocumento(Anexo anexo, ProjetoAnexo projetoAnexo, ref Resultado<object> resultado, Usuario usuarioLogado, Dictionary<string, object> parametrosAdicionais)
        {
            var processamentoTipo = _repositorio.ProcessamentoTipo.SelecionarPorId((int)ProcessamentoTipoEnum.Anexo);
            var processamento = (Processamento)projetoAnexo.ProcessamentoAnexo;

            projetoAnexo.Anexo = anexo;
            resultado.Retorno = DeterminarPastaAnexo(projetoAnexo.ProcessamentoIndexer, AnexoTipoEnum.Documento);
            _processamentoAplicacao.AtribuirQueueProcessamento(ref processamento, SelecionarParametro(ParametroAmbienteEnum.StorageConnection), processamentoTipo.QueueNome, JsonConvert.SerializeObject(new { Id = processamento.Id, projetoAnexo.Projeto.Ambiente.Cliente.Pais.CultureInfo }), 0);

            resultado.Sucesso = true;
        }
        private void AtribuirAnexoDocumentoPagina(Anexo anexo, ProjetoAnexo projetoAnexo, ref Resultado<object> resultado, Dictionary<string, object> parametrosAdicionais)
        {
            var anexoPagina = new AnexoPagina();
            if (projetoAnexo.Anexo.Paginas == null)
                projetoAnexo.Anexo.Paginas = new List<AnexoPagina>();

            int ordem = Convert.ToInt32(parametrosAdicionais["Ordem"]);
            projetoAnexo.Anexo.Paginas.Add(new AnexoPagina(ordem) { Anexo = anexo,AnexoId = anexo.Id, Ordem = ordem, AnexoPaiId = projetoAnexo.AnexoId });

            resultado.Sucesso = true;
            resultado.Retorno = DeterminarPastaAnexo(projetoAnexo, AnexoTipoEnum.DocumentoPagina);
        }


        private Resultado<ArquivoFormato> ValidarArquivoPostado(Stream stream, string arquivoNome, int anexoArquivoTipoId, bool pularValidacoes)
        {
            var resultado = new Resultado<ArquivoFormato>();
            var anexoArquivoTipo = _repositorio.AnexoArquivoTipo.SelecionarPorId(anexoArquivoTipoId);
            if (!pularValidacoes)
            {
                if (stream == null || stream.Length == 0)
                    resultado.Mensagem = "Arquivo inválido";
                else if (anexoArquivoTipo == null)
                    resultado.Mensagem = "Tipo de arquivo inválido";
                else if (!anexoArquivoTipo.FormatosPermitidos.Any(f => f.ArquivoFormato.Nome.ToUpper() == Path.GetExtension(arquivoNome).ToUpper()))
                {
                    resultado.Mensagem = "Os formatos permitidos são:";
                    anexoArquivoTipo.FormatosPermitidos.ToList().ForEach(f => resultado.Mensagem += f.ArquivoFormato.Nome + " ");
                }
                else
                {
                    var arquivoFormato = anexoArquivoTipo?.FormatosPermitidos?.FirstOrDefault(f => f.ArquivoFormato.Nome.ToUpper() == Path.GetExtension(arquivoNome).ToUpper()).ArquivoFormato;

                    if (ConverterBytesParaMegabytes(stream.Length) > arquivoFormato.TamanhoMaximoMb)
                        resultado.Mensagem = string.Concat("O arquivo deve ser menor que: ", arquivoFormato.TamanhoMaximoMb.ToString(), "mb");
                    else
                    {
                        var resultadoValidacaoAssinatura = ValidarAssinaturaArquivo(stream, arquivoFormato);
                        if (!resultadoValidacaoAssinatura.Sucesso)
                            resultado.Mensagem = resultadoValidacaoAssinatura.Mensagem;
                        else
                        {
                            if (arquivoNome.Contains("\\"))
                            {
                                var nameArray = arquivoNome.Split('\\');
                                arquivoNome = nameArray[nameArray.Count() - 1].ToString();
                            }

                            if (Path.GetExtension(arquivoNome).ToUpper() == ".XML" && !VerificaSeXMLContemDTD(stream))
                                resultado.Mensagem = "XML invlálido DTD";
                            else
                            {
                                resultado.Retorno = anexoArquivoTipo.FormatosPermitidos.First(f => f.ArquivoFormato.Nome.ToUpper() == Path.GetExtension(arquivoNome).ToUpper()).ArquivoFormato;
                                resultado.Sucesso = true;
                            }
                        }
                    }
                }
            }
            else
            {
                resultado.Retorno = anexoArquivoTipo.FormatosPermitidos.First(f => f.ArquivoFormato.Nome.ToUpper() == Path.GetExtension(arquivoNome).ToUpper()).ArquivoFormato;
                resultado.Sucesso = true;
            }

            return resultado;
        }

        //Validação necessaria para impedir ataques XXE
        private bool VerificaSeXMLContemDTD(Stream stream)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Prohibit;
                settings.MaxCharactersFromEntities = 6000;

                using (XmlReader reader = XmlReader.Create(stream, settings))
                {
                    XmlDocument xmlDoc = new XmlDocument();

                    //Se o arquivo for invalido vai dar erro.
                    xmlDoc.Load(reader);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.LogArquivo.Log(ex, null);
                return false;
            }
        }

        public Resultado<object> ValidarAssinaturaArquivo(Stream stream, ArquivoFormato arquivoFormato)
        {
            stream.Position = 0;

            var resultado = new Resultado<object>();

            var assinaturas = new List<ArquivoFormatoAssinatura>();
            var tiposTextoContem = new string[] { "text", "txt" };

            if (!tiposTextoContem.Any(t => arquivoFormato.MimeType.Contains(t)))
            {
                //se o tipo NÂO for de texto, vai considerar as assinaturas do formato enviado, pois arquivos de texto não terão assinaturas registradas
                assinaturas.AddRange(arquivoFormato.ArquivoFormatoAssinaturas);
            }
            else
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    //se o tipo FOR de texto, verifica se tem algum caractere de controle
                    using (var reader = new StreamReader(memoryStream))
                    {
                        var caracteresControlePermitidos = new char[] { (char)10, (char)11, (char)13, (char)9, (char)0 };
                        var caracteresControleEncontrados = new List<string>();

                        string linha;
                        int numeroLinha = 1;

                        while ((linha = reader.ReadLine()) != null)
                        {
                            var charsNaoPermitidosEncontrados = linha.Where(c => char.IsControl(c) && !caracteresControlePermitidos.Any(cp => cp == c));

                            if (charsNaoPermitidosEncontrados.Any())
                                caracteresControleEncontrados.Add($"line {numeroLinha}: {string.Join(", ", charsNaoPermitidosEncontrados)}");
                            numeroLinha++;
                        }

                        if (caracteresControleEncontrados.Any())
                            resultado.Mensagem = string.Format("{0} caracteres de controle foram encontrados no arquivo", caracteresControleEncontrados.Count) + " " + string.Join("; ", caracteresControleEncontrados) + ".";
                        else //se não encontrar nenhum caractere de controle inválido, adiciona todas as assinaturas para validação
                            assinaturas.AddRange(_repositorio.ArquivoFormato.SelecionarTodos().ToList().SelectMany(af => af.ArquivoFormatoAssinaturas));
                    }
                }
            }

            if (string.IsNullOrEmpty(resultado.Mensagem) && assinaturas.Any())
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    using (var reader = new BinaryReader(memoryStream))
                    {
                        var byteArray = new List<byte[]>();
                        foreach (var assinatura in assinaturas)
                            byteArray.Add(assinatura.ArquivoFormatoAssinaturaBytes.Select(b => b.Byte).ToArray());

                        var headerBytes = reader.ReadBytes(byteArray.Max(m => m.Length));
                        var assinaturaEncontrada = byteArray.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));

                        //será valido APENAS se a assinatura FOR encontrada E não for um arquivo de texto
                        //pois se for um arquivo de texto e alguma assinatura for encontrada, então trata-se de um arquivo de outro formato enviado como texto, portanto, INVALIDO

                        if (tiposTextoContem.Any(t => arquivoFormato.MimeType.Contains(t)) && assinaturaEncontrada)
                            resultado.Mensagem = "Uma assinatura foi encontrada para o arquivo, porém o formato identificado não é válido";
                        else if (tiposTextoContem.Any(t => arquivoFormato.MimeType.Contains(t)) && !assinaturaEncontrada)
                            resultado.Sucesso = true;
                        else if (!assinaturaEncontrada && !tiposTextoContem.Any(t => arquivoFormato.MimeType.Contains(t)))
                            resultado.Mensagem = "Arquivo inválido";
                        else if (assinaturaEncontrada)
                            resultado.Sucesso = true;
                    }
                }
            }

            return resultado;
        }

        public double ConverterBytesParaMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public Resultado<object> SalvarArquivoAnexoStream(Stream input, string caminhoCompletoArquivo)
        {
            var resultado = new Resultado<object>();
            try
            {
                //input.Position = 0;
                //CriarDiretorio(Path.GetDirectoryName(caminhoCompletoArquivo));
                //using (Stream file = File.Create(caminhoCompletoArquivo))
                //{
                //    byte[] buffer = new byte[8 * 1024];
                //    int len;
                //    while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                //    {
                //        file.Write(buffer, 0, len);
                //    }
                //}

                resultado.Sucesso = true;
            }
            catch (Exception ex)
            {
                resultado.Mensagem = "Função indisponível no momento";
                Log.LogArquivo.Log(ex, "Erro ao salvar stream " + caminhoCompletoArquivo);
            }

            return resultado;
        }
    }
}

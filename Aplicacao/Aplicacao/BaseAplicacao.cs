using Aplicacao.Util;
using Aplicacao.Util.OpenAI;
using Azure;
using Azure.AI.OpenAI;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Log;

namespace Aplicacao
{
    public class BaseAplicacao<T> : IBaseAplicacao<T> where T : class
    {
        private readonly IUnitOfWorkRepositorio _repositorio;
        private readonly IConfiguration _configuracao;
        public BaseAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao)
        {
            _repositorio = repositorio;
            _configuracao = configuracao;
        }

        #region Dados
        public void Atualizar(T obj)
        {
            ExecutarMetodo("Atualizar", new object[] { obj }, new Type[] { obj.GetType() });
        }

        public void Recarregar(T obj)
        {
            ExecutarMetodo("Recarregar", new object[] { obj }, new Type[] { obj.GetType() });
        }

        public void Deletar(T obj)
        {
            ExecutarMetodo("Deletar", new object[] { obj }, new Type[] { obj.GetType() });
        }

        public void DeletarTodos(ICollection<T> obj)
        {
            ExecutarMetodo("DeletarTodos", new object[] { obj }, new Type[] { obj.GetType() });
        }

        public void Inserir(T obj)
        {
            ExecutarMetodo("Inserir", new object[] { obj }, new Type[] { obj.GetType() });
        }

        public void InserirTodos(ICollection<T> obj)
        {
            ExecutarMetodo("InserirTodos", new object[] { obj }, new Type[] { obj.GetType() });
        }

        public T SelecionarFirstOrDefault(Expression<Func<T, bool>> filter = null)
        {
            return (T)ExecutarMetodo("SelecionarFirstOrDefault", new object[] { filter }, new Type[] { typeof(Expression<Func<T, bool>>) });
        }

        public T SelecionarPorId(int id)
        {
            return (T)ExecutarMetodo("SelecionarPorId", new object[] { id }, new Type[] { id.GetType() });
        }

        public T SelecionarPorId(string id)
        {
            return (T)ExecutarMetodo("SelecionarPorId", new object[] { id }, new Type[] { id.GetType() });
        }

        public IEnumerable<T> SelecionarTodos(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool noTracking = false)
        {
            return (IEnumerable<T>)ExecutarMetodo("SelecionarTodos", new object[] { filter, orderBy, includeProperties, noTracking }, new Type[] { typeof(Expression<Func<T, bool>>), typeof(Func<IQueryable<T>, IOrderedQueryable<T>>), typeof(string), typeof(bool) });
        }

        public object ExecutarMetodo(string methodName, object[] parameters, Type[] parametersTypes)
        {
            PropertyInfo p = _repositorio.GetType().GetProperty(typeof(T).Name);
            Type type = p.PropertyType;
            if (methodName == null)
                methodName = SelecionarMetodoAtual();
            MethodInfo method = null;
            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (parameters != null)
                    method = interfaceType.GetMethod(methodName, parametersTypes);
                else
                    method = interfaceType.GetMethod(methodName, new Type[] { });
                if (method != null)
                {
                    return method.Invoke(_repositorio.GetType().GetProperty(typeof(T).Name).GetValue(_repositorio, null), parameters);
                }
            }
            throw new Exception("Metodo não reconhecido");
        }

        private string SelecionarMetodoAtual()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(2);
            return sf.GetMethod().Name;
        }


        #endregion

        #region Util
        public Task<RestResponse> FazerRequisicao(Method metodo, string url, string? corpo, string? contentType)
        {
            var options = new RestClientOptions(url)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                MaxTimeout = 999999999
            };

            var client = new RestClient(options);

            var requisicao = new RestRequest();
            requisicao.Timeout = 999999999;
            requisicao.Method = metodo;

            if (!string.IsNullOrEmpty(corpo))
                requisicao.AddBody(corpo, contentType);

            if (!string.IsNullOrEmpty(contentType))
                requisicao.AddHeader("Content-Type", contentType);

            return client.ExecuteAsync(requisicao);
        }
        public Task<RestResponse> FazerRequisicao(Method metodo, string url, List<Dominio.Entidades.Parameter> parametros, string? contentType)
        {
            var options = new RestClientOptions(url)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                MaxTimeout = 999999999
            };

            var client = new RestClient(options);

            var requisicao = new RestRequest();
            requisicao.Timeout = 999999999;
            requisicao.Method = metodo;

            if (!string.IsNullOrEmpty(contentType))
                requisicao.AddHeader("Content-Type", contentType);

            foreach (var parametro in parametros)
                requisicao.AddParameter(parametro.Nome, parametro.Valor);

            return client.ExecuteAsync(requisicao);

        }
        public string LerArquivoTexto(string caminhoCompletoArquivo)
        {
            string retorno;
            IdentificarEncoding(caminhoCompletoArquivo, out retorno);
            return retorno;
        }
        public Resultado<decimal> ConverterParaDecimal(string valorString, string mascara)
        {
            var resultado = new Resultado<decimal>();

            try
            {
                if (!decimal.TryParse(valorString, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal valorDecimal))
                    throw new Exception();
                else
                {
                    string valorFormatado = valorDecimal.ToString(mascara);
                    resultado.Sucesso = true;
                    resultado.Retorno = valorDecimal;
                }
            }
            catch
            {
                resultado.Mensagem = string.Format("Não foi possível converter o valor {0}", valorString);
            }

            return resultado;
        }
        public Resultado<DateTime> ConverterParaDateTime(string valorString, string mascara)
        {
            var resultado = new Resultado<DateTime>();

            try
            {
                if (!DateTime.TryParseExact(valorString, mascara, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data))
                    throw new Exception();
                else
                {
                    resultado.Sucesso = true;
                    resultado.Retorno = data;
                }
            }
            catch
            {
                resultado.Mensagem = string.Format("Não foi possível converter a data {0}", valorString);
            }

            return resultado;
        }
        public int ExecutarQuery(string query)
        {
            return _repositorio.ExecutarQuery(query);
        }
        public List<T> ExecutarViewSQL<T>(string query)
        {
            return _repositorio.ExecutarViewSQL<T>(query);
        }
        public string SelecionarParametro(ParametroAmbienteEnum parametroAmbienteEnum)
        {
            return _configuracao[parametroAmbienteEnum.ToString()];
        }
        public string DeterminarPastaAnexo(object obj, AnexoTipoEnum anexoTipo)
        {
            string retorno = string.Empty;
            switch (anexoTipo)
            {
                case AnexoTipoEnum.Documento:
                    return @Path.Combine("Ambiente", ((ProcessamentoIndexer)obj).Projeto.AmbienteId.ToString(), "Anexos", "Projetos", ((ProcessamentoIndexer)obj).ProjetoId.ToString(), "Processamentos", ((ProcessamentoIndexer)obj).Id, "Documentos");
                case AnexoTipoEnum.DocumentoPagina:
                    var processamentoIndexer = ((ProjetoAnexo)obj).ProcessamentoIndexer;
                    return @Path.Combine("Ambiente", ((ProjetoAnexo)obj).Projeto.AmbienteId.ToString(), "Anexos", "Projetos", ((ProjetoAnexo)obj).ProjetoId.ToString(), "Processamentos", processamentoIndexer.Id, "DocumentosPaginas", ((ProjetoAnexo)obj).AnexoId.ToString());
                default:
                    throw new InvalidOperationException("Tipo de anexo não cadastrado");
            }

            return retorno;
        }
        public Encoding IdentificarEncoding(string filename, out string text, int taster = 1000)
        {
            byte[] b = File.ReadAllBytes(filename);

            if (b.Length >= 4 && b[0] == 0x00 && b[1] == 0x00 && b[2] == 0xFE && b[3] == 0xFF) { text = Encoding.GetEncoding("utf-32BE").GetString(b, 4, b.Length - 4); return Encoding.GetEncoding("utf-32BE"); }  // UTF-32, big-endian
            else if (b.Length >= 4 && b[0] == 0xFF && b[1] == 0xFE && b[2] == 0x00 && b[3] == 0x00) { text = Encoding.UTF32.GetString(b, 4, b.Length - 4); return Encoding.UTF32; }    // UTF-32, little-endian
            else if (b.Length >= 2 && b[0] == 0xFE && b[1] == 0xFF) { text = Encoding.BigEndianUnicode.GetString(b, 2, b.Length - 2); return Encoding.BigEndianUnicode; }     // UTF-16, big-endian
            else if (b.Length >= 2 && b[0] == 0xFF && b[1] == 0xFE) { text = Encoding.Unicode.GetString(b, 2, b.Length - 2); return Encoding.Unicode; }              // UTF-16, little-endian
            else if (b.Length >= 3 && b[0] == 0xEF && b[1] == 0xBB && b[2] == 0xBF) { text = Encoding.UTF8.GetString(b, 3, b.Length - 3); return Encoding.UTF8; } // UTF-8
            else if (b.Length >= 3 && b[0] == 0x2b && b[1] == 0x2f && b[2] == 0x76) { text = Encoding.UTF7.GetString(b, 3, b.Length - 3); return Encoding.UTF7; } // UTF-7

            if (taster == 0 || taster > b.Length)
                taster = b.Length;

            int i = 0;
            bool utf8 = false;
            while (i < taster - 4)
            {
                if (b[i] <= 0x7F) { i += 1; continue; }     // If all characters are below 0x80, then it is valid UTF8, but UTF8 is not 'required' (and therefore the text is more desirable to be treated as the default codepage of the computer). Hence, there's no "utf8 = true;" code unlike the next three checks.
                if (b[i] >= 0xC2 && b[i] <= 0xDF && b[i + 1] >= 0x80 && b[i + 1] < 0xC0) { i += 2; utf8 = true; continue; }
                if (b[i] >= 0xE0 && b[i] <= 0xF0 && b[i + 1] >= 0x80 && b[i + 1] < 0xC0 && b[i + 2] >= 0x80 && b[i + 2] < 0xC0) { i += 3; utf8 = true; continue; }
                if (b[i] >= 0xF0 && b[i] <= 0xF4 && b[i + 1] >= 0x80 && b[i + 1] < 0xC0 && b[i + 2] >= 0x80 && b[i + 2] < 0xC0 && b[i + 3] >= 0x80 && b[i + 3] < 0xC0) { i += 4; utf8 = true; continue; }
                utf8 = false; break;
            }
            if (utf8 == true)
            {
                text = Encoding.UTF8.GetString(b);
                return Encoding.UTF8;
            }

            double threshold = 0.1;
            int count = 0;
            for (int n = 0; n < taster; n += 2) if (b[n] == 0) count++;
            if (((double)count) / taster > threshold) { text = Encoding.BigEndianUnicode.GetString(b); return Encoding.BigEndianUnicode; }
            count = 0;
            for (int n = 1; n < taster; n += 2) if (b[n] == 0) count++;
            if (((double)count) / taster > threshold) { text = Encoding.Unicode.GetString(b); return Encoding.Unicode; } // (little-endian)

            for (int n = 0; n < taster - 9; n++)
            {
                if (
                    ((b[n + 0] == 'c' || b[n + 0] == 'C') && (b[n + 1] == 'h' || b[n + 1] == 'H') && (b[n + 2] == 'a' || b[n + 2] == 'A') && (b[n + 3] == 'r' || b[n + 3] == 'R') && (b[n + 4] == 's' || b[n + 4] == 'S') && (b[n + 5] == 'e' || b[n + 5] == 'E') && (b[n + 6] == 't' || b[n + 6] == 'T') && (b[n + 7] == '=')) ||
                    ((b[n + 0] == 'e' || b[n + 0] == 'E') && (b[n + 1] == 'n' || b[n + 1] == 'N') && (b[n + 2] == 'c' || b[n + 2] == 'C') && (b[n + 3] == 'o' || b[n + 3] == 'O') && (b[n + 4] == 'd' || b[n + 4] == 'D') && (b[n + 5] == 'i' || b[n + 5] == 'I') && (b[n + 6] == 'n' || b[n + 6] == 'N') && (b[n + 7] == 'g' || b[n + 7] == 'G') && (b[n + 8] == '='))
                    )
                {
                    if (b[n + 0] == 'c' || b[n + 0] == 'C') n += 8; else n += 9;
                    if (b[n] == '"' || b[n] == '\'') n++;
                    int oldn = n;
                    while (n < taster && (b[n] == '_' || b[n] == '-' || (b[n] >= '0' && b[n] <= '9') || (b[n] >= 'a' && b[n] <= 'z') || (b[n] >= 'A' && b[n] <= 'Z')))
                    { n++; }
                    byte[] nb = new byte[n - oldn];
                    Array.Copy(b, oldn, nb, 0, n - oldn);
                    try
                    {
                        string internalEnc = Encoding.ASCII.GetString(nb);
                        text = Encoding.GetEncoding(internalEnc).GetString(b);
                        return Encoding.GetEncoding(internalEnc);
                    }
                    catch { break; }
                }
            }

            text = Encoding.Default.GetString(b);
            return Encoding.Default;
        }
        public string LimparTextoDeCaracteresEspeciais(string texto)
        {
            string pattern = "[^a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ0-9 ]";
            return Regex.Replace(texto, pattern, "");
        }
        public string LerArquivoEmbutido(string trechoNomeArquivo, Assembly assembly)
        {
            return AssemblyUtil.LerArquivoEmbutido(trechoNomeArquivo, assembly);
        }
        public Azure.Response<Azure.Storage.Queues.Models.SendReceipt> AdicionarItemQueue(string connection, string queueName, string mensagem, int maxRetries)
        {
            QueueClientOptions options = new QueueClientOptions();
            options.Retry.MaxRetries = maxRetries;
            options.Retry.NetworkTimeout = TimeSpan.FromHours(24);

            var queueClient = new QueueClient(connection, queueName, options);
            queueClient.CreateIfNotExists();

            return queueClient.SendMessage(Base64Codificar(mensagem));
        }
        public string Base64Codificar(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        public string Base64Decodificar(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public bool ConfirmarBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;
            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception) { }
            return false;
        }
        public string GerarSenhaAleatoriaB2C(int tamanhoDaSenha)
        {
            List<string> listacaracteresPermitidos = new List<string>() { "abcdefghijkmnopqrstuvwxyz", "ABCDEFGHJKLMNOPQRSTUVWXYZ", "0123456789", "!@$?_-#", };
            int i = 0;
            int count = 0;
            char[] chars = new char[tamanhoDaSenha];
            Random rd = new Random();

            do
            {
                var caracteresPermitidos = listacaracteresPermitidos[i];
                chars[count] = caracteresPermitidos[rd.Next(0, caracteresPermitidos.Length)];

                i++;
                i = i <= 3 ? i : 0;
                count++;

            } while (count < tamanhoDaSenha);

            return new string(chars);
        }
        public MemoryStream GerarMemoryStreamString(string str)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream, Encoding.UTF8);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public string ComporURL(string action, string route, List<Dominio.Entidades.Parameter> parameters)
        {
            var parametros = new List<string>();
            if (parameters != null && parameters.Any())
                parameters.ForEach(p => parametros.Add(p.Nome + "=" + p.Valor));
            var url = string.Format("{0}{1}/{2}?{3}", SelecionarParametro(ParametroAmbienteEnum.URLPadrao), route, action, string.Join("&", parametros));
            return url;
        }
        #endregion

        #region Blob
        public Resultado<object> EnviarArquivoBlobStorage(string conn, string container, string caminhoCompletoArquivo, Stream stream, Dictionary<string, string> tags)
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
        public Resultado<object> ExcluirArquivoBlobStorage(string conn, string blobContainerName, string caminhoArquivoBlobStorage)
        {
            var resultado = new Resultado<object>();

            try
            {
                BlobContainerClient containerClient = new BlobContainerClient(conn, blobContainerName);
                BlobClient blobClient = containerClient.GetBlobClient(caminhoArquivoBlobStorage);
                BlobUploadOptions options = new BlobUploadOptions() { HttpHeaders = new BlobHttpHeaders { ContentType = "text/xml" } };

                var result = blobClient.DeleteIfExists();

                if (result.GetRawResponse().IsError)
                    resultado.Mensagem = "Ocorreu uma falha na solicitação";
                else
                    resultado.Sucesso = true;

            }
            catch (Exception ex)
            {
                resultado.Mensagem = "Ocorreu uma falha na solicitação";
                resultado.MensagemInterna = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return resultado;
        }
        public Dictionary<string, string> GerarTagsParaBlobPorAnexo(Anexo anexo)
        {
            return new Dictionary<string, string>
                            {
                                { "NomeArquivoAlterado", Dominio.Validacoes.Nome.RemoverAcentos(anexo.NomeArquivoAlterado) },
                                { "ArquivoFormato", Dominio.Validacoes.Nome.RemoverAcentos(anexo.ArquivoFormato?.Nome) },
                                { "AnexoArquivoTipo", Dominio.Validacoes.Nome.RemoverAcentos(anexo.AnexoArquivoTipo?.Nome) },
                                { "CriadoEm", anexo.CriadoEm?.ToUniversalTime().ToString() }
                            };
        }
        #endregion

        #region Function / CS / OpenAI       
        public Resultado<string> ComporPrompParaConsultaChatGPT(ref PerguntaResposta perguntaResposta)
        {
            var resultado = new Resultado<string>();
            try
            {
                var query = ComporQueryParaCognitiveSearch(perguntaResposta.CaminhoBlob);
                var resultadoConteudo = SelecionarConteudoCognitiveSearch(query, perguntaResposta.Projeto.Ambiente.CongnitiveSearchSize, perguntaResposta.Projeto.Ambiente.CognitiveSearchIndexName);

                var content = "";
                var prompt = "Responda à pergunta com a maior sinceridade possível usando o texto fornecido e, se a resposta não estiver contida no texto abaixo, diga 'informação não encontrada' \n\n Context:\n {0}\n\n Q:{1} \n A:";

                foreach (var indexContent in resultadoConteudo.Retorno)
                {
                    content += indexContent.Content;
                    var anexo = _repositorio.Anexo.SelecionarFirstOrDefault(x => x.NomeArquivoAlterado == indexContent.FileName);

                    if (anexo != null && anexo.AnexoPaginaPai != null)
                        perguntaResposta.PaginasRelacionadas.Add(anexo.AnexoPaginaPai);
                }

                perguntaResposta.Prompt = string.Format(prompt, content, perguntaResposta.Pergunta);
                resultado.Sucesso = true;
            }
            catch (Exception ex)
            {
                resultado.Mensagem = "Ocorreu um erro ao tentar compor o prompt da pergunta" + ex.ToString();
                Log.LogArquivo.Log(ex, resultado.Mensagem);
            }

            return resultado;

        }
        public int CalcularNumeroTokens(string texto)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(texto);

            // Get the root of the syntax tree
            SyntaxNode root = syntaxTree.GetRoot();

            // Count the tokens in the syntax tree
            return root.DescendantTokens().Count();
        }
        public Resultado<OpenAIResponse> FazerPerguntaChatGPT(string instrucoes, float Temperature, int MaxTokens)
        {
            var resultado = new Resultado<OpenAIResponse>() { Retorno = new OpenAIResponse() };

            try
            {
                if (string.IsNullOrEmpty(instrucoes))
                    resultado.Mensagem = "Uma instrução é requerida";
                else
                {
                    string deploymentName = SelecionarParametro(ParametroAmbienteEnum.AzureOpenAIDeploymentName);
                    string endpoint = SelecionarParametro(ParametroAmbienteEnum.AzureOpenAIEndPoint);
                    var credential = SelecionarParametro(ParametroAmbienteEnum.AzureKeyCredentialOpenAI);

                    LogArquivo.Log(null, "AzureOpenAIDeploymentName " + deploymentName);
                    LogArquivo.Log(null, "AzureOpenAIEndPoint " + endpoint);
                    LogArquivo.Log(null, "AzureKeyCredentialOpenAI " + credential);

                    var client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(credential));

                    CompletionsOptions completionsOptions = new CompletionsOptions()
                    {
                        Temperature = Temperature,
                        MaxTokens = MaxTokens,
                    };

                    completionsOptions.StopSequences.Add("\n");
                    completionsOptions.StopSequences.Add("Q:");
                    completionsOptions.StopSequences.Add("B:");

                    completionsOptions.Prompts.Add(instrucoes);

                    Response<Completions> completionsResponse = client.GetCompletions(deploymentName, completionsOptions);


                    if (completionsResponse.GetRawResponse().IsError)
                        resultado.Mensagem = "Ocorreu um erro";
                    else
                    {
                        //string response = completionsResponse.Value.Choices[0].Text.Trim();
                        resultado.Retorno.Query = completionsResponse.Value.Choices[0].Text.TrimStart().ToString();
                        //resultado.Retorno.Query = LimparTextoDeCaracteresEspeciais(response);
                        resultado.Retorno.Json = JsonConvert.SerializeObject(completionsResponse.Value);
                        resultado.Sucesso = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogArquivo.Log(ex, resultado.Mensagem);
                resultado.Mensagem = "Ocorreu um erro ao tentar falar com chat gpt";
            }

            return resultado;
        }
        public string ComporQueryParaCognitiveSearch(string caminhoBlobStorage)
        {
            var parametrosConsulta = string.Concat(SelecionarParametro(ParametroAmbienteEnum.AzureBlobURI), SelecionarParametro(ParametroAmbienteEnum.BlobContainerNameAnexos), caminhoBlobStorage);
            var query = string.Format(SelecionarParametro(ParametroAmbienteEnum.AzureCognitiveSearchQuery), parametrosConsulta);

            return query;
        }
        public Resultado<List<IndexContent>> SelecionarConteudoCognitiveSearch(string query, int size, string indexName)
        {
            var resultado = new Resultado<List<IndexContent>>() { Retorno = new List<IndexContent>() };
            try
            {
                var resultadoSearchClient = SelecionarSearchClient(indexName);
                if (!resultadoSearchClient.Sucesso)
                    resultado.AtribuirMensagemErro(resultadoSearchClient);
                else
                {
                    var select = new List<string> { "id", "content", "metadata_storage_name", "metadata_storage_path" };
                    SearchOptions options = new SearchOptions() { Size = size, Filter = query, IncludeTotalCount = true };
                    select.ForEach(s => options.Select.Add(s));

                    SearchResults<DocumentModel> results = resultadoSearchClient.Retorno.Search<DocumentModel>("", options);

                    if (results.TotalCount == null || results.TotalCount == 0)
                        resultado.Mensagem = "Desculpe, mas não encontramos resultados para a sua consulta na index. Por favor, verifique os critérios da sua pesquisa e tente novamente.";
                    else
                    {
                        foreach (SearchResult<DocumentModel> result in results.GetResults())
                        {
                            resultado.Retorno.Add(new IndexContent() { FileName = result.Document.metadata_storage_name, Content = result.Document.content });
                        }
                        resultado.Sucesso = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogArquivo.Log(ex, resultado.Mensagem);
                resultado.Mensagem = "Ocorreu um erro ao tentar selecionar conteudo do cognitive search";
            }

            return resultado;
        }
        public void AtribuirQueueProcessamento(ref Processamento processamento, string connection, string queueName, string mensagem, int maxRetries)
        {
            try
            {
                var result = AdicionarItemQueue(connection, queueName, mensagem, maxRetries);
                var response = result.GetRawResponse();

                if (response.IsError)
                {
                    processamento.ProcessamentoStatusId = (int)ProcessamentoStatusEnum.ProcessadoComErro;
                    processamento.InicioProcessamentoEm = DateTime.Now;
                    processamento.FimProcessamentoEm = DateTime.Now;
                    processamento.ProcessamentoLogs.Add(new ProcessamentoLog(response.Status.ToString()));
                }
                else
                {
                    processamento.QueueMessageId = result.Value.MessageId;
                    processamento.QueueExpiraEm = result.Value.ExpirationTime.UtcDateTime;
                }
            }
            catch (Exception ex)
            {
                processamento.ProcessamentoStatusId = (int)ProcessamentoStatusEnum.ProcessadoComErro;
                processamento.InicioProcessamentoEm = DateTime.Now;
                processamento.FimProcessamentoEm = DateTime.Now;
                processamento.ProcessamentoLogs.Add(new ProcessamentoLog(ex.ToString()));
            }
        }
        private Resultado<SearchClient> SelecionarSearchClient(string indexName)
        {
            var resultado = new Resultado<SearchClient>();
            try
            {
                string apiKey = SelecionarParametro(ParametroAmbienteEnum.AzureKeyCredential);

                // Create a SearchIndexClient to send create/delete index commands
                Uri serviceEndpoint = new Uri(SelecionarParametro(ParametroAmbienteEnum.AzureIndexerURI));
                AzureKeyCredential credential = new AzureKeyCredential(apiKey);
                SearchIndexClient adminClient = new SearchIndexClient(serviceEndpoint, credential);

                // Create a SearchClient to load and query documents
                resultado.Retorno = new SearchClient(serviceEndpoint, indexName, credential);
                resultado.Sucesso = true;
            }
            catch (Exception ex)
            {
                resultado.AtribuirMensagemErro(ex, "Ocorreu um erro ao tentar conectar-se ao cognitive search");
            }

            return resultado;
        }
        public Resultado<SearchIndexerClient> SelecionarSearchIndexerClient()
        {
            var resultado = new Resultado<SearchIndexerClient>();
            try
            {
                resultado.Retorno = new SearchIndexerClient(new Uri(SelecionarParametro(ParametroAmbienteEnum.AzureIndexerURI)), new AzureKeyCredential(SelecionarParametro(ParametroAmbienteEnum.AzureIndexerKeyCredential)));
                resultado.Sucesso = true;
            }
            catch (Exception ex)
            {
                resultado.AtribuirMensagemErro(ex, "Ocorreu um erro ao tentar conectar-se ao cognitive search");
            }

            return resultado;

        }
        #endregion
    }
}

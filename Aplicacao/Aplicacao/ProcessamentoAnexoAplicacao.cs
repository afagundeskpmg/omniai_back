using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf;
using Newtonsoft.Json;

namespace Aplicacao
{
    public class ProcessamentoAnexoAplicacao : BaseAplicacao<ProcessamentoAnexo>, IProcessamentoAnexoAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;
        private readonly IAnexoAplicacao _anexoAplicacao;
        public ProcessamentoAnexoAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao, IAnexoAplicacao anexoAplicacao) : base(repositorio, configuracao)
        {
            _repositorio = repositorio;
            _anexoAplicacao = anexoAplicacao;
        }
        public void Processar(ref ProcessamentoAnexo processamento)
        {
            try
            {
                #region Atualizar Status Processamento
                processamento.AtualizarStatusDeProcessamento(ProcessamentoStatusEnum.EmProcessamento, null);
                Atualizar(processamento);
                _repositorio.SaveChanges();
                #endregion

                SepararArquivosPorPagina(ref processamento);

                if (processamento.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.EmProcessamento)
                    processamento.FinalizarProcessamentoComStatus(ProcessamentoStatusEnum.ProcessadoComSucesso, null, null);
            }
            catch (Exception ex)
            {
                var mensagem = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                processamento.FinalizarProcessamentoComStatus(ProcessamentoStatusEnum.ProcessadoComErro, null, new ProcessamentoLog(string.Concat("Ocorreu um erro no processameto Ex:", ex.Message)));
                Log.LogArquivo.Log(ex, processamento.Id.ToString());
            }

            Atualizar(processamento);                      
        }
        private void SepararArquivosPorPagina(ref ProcessamentoAnexo processamento)
        {
            #region Iniciar Processamento
            processamento.ProcessamentoStatusId = (int)ProcessamentoStatusEnum.EmProcessamento;
            processamento.InicioProcessamentoEm = DateTime.Now;
            Atualizar(processamento);
            _repositorio.SaveChanges();
            #endregion

            var resultado = new Resultado<object>();
            var resultadoSelecaoArquivo = _anexoAplicacao.SelecionarParaAlteracao(processamento.ProjetoAnexo.AnexoId, processamento.CriadoPor, null);
            var resultadoQuebraArquivo = new Resultado<object>();

            if (!resultadoSelecaoArquivo.Sucesso)
                resultado.AtribuirMensagemErro(resultadoSelecaoArquivo);
            else
            {
                var projetoAnexo = processamento.ProjetoAnexo;
                var arquivoFormatoId = processamento.ProjetoAnexo.Anexo.ArquivoFormatoId;
                var accName = SelecionarParametro(ParametroAmbienteEnum.StorageAccountName);
                var accKey = _anexoAplicacao.SelecionarParametro(ParametroAmbienteEnum.StorageAccountKey);
                var resultadoBlob = _anexoAplicacao.SelecionarCloudBlob(accName, accKey, resultadoSelecaoArquivo.Retorno.BlobContainerName, resultadoSelecaoArquivo.Retorno.CaminhoArquivoBlobStorage);

                using (Stream ms = new MemoryStream())
                {
                    resultadoBlob.Retorno.DownloadToStreamAsync(ms).Wait();

                    ms.Position = 0;

                    switch (arquivoFormatoId)
                    {
                        case (int)ArquivoFormatoEnum.PDF:
                            resultadoQuebraArquivo = QuebrarPDFPorPaginas(ms, projetoAnexo, processamento.CriadoPor);
                            break;
                        default:
                            break;
                    }
                }

                if (!resultadoQuebraArquivo.Sucesso)
                    resultado.AtribuirMensagemErro(resultadoQuebraArquivo);
                else
                    resultado.Sucesso = true;
            }

            if (!resultado.Sucesso)
                processamento.FinalizarProcessamentoComStatus(ProcessamentoStatusEnum.ProcessadoComErro, null, new ProcessamentoLog(resultadoSelecaoArquivo.Mensagem));
        }
        private Resultado<object> QuebrarPDFPorPaginas(Stream stream, ProjetoAnexo projetoAnexo, Usuario usuario)
        {
            var resultado = new Resultado<object>();

            try
            {
                var anexo = projetoAnexo.Anexo;

                using (PdfDocument originalDocument = PdfReader.Open(stream, PdfDocumentOpenMode.Import))
                {
                    for (int pageIndex = 0; pageIndex < originalDocument.PageCount; pageIndex++)
                    {
                        PdfDocument newDocument = new PdfDocument();
                        newDocument.AddPage(originalDocument.Pages[pageIndex]);
                        using (MemoryStream pageStream = new MemoryStream())
                        {
                            newDocument.Save(pageStream);
                            pageStream.Seek(0, SeekOrigin.Begin);
                            var fileNameArray = anexo.NomeArquivoOriginal.Split(".");
                            string filaName = string.Concat(fileNameArray[0], "_pag_", pageIndex, ".", fileNameArray[1]);

                            var resultadoCadastroPagina = _anexoAplicacao.Cadastrar((int)AnexoTipoEnum.DocumentoPagina, anexo.AnexoArquivoTipoId, projetoAnexo, pageStream, filaName, usuario, new Dictionary<string, object>() { { "Ordem", pageIndex } },true);

                            if (!resultadoCadastroPagina.Sucesso)
                            {
                                resultado.Mensagem = string.Format("Ocorreu um erro ao tentar quebrar o arquivo {0} por página.", anexo.NomeArquivoOriginal);
                                resultado.MensagemInterna = resultadoCadastroPagina.Mensagem;
                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(resultado.Mensagem))
                            resultado.Sucesso = true;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.AtribuirMensagemErro(ex, "Ocorreu um erro ao tentar quebrar o arquivo em páginas");
            }
            return resultado;
        }
        public Resultado<DatatableRetorno<object>> SelecionarPorFiltro(string? Id, int? ambienteId, string? projetoId, DateTime? dataDe, DateTime? dataAte, bool excluido, int? start, int? length)
        {
            return _repositorio.ProcessamentoIndexer.SelecionarPorFiltro(Id, ambienteId, projetoId, dataDe, dataAte, excluido, start, length);
        }
    }
}

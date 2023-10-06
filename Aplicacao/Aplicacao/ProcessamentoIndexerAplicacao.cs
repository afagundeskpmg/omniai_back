using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf;
using System.IO;
using System.ComponentModel;
using Azure;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Reflection.Metadata;
using Microsoft.IdentityModel.Tokens;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Newtonsoft.Json;

namespace Aplicacao
{
    public class ProcessamentoIndexerAplicacao : BaseAplicacao<ProcessamentoIndexer>, IProcessamentoIndexerAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;
        private readonly IAnexoAplicacao _anexoAplicacao;
        private readonly IProcessamentoAplicacao _processamentoAplicacao;
        public ProcessamentoIndexerAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao, IProcessamentoAplicacao processamentoAplicacao) : base(repositorio, configuracao)
        {
            _repositorio = repositorio;
            _processamentoAplicacao = processamentoAplicacao;
        }

        public void Processar(ref ProcessamentoIndexer processamento)
        {
            try
            {
                #region Atualizar Status Processamento
                processamento.AtualizarStatusDeProcessamento(ProcessamentoStatusEnum.EmProcessamento, null);
                processamento.InicioIndexacao = DateTime.Now;
                Atualizar(processamento);
                _repositorio.SaveChanges();
                #endregion

                IndexarArquivos(ref processamento);
            }
            catch (Exception ex)
            {
                var mensagem = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                processamento.FinalizarProcessamentoComStatus(ProcessamentoStatusEnum.ProcessadoComErro, null, new ProcessamentoLog(string.Concat("Ocorreu um erro no processameto Ex:", ex.Message)));
                Log.LogArquivo.Log(ex, processamento.Id.ToString());
            }
     
            processamento.DeletarIndexer = true;
            Atualizar(processamento);

            if (processamento.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.EmProcessamento)
                AdicionarItemQueue(SelecionarParametro(ParametroAmbienteEnum.StorageConnection), "deletarindexerdatasource", JsonConvert.SerializeObject(new { Id = processamento.Id, processamento.Projeto.Ambiente.Cliente.Pais.CultureInfo }), 1);
        }
        private void IndexarArquivos(ref ProcessamentoIndexer processamento)
        {
            var resultado = new Resultado<object>();
            var resultadoSearchIndexerClient = SelecionarSearchIndexerClient();

            if (!resultadoSearchIndexerClient.Sucesso)
                resultado.AtribuirMensagemErro(resultadoSearchIndexerClient);
            else
            {
                var indexerClient = resultadoSearchIndexerClient.Retorno;
                var resultadoCriacaoDataSource = CriarDataSource(ref indexerClient, ref processamento);

                if (!resultadoCriacaoDataSource.Sucesso)
                    resultado.AtribuirMensagemErro(resultadoCriacaoDataSource);
                else
                {
                    var resultadoCriacaoIndexer = CriarIndexer(ref indexerClient, ref processamento);
                    if (!resultadoCriacaoIndexer.Sucesso)
                        resultado.AtribuirMensagemErro(resultadoCriacaoIndexer);
                    else
                        resultado.Sucesso = true;
                }
            }

            if (!resultado.Sucesso)
                processamento.FinalizarProcessamentoComStatus(ProcessamentoStatusEnum.ProcessadoComErro, null, new ProcessamentoLog(resultado.Mensagem));

        }
        private Resultado<object> CriarDataSource(ref SearchIndexerClient indexerClient, ref ProcessamentoIndexer processamento)
        {
            #region Parametros
            var resultado = new Resultado<object>();
            var dataSourceName = processamento.DataSourceName;
            var containerName = SelecionarParametro(ParametroAmbienteEnum.BlobContainerNameAnexos);
            var caminho = processamento.BlobFolder;
            #endregion

            try
            {
                string blobStorageConnectionstring = SelecionarParametro(ParametroAmbienteEnum.StorageConnection);

                SearchIndexerDataSourceConnection dataSource = new SearchIndexerDataSourceConnection(name: dataSourceName, type: SearchIndexerDataSourceType.AzureBlob, connectionString: blobStorageConnectionstring, container: new SearchIndexerDataContainer(containerName)
                {
                    Query = caminho
                });

                indexerClient.CreateOrUpdateDataSourceConnection(dataSource);

                resultado.Sucesso = true;
            }
            catch (Exception ex)
            {
                resultado.AtribuirMensagemErro(ex, "Ocorreu um erro ao tentar gerar um novo datasource");
            }

            return resultado;
        }
        private Resultado<object> CriarIndexer(ref SearchIndexerClient indexerClient, ref ProcessamentoIndexer processamento)
        {
            #region Parametros
            var resultado = new Resultado<object>();
            var indexerName = processamento.IndexerName;
            var indexName = processamento.Projeto.Ambiente.CognitiveSearchIndexName;
            var dataSourceName = processamento.DataSourceName;
            var descricao = string.Format("Indexer criado para atender ao processamentoId: {0} referênte ao ambiente: {1} na data: {2} pelo usuário: {3}", processamento.Id, processamento.Projeto.Ambiente.Cliente.Nome, DateTime.Now.ToString(), processamento.CriadoPor.UserName);
            var skillSetName = processamento.Projeto.Ambiente.CognitiveSearchSkillSetName;
            var parameters = new IndexingParameters()
            {
                IndexingParametersConfiguration = new IndexingParametersConfiguration()
                {
                    ImageAction = BlobIndexerImageAction.GenerateNormalizedImages,
                    AllowSkillsetToReadFileData = true,
                    PdfTextRotationAlgorithm = BlobIndexerPdfTextRotationAlgorithm.DetectAngles,
                    DataToExtract = BlobIndexerDataToExtract.ContentAndMetadata
                }
            };
            #endregion

            try
            {
                SearchIndexer indexer = new SearchIndexer(indexerName, dataSourceName, indexName);
                indexer.Description = descricao;
                indexer.SkillsetName = skillSetName;
                indexer.Parameters = parameters;

                var jsonString = LerArquivoEmbutido("field_mappings.json", typeof(IUnitOfWorkAplicacao).Assembly);
                var jsonData = JsonConvert.DeserializeObject<Dictionary<object, object>>(jsonString);

                var fieldMappings = ((Newtonsoft.Json.Linq.JObject)(JsonConvert.DeserializeObject<Dictionary<object, object>>(jsonString)["FieldMappings"]));
                var outputFieldMappings = ((Newtonsoft.Json.Linq.JObject)(JsonConvert.DeserializeObject<Dictionary<object, object>>(jsonString)["OutputFieldMappings"]));

                // Adicione os mapeamentos de campo aos FieldMappings e OutputFieldMappings.
                foreach (var kvp in fieldMappings)
                {
                    indexer.FieldMappings.Add(new FieldMapping(kvp.Key) { TargetFieldName = kvp.Value.ToString() });
                }

                // Adicione os mapeamentos do OutputFieldMappings ao indexer
                foreach (var kvp in outputFieldMappings)
                {
                    indexer.OutputFieldMappings.Add(new FieldMapping(kvp.Key) { TargetFieldName = kvp.Value.ToString() });
                }

                indexerClient.CreateOrUpdateIndexer(indexer);

                resultado.Sucesso = true;
            }
            catch (Exception ex)
            {
                resultado.MensagemInterna = ex.Message;
            }

            return resultado;
        }
        public Resultado<object> DeletarIndexerDataSource(ref ProcessamentoIndexer processamento)
        {
            var resultado = new Resultado<object>();
            var resultadoSelecaoSearchIndexer = SelecionarSearchIndexerClient();

            if (!resultadoSelecaoSearchIndexer.Sucesso)
                resultado.AtribuirMensagemErro(resultadoSelecaoSearchIndexer);
            else
            {
                if (!resultadoSelecaoSearchIndexer.Sucesso)
                    resultado.AtribuirMensagemErro(resultadoSelecaoSearchIndexer);
                else
                {
                    var listaIndexers = resultadoSelecaoSearchIndexer.Retorno.GetIndexerNames();

                    if (!listaIndexers.GetRawResponse().IsError)
                    {
                        var indexerName = processamento.IndexerName;
                        var dataSourceName = processamento.DataSourceName;

                        if (listaIndexers.Value != null && listaIndexers.Value.Any(x => x.Equals(indexerName)))
                        {
                            var responseStatus = resultadoSelecaoSearchIndexer.Retorno.GetIndexerStatus(indexerName);

                            while (!responseStatus.GetRawResponse().IsError && (responseStatus.Value.LastResult == null || responseStatus.Value.LastResult.Status.ToString() == "InProgress"))
                            {
                                Thread.Sleep(5000);
                                responseStatus = resultadoSelecaoSearchIndexer.Retorno.GetIndexerStatus(indexerName);
                            }

                            if (responseStatus.Value.LastResult != null)
                                processamento.Dados = JsonConvert.SerializeObject(responseStatus.Value);

                            if (responseStatus.GetRawResponse().IsError)
                                resultado.Mensagem = "Ocorreu um erro no processo de deleção da index.";
                            else if (responseStatus.Value != null && responseStatus.Value.LastResult.Status.ToString() != "Success")
                            {
                                resultado.Mensagem = string.Format("Não foi possível deletar a index {0}, pois encontra-se no status: {1}.", indexerName, responseStatus.Value.LastResult.Status.ToString());

                                if (responseStatus.Value.LastResult.Status.ToString().Contains("Failure"))
                                    processamento.FinalizarProcessamentoComStatus(ProcessamentoStatusEnum.ProcessadoComErro, null, null);
                            }
                            else
                            {
                                var response = resultadoSelecaoSearchIndexer.Retorno.DeleteIndexer(indexerName);

                                if (response.IsError)
                                {
                                    resultado.Mensagem = string.Format("Não foi possível deletar a index {0}, pois ocorreu um erro.", indexerName, responseStatus.Value.LastResult.Status.ToString());
                                    processamento.FinalizarProcessamentoComStatus(ProcessamentoStatusEnum.ProcessadoComErro, null, null);
                                }
                                else
                                {
                                    response = resultadoSelecaoSearchIndexer.Retorno.DeleteDataSourceConnection(dataSourceName);

                                    if (response.IsError)
                                        resultado.Mensagem = string.Format("Ocorreu um erro ao tentar deletar o datasource: {0}.", dataSourceName);
                                }
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(resultado.Mensagem))
                processamento.AtribuirLog(new ProcessamentoLog(resultado.Mensagem));
            else
            {
                var processamentoStatus = processamento.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.ProcessadoComErro ? ProcessamentoStatusEnum.ProcessadoComErro : ProcessamentoStatusEnum.ProcessadoComSucesso;
                processamento.FinalizarProcessamentoComStatus(processamentoStatus, null, null);
                processamento.DeletarIndexer = false;
                resultado.Sucesso = true;
            }

            processamento.FimIndexacao = DateTime.Now;
            Atualizar(processamento);

            return resultado;
        }
        public Resultado<DatatableRetorno<object>> SelecionarPorFiltro(string? Id, int? ambienteId, string? projetoId, DateTime? dataDe, DateTime? dataAte, bool excluido, int? start, int? length)
        {
            return _repositorio.ProcessamentoIndexer.SelecionarPorFiltro(Id, ambienteId, projetoId, dataDe, dataAte, excluido, start, length);
        }
        public Resultado<ProcessamentoIndexer> CadastrarPorProjeto(Projeto projeto, Usuario usuarioLogado)
        {
            var resultado = new Resultado<ProcessamentoIndexer>();
            resultado.Retorno = projeto.ProcessamentosIndexers.FirstOrDefault(x => x.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.ProcessamentoSolicitado);

            if (resultado.Retorno == null)
                resultado.Retorno = new ProcessamentoIndexer(usuarioLogado.Id, ProcessamentoStatusEnum.ProcessamentoSolicitado, projeto);

            if (string.IsNullOrEmpty(resultado.Retorno.BlobFolder))
            {
                var caminhoBlob = @Path.Combine("Ambiente", resultado.Retorno.Projeto.AmbienteId.ToString(), "Anexos", "Projetos", resultado.Retorno.ProjetoId.ToString(), "Processamentos", resultado.Retorno.Id, "DocumentosPaginas/").Replace("\\", "/");
                resultado.Retorno.BlobFolder = caminhoBlob;

                _repositorio.Processamento.Inserir(resultado.Retorno);
                _repositorio.SaveChanges();
            }

            resultado.Sucesso = true;

            return resultado;

        }
    }
}

using Aplicacao.Interface;
using Dados.Interface;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Aplicacao
{
    public class ProcessamentoNerAplicacao : BaseAplicacao<ProcessamentoNer>, IProcessamentoNerAplicacao
    {
        private readonly IUnitOfWorkRepositorio _repositorio;
        public ProcessamentoNerAplicacao(IUnitOfWorkRepositorio repositorio, IConfiguration configuracao) : base(repositorio, configuracao)
        {
            _repositorio = repositorio;
        }
        public void Processar(ref ProcessamentoNer processamento)
        {
            try
            {
                #region Atualizar Status Processamento
                processamento.AtualizarStatusDeProcessamento(ProcessamentoStatusEnum.EmProcessamento, null);
                Atualizar(processamento);
                _repositorio.SaveChanges();
                #endregion

                AtribuirProcessamentosPergunta(ref processamento);

                if (processamento.ProcessamentosPerguntaGeradas != null)
                    AtribuirAFilaQueue(ref processamento);

            }
            catch (Exception ex)
            {
                var mensagem = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                processamento.FinalizarProcessamentoComStatus(ProcessamentoStatusEnum.ProcessadoComErro, null, new ProcessamentoLog(string.Concat("Ocorreu um erro no processameto Ex:", ex.Message)));
                Log.LogArquivo.Log(ex, processamento.Id.ToString());
            }

            Atualizar(processamento);

        }
        private void AtribuirProcessamentosPergunta(ref ProcessamentoNer processamento)
        {
            var projetosAnexos = processamento.Projeto.Anexos.Where(x => x.ProcessamentoIndexerId != null && x.ProcessamentoIndexer.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.ProcessadoComSucesso);

            foreach (var projetoAnexo in projetosAnexos)
            {
                foreach (var entidade in projetoAnexo.DocumentoTipo.Entidades)
                {
                    processamento.ProcessamentosPerguntaGeradas.Add(new ProcessamentoPergunta(processamento.CriadoPorId, ProcessamentoStatusEnum.ProcessamentoSolicitado, projetoAnexo, entidade));
                }
            }

        }
        public void AtribuirAFilaQueue(ref ProcessamentoNer processamento)
        {

            foreach (var processamentoPergunta in processamento.ProcessamentosPerguntaGeradas)
            {
                var processamentoTemp = (Processamento)processamento;
                var processamentoTipo = _repositorio.ProcessamentoTipo.SelecionarPorId((int)ProcessamentoTipoEnum.Pergunta);
                AtribuirQueueProcessamento(ref processamentoTemp, SelecionarParametro(ParametroAmbienteEnum.StorageConnection), processamentoTipo.QueueNome, JsonConvert.SerializeObject(new { Id = processamentoPergunta.Id, processamento.Projeto.Ambiente.Cliente.Pais.CultureInfo }), 0);

            }
        }
        public Resultado<DatatableRetorno<object>> SelecionarPorFiltro(string? Id, int? ambienteId, string? projetoId, DateTime? dataDe, DateTime? dataAte, bool excluido, int? start, int? length)
        {
            return _repositorio.ProcessamentoIndexer.SelecionarPorFiltro(Id, ambienteId, projetoId, dataDe, dataAte, excluido, start, length);
        }
    }

}

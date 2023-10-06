using Dominio.Entidades;

namespace Aplicacao.Interface
{
    public interface IProcessamentoNerAplicacao : IBaseAplicacao<ProcessamentoNer>
    {
        void Processar(ref ProcessamentoNer processamento);
        public void AtribuirAFilaQueue(ref ProcessamentoNer processamento);
        Resultado<DatatableRetorno<object>> SelecionarPorFiltro(string? Id, int? ambienteId, string? projetoId, DateTime? dataDe, DateTime? dataAte, bool excluido, int? start, int? length);
    }
}

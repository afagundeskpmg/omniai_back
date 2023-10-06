using Dominio.Entidades;

namespace Aplicacao.Interface
{
    public interface IProcessamentoAnexoAplicacao: IBaseAplicacao<ProcessamentoAnexo>
    {
        void Processar(ref ProcessamentoAnexo processamento);
        Resultado<DatatableRetorno<object>> SelecionarPorFiltro(string? Id, int? ambienteId, string? projetoId, DateTime? dataDe, DateTime? dataAte, bool excluido, int? start, int? length);
    }
}

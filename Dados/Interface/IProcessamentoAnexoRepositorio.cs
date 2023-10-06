using Dominio.Entidades;

namespace Dados.Interface
{
    public interface IProcessamentoAnexoRepositorio : IBaseRepositorio<ProcessamentoAnexo>
    {
        Resultado<DatatableRetorno<object>> SelecionarPorFiltro(string? Id, int? ambienteId, string? projetoId, DateTime? dataDe, DateTime? dataAte, bool excluido, int? start, int? length);
    }
}

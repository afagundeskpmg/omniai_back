using Dominio.Entidades;

namespace Dados.Interface
{
    public interface IEntidadeRepositorio : IBaseRepositorio<Entidade>
    {
        Resultado<DatatableRetorno<object>> SelecionarPorFiltro(int? ambienteId, string? documentoId, string? nome, DateTime? dataDe, DateTime? dataAte,bool excluido, int? start, int? length);
    }
}

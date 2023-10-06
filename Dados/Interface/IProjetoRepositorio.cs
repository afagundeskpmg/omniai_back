using Dominio.Entidades;

namespace Dados.Interface
{
    public interface IProjetoRepositorio : IBaseRepositorio<Projeto>
    {
        Resultado<DatatableRetorno<object>> SelecionarProjetosFiltro(int? ambienteId, string? nome, DateTime? dataDe, DateTime? dataAte, bool excluido, int? start, int? length);
    }
}

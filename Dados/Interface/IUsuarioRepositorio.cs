using Dominio.Entidades;

namespace Dados.Interface
{
    public interface IUsuarioRepositorio : IBaseRepositorio<Usuario>
    {
        Resultado<DatatableRetorno<object>> SelecionarUsuariosPorFiltro(int[] organizacoesIds, string nome, string email, bool? excluido, int? start, int? length);
    }
}

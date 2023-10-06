using Dominio.Entidades;
using Dominio.Util.API.B2C;

namespace Aplicacao.Interface
{
    public interface IUsuarioAplicacao : IBaseAplicacao<Usuario>
    {
        Resultado<DatatableRetorno<object>> SelecionarUsuariosPorFiltro(int[] ambientesIds, string nome, string email, bool? excluido, int? start, int? length);
        Task<Resultado<Token>> SelecionarMicrosoftGrathToken(string username, string password);
        Task<Resultado<Usuario>> Salvar(Ambiente ambiente, Usuario usuarioLogado, Papel papel, bool permitirAlterarEmpresa, string email, string nome);
        Resultado<object> SalvarClaimsUsuario(Usuario usuario, Usuario usuarioLogado, int[] claimsSelecionadasIds);
    }
}

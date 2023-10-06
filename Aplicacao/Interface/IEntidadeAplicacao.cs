using Dominio.Entidades;

namespace Aplicacao.Interface
{
    public interface IEntidadeAplicacao : IBaseAplicacao<Entidade>
    {
        Resultado<object> Cadastrar(string nome, string pergunta, string documentoTipoId, Usuario usuarioLogado);
        Resultado<DatatableRetorno<object>> SelecionarPorFiltro(int? ambienteId, string? documentoTipoId, string? nome, DateTime? dataDe, DateTime? dataAte,bool excluido, int? start, int? lenght);
    }
}

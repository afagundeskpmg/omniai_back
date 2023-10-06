using Dados.Interface;
using Dominio.Entidades;

namespace Aplicacao.Interface
{
    public interface IProjetoAplicacao: IBaseAplicacao<Projeto>
    {
        Resultado<object> Cadastrar(string nome, int ambienteId, Usuario usuario);
        Resultado<DatatableRetorno<object>> SelecionarPorFiltro(int? ambienteId, string? nome, DateTime? dataDe, DateTime? dataAte,bool excluido, int? start, int? length);
    }
}

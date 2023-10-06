using Dominio.Entidades;

namespace Dados.Interface
{
    public interface IDocumentoTipoRepositorio : IBaseRepositorio<DocumentoTipo>
    {
        Resultado<DatatableRetorno<object>> SelecionarProjetosFiltro(int? ambienteId, string? nome, DateTime? dataDe, DateTime? dataAte,bool excluido, int? start, int? length);
    }
}

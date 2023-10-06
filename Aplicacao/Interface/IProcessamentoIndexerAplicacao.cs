using Dominio.Entidades;

namespace Aplicacao.Interface
{
    public interface IProcessamentoIndexerAplicacao: IBaseAplicacao<ProcessamentoIndexer>
    {        
        void Processar(ref ProcessamentoIndexer processamento);
        Resultado<DatatableRetorno<object>> SelecionarPorFiltro(string? Id, int? ambienteId, string? projetoId, DateTime? dataDe, DateTime? dataAte, bool excluido, int? start, int? length);
        Resultado<object> DeletarIndexerDataSource(ref ProcessamentoIndexer processamento);
        Resultado<ProcessamentoIndexer> CadastrarPorProjeto(Projeto projeto, Usuario usuarioLogado);
    }
}

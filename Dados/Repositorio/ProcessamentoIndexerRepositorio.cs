using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;
using Dominio.Validacoes;

namespace Dados.Repositorio
{
    public class ProcessamentoIndexerRepositorio : BaseRepositorio<ProcessamentoIndexer>, IProcessamentoIndexerRepositorio
    {
        public ProcessamentoIndexerRepositorio(ContextoBase contexto) : base(contexto)
        {

        }
        public Resultado<DatatableRetorno<object>> SelecionarPorFiltro(string? Id,int? ambienteId, string? projetoId, DateTime? dataDe, DateTime? dataAte, bool excluido, int? start, int? length)
        {
            var resultado = new Resultado<DatatableRetorno<object>>();

            IQueryable<ProcessamentoIndexer> processamento = _contexto.ProcessamentoIndexer.OrderByDescending(u => u.Id);

            if(!string.IsNullOrEmpty(Id))
                processamento = processamento.Where(p => p.Id == Id);

            if (ambienteId.HasValue)
                processamento = processamento.Where(p => p.Projeto.AmbienteId == ambienteId);

            int recordsTotal = processamento.Count();

            processamento = processamento.Where(x => x.Excluido == excluido);

            if (!string.IsNullOrEmpty(projetoId))
                processamento = processamento.Where(p => p.ProjetoId == projetoId); 

            if (dataDe != null)
                processamento = processamento.Where(p => p.CriadoEm >= dataDe);

            if (dataAte != null)
                processamento = processamento.Where(p => p.CriadoEm <= dataAte);

            var dataTableRetorno = new DatatableRetorno<object>()
            {
                data = new List<object>(),
                recordsTotal = recordsTotal,
                recordsFiltered = processamento.Count(),
            };

            if (start.HasValue && start.Value < dataTableRetorno.recordsFiltered)
                processamento = processamento.Skip(start.Value);

            if (length.HasValue)
                processamento = processamento.Take(Math.Min(length.Value, dataTableRetorno.recordsFiltered));

            foreach (var obj in processamento.ToList())
                dataTableRetorno.data.Add(obj.SerializarParaListar());

            resultado.Retorno = dataTableRetorno;

            resultado.Sucesso = resultado.Retorno != null;

            return resultado;
        }
    }
}
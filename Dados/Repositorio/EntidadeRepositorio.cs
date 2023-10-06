using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class EntidadeRepositorio : BaseRepositorio<Entidade>, IEntidadeRepositorio
    {
        public EntidadeRepositorio(ContextoBase contexto) : base(contexto)
        {

        }

        public Resultado<DatatableRetorno<object>> SelecionarPorFiltro(int? ambienteId, string? documentoId, string? nome, DateTime? dataDe, DateTime? dataAte, bool excluido, int? start, int? length)
        {
            var resultado = new Resultado<DatatableRetorno<object>>();

            IQueryable<Entidade> entidades = _contexto.Entidade.OrderByDescending(u => u.Id);

            if (ambienteId.HasValue)
                entidades = entidades.Where(p => p.DocumentoTipo.AmbienteId == ambienteId);

            int recordsTotal = entidades.Count();

            entidades = entidades.Where(x => x.Excluido == excluido);

            if (!string.IsNullOrEmpty(documentoId))
                entidades = entidades.Where(p => p.DocumentoTipoId == documentoId);

            if (!string.IsNullOrEmpty(nome))
                entidades = entidades.Where(x => x.Nome.ToUpper().Contains(nome) || nome.ToUpper().Contains(x.Nome.ToUpper()));

            if (dataDe != null)
                entidades = entidades.Where(p => p.CriadoEm >= dataDe);

            if (dataAte != null)
                entidades = entidades.Where(p => p.CriadoEm <= dataAte);

            var dataTableRetorno = new DatatableRetorno<object>()
            {
                data = new List<object>(),
                recordsTotal = recordsTotal,
                recordsFiltered = entidades.Count(),
            };

            if (start.HasValue && start.Value < dataTableRetorno.recordsFiltered)
                entidades = entidades.Skip(start.Value);

            if (length.HasValue)
                entidades = entidades.Take(Math.Min(length.Value, dataTableRetorno.recordsFiltered));

            foreach (var obj in entidades.ToList())
                dataTableRetorno.data.Add(obj.SerializarParaListar());

            resultado.Retorno = dataTableRetorno;

            resultado.Sucesso = resultado.Retorno != null;

            return resultado;
        }
    }
}
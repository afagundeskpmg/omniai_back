using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class DocumentoTipoRepositorio : BaseRepositorio<DocumentoTipo>, IDocumentoTipoRepositorio
    {
        public DocumentoTipoRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }

        public Resultado<DatatableRetorno<object>> SelecionarProjetosFiltro(int? ambienteId, string? nome, DateTime? dataDe, DateTime? dataAte, bool excluido, int? start, int? length)
        {
            var resultado = new Resultado<DatatableRetorno<object>>();

            IQueryable<DocumentoTipo> documentosTipo = _contexto.DocumentoTipo.OrderByDescending(u => u.Id);

            if (ambienteId.HasValue)
                documentosTipo = documentosTipo.Where(p => p.AmbienteId == ambienteId);

            int recordsTotal = documentosTipo.Count();

            documentosTipo = documentosTipo.Where(x => x.Excluido == excluido);

            if (!string.IsNullOrEmpty(nome))
                documentosTipo = documentosTipo.Where(x => x.Nome.ToUpper().Contains(nome) || nome.ToUpper().Contains(x.Nome.ToUpper()));

            if (dataDe != null)
                documentosTipo = documentosTipo.Where(p => p.CriadoEm >= dataDe);

            if (dataAte != null)
                documentosTipo = documentosTipo.Where(p => p.CriadoEm <= dataAte);

            var dataTableRetorno = new DatatableRetorno<object>()
            {
                data = new List<object>(),
                recordsTotal = recordsTotal,
                recordsFiltered = documentosTipo.Count(),
            };

            if (start.HasValue && start.Value < dataTableRetorno.recordsFiltered)
                documentosTipo = documentosTipo.Skip(start.Value);

            if (length.HasValue)
                documentosTipo = documentosTipo.Take(Math.Min(length.Value, dataTableRetorno.recordsFiltered));

            foreach (var obj in documentosTipo.ToList())
                dataTableRetorno.data.Add(obj.SerializarParaListar());

            resultado.Retorno = dataTableRetorno;

            resultado.Sucesso = resultado.Retorno != null;

            return resultado;
        }
    }
}
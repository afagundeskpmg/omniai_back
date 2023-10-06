using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class ProjetoRepositorio : BaseRepositorio<Projeto>, IProjetoRepositorio
    {
        public ProjetoRepositorio(ContextoBase contexto) : base(contexto)
        {

        }

        public Resultado<DatatableRetorno<object>> SelecionarProjetosFiltro(int? ambienteId, string? nome, DateTime? dataDe, DateTime? dataAte, bool excluido, int? start, int? length)
        {
            var resultado = new Resultado<DatatableRetorno<object>>();

            IQueryable<Projeto> projetos = _contexto.Projeto.OrderByDescending(u => u.Id);

            if (ambienteId.HasValue)
                projetos = projetos.Where(p => p.AmbienteId == ambienteId);

            int recordsTotal = projetos.Count();

            projetos = projetos.Where(x => x.Excluido == excluido);

            if (!string.IsNullOrEmpty(nome))
                projetos = projetos.Where(x => x.Nome.ToUpper().Contains(nome) || nome.ToUpper().Contains(x.Nome.ToUpper()));

            if (dataDe != null)
                projetos = projetos.Where(p => p.CriadoEm >= dataDe);

            if (dataAte != null)
                projetos = projetos.Where(p => p.CriadoEm <= dataAte);

            var dataTableRetorno = new DatatableRetorno<object>()
            {
                data = new List<object>(),
                recordsTotal = recordsTotal,
                recordsFiltered = projetos.Count(),
            };

            if (start.HasValue && start.Value < dataTableRetorno.recordsFiltered)
                projetos = projetos.Skip(start.Value);

            if (length.HasValue)
                projetos = projetos.Take(Math.Min(length.Value, dataTableRetorno.recordsFiltered));

            foreach (var obj in projetos.ToList())
                dataTableRetorno.data.Add(obj.SerializarParaListar());

            resultado.Retorno = dataTableRetorno;

            resultado.Sucesso = resultado.Retorno != null;

            return resultado;
        }
    }
}
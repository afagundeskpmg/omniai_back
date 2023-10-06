using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class UsuarioRepositorio : BaseRepositorio<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
        public Resultado<DatatableRetorno<object>> SelecionarUsuariosPorFiltro(int[] organizacoesIds, string nome, string email, bool? excluido, int? start, int? length)
        {
            var resultado = new Resultado<DatatableRetorno<object>>();

            IQueryable<Usuario> usuarios = _contexto.Usuario.OrderBy(u => u.UserName);

            if (organizacoesIds != null && organizacoesIds.Any())
                usuarios = usuarios.Where(u => u.Ambientes.Any(a => organizacoesIds.Contains(a.Id)));

            int recordsTotal = usuarios.Count();

            if (!string.IsNullOrEmpty(nome))
                usuarios = usuarios.Where(x => x.Nome.ToUpper().Contains(nome) || nome.ToUpper().Contains(x.Nome.ToUpper()));

            if (!string.IsNullOrEmpty(email))
                usuarios = usuarios.Where(x => x.UserName.ToUpper().Contains(email.ToUpper()) || email.ToUpper().Contains(x.UserName.ToUpper()));

            if (excluido.HasValue)
                usuarios = usuarios.Where(u => u.Excluido == excluido.Value);

            var dataTableRetorno = new DatatableRetorno<object>()
            {
                data = new List<object>(),
                recordsTotal = recordsTotal,
                recordsFiltered = usuarios.Count(),
            };

            if (start.HasValue && start.Value < dataTableRetorno.recordsFiltered)
                usuarios = usuarios.Skip(start.Value);

            if (length.HasValue)
                usuarios = usuarios.Take(Math.Min(length.Value, dataTableRetorno.recordsFiltered));

            foreach (var usuario in usuarios.ToList())
                dataTableRetorno.data.Add(usuario.SerializarParaListar());

            resultado.Retorno = dataTableRetorno;

            resultado.Sucesso = resultado.Retorno != null;

            return resultado;
        }
    }
}
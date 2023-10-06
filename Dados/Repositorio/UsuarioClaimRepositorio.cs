using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class UsuarioClaimRepositorio : BaseRepositorio<UsuarioClaim>, IUsuarioClaimRepositorio
    {
        public UsuarioClaimRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
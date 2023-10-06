using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class EmailRepositorio : BaseRepositorio<Email>, IEmailRepositorio
    {
        public EmailRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
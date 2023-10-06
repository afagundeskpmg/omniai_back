using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class PessoaRepositorio : BaseRepositorio<Pessoa>, IPessoaRepositorio
    {
        public PessoaRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
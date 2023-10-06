using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class PessoaJuridicaRepositorio : BaseRepositorio<PessoaJuridica>, IPessoaJuridicaRepositorio
    {
        public PessoaJuridicaRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
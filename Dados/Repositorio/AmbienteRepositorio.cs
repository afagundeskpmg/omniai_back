using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class AmbienteRepositorio : BaseRepositorio<Ambiente>, IAmbienteRepositorio
    {
        public AmbienteRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
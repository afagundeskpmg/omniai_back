using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class ProcessamentoPerguntaRepositorio : BaseRepositorio<ProcessamentoPergunta>, IProcessamentoPerguntaRepositorio
    {
        public ProcessamentoPerguntaRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
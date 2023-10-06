using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class ProcessamentoStatusRepositorio : BaseRepositorio<ProcessamentoStatus>, IProcessamentoStatusRepositorio
    {
        public ProcessamentoStatusRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
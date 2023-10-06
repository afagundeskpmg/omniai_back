using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class ProcessamentoTipoRepositorio : BaseRepositorio<ProcessamentoTipo>, IProcessamentoTipoRepositorio
    {
        public ProcessamentoTipoRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
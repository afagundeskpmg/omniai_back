using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class ArquivoFormatoRepositorio : BaseRepositorio<ArquivoFormato>, IArquivoFormatoRepositorio
    {
        public ArquivoFormatoRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
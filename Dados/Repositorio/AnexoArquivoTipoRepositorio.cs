using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class AnexoArquivoTipoRepositorio : BaseRepositorio<AnexoArquivoTipo>, IAnexoArquivoTipoRepositorio
    {
        public AnexoArquivoTipoRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
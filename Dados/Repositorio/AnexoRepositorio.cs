using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class AnexoRepositorio : BaseRepositorio<Anexo>, IAnexoRepositorio
    {
        public AnexoRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
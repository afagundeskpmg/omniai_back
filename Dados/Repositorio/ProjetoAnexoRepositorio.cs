using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class ProjetoAnexoRepositorio : BaseRepositorio<ProjetoAnexo>, IProjetoAnexoRepositorio
    {
        public ProjetoAnexoRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
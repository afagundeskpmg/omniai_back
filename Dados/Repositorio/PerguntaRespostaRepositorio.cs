using Dados.Contexto;
using Dados.Interface;
using Dominio.Entidades;

namespace Dados.Repositorio
{
    public class PerguntaRespostaRepositorio : BaseRepositorio<PerguntaResposta>, IPerguntaRespostaRepositorio
    {
        public PerguntaRespostaRepositorio(ContextoBase contexto):base (contexto)
        {
            
        }
    }
}
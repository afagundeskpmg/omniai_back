using Dominio.Entidades;

namespace Aplicacao.Interface
{
    public interface IPerguntaRespostaAplicacao : IBaseAplicacao<PerguntaResposta>
    {
        Resultado<PerguntaResposta> Perguntar(PerguntaResposta perguntaResposta);
    }
}

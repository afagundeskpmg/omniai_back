using Dominio.Entidades;

namespace Aplicacao.Interface
{
    public interface IProcessamentoPerguntaAplicacao: IBaseAplicacao<ProcessamentoPergunta>
    {
        void Processar(ref ProcessamentoPergunta processamento);
    }
}

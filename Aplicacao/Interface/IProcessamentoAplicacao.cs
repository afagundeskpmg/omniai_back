using Dominio.Entidades;

namespace Aplicacao.Interface
{
    public interface IProcessamentoAplicacao: IBaseAplicacao<Processamento>
    {
        public void FinalizarProcessamento(ProcessamentoStatusEnum processamentoStatusEnum, string processamentoId, string log);
        public void AtribuirQueueProcessamento(ref Processamento processamento, string connection, string queueName, string mensagem, int maxRetries);
    }
}

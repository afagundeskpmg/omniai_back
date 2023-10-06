using Dominio.Entidades;

namespace Dados.Seed
{
    public static class CargaProcessamentoStaus
    {
        public static List<object> GerarCarga()
        {
            return new List<object>()
            {
                new ProcessamentoStatus() { Id = (int)ProcessamentoStatusEnum.ProcessamentoSolicitado, Nome = "Solicitado" },
                new ProcessamentoStatus() { Id = (int)ProcessamentoStatusEnum.EmProcessamento, Nome = "Em Processamento" },
                new ProcessamentoStatus() { Id = (int)ProcessamentoStatusEnum.ProcessadoComErro, Nome = "Processado com Erro" },
                new ProcessamentoStatus() { Id = (int)ProcessamentoStatusEnum.ProcessadoComSucesso, Nome = "Processado com Sucesso" },
            };
        }
    }
}

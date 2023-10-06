using Aplicacao.Aplicacao;
using Aplicacao.Interface;
using Dominio.Entidades;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace ProcessamentosOmni.Functions
{
    public class SelecionarProcessamentoNerPendenteEncerramentoFunction
    {
        [FunctionName("SelecionarProcessamentoNerPendenteEncerramentoFunction")]
        public void Run([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            var _configuration = Util.GerarConfiguration();
            Util.InicializarLog(_configuration);

            try
            {
                using (IUnitOfWorkAplicacao _aplicacao = new UnitOfWorkAplicacao(_configuration))
                {
                    var processamentos = _aplicacao.ProcessamentoNer.SelecionarTodos(u => u.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.EmProcessamento && !u.ProcessamentosPerguntaGeradas.Any(x => x.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.EmProcessamento || x.ProcessamentoStatusId == (int)ProcessamentoStatusEnum.ProcessamentoSolicitado)).ToList();

                    foreach (var processamento in processamentos)
                    {
                        try
                        {
                            processamento.FinalizarProcessamentoComStatus(ProcessamentoStatusEnum.ProcessadoComSucesso, null, null);
                            processamento.AtribuirLog(new ProcessamentoLog("Encerrado automaticamente por falta de atualização"));
                            _aplicacao.Processamento.Atualizar(processamento);
                            _aplicacao.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Log.LogArquivo.Log(ex, "Ocorreu um erro ao tentar selecionar processamentos pendentes");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogArquivo.Log(ex, null);
            }
        }
    }
}


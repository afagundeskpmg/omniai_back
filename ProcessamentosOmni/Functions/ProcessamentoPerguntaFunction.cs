using Aplicacao.Aplicacao;
using Aplicacao.Interface;
using Dominio.Entidades;
using Log;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Model;
using Newtonsoft.Json;
using ProcessamentosOmni;
using System;
using System.Threading;

namespace Processamentos
{
    public class ProcessamentoPerguntaFunction
    {
        [FunctionName("ProcessamentoPerguntaFunction")]
        public void Run([QueueTrigger("processamentopergunta", Connection = "StorageConnection")] CloudQueueMessage myQueueItem)
        {
            var _configuration = Util.GerarConfiguration();
            Util.InicializarLog(_configuration);
            int dequeueCount = myQueueItem.DequeueCount;

            if (dequeueCount <= 1)
            {
                try
                {
                    using (IUnitOfWorkAplicacao _aplicacao = new UnitOfWorkAplicacao(_configuration))
                    {
                        var i = 0;
                        var model = JsonConvert.DeserializeObject<ProcessamentoModel>(myQueueItem.AsString);
                        Util.SetarIdioma(model.CultureInfo);
                        ProcessamentoPergunta processamento = null;

                        do
                        {
                            processamento = _aplicacao.ProcessamentoPergunta.SelecionarFirstOrDefault(p => p.Id == model.Id);

                            if (processamento == null || string.IsNullOrEmpty(processamento.QueueMessageId))
                                Thread.Sleep(1000);
                            i++;
                        } while (processamento == null && i < 3);

                        if (processamento == null)
                            LogArquivo.Log(null, string.Concat("Objecto não encontrado para o processamento id ", model.Id));
                        else
                        {
                            _aplicacao.ProcessamentoPergunta.Processar(ref processamento);
                            _aplicacao.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogArquivo.Log(ex, null);
                }
            }
        }
    }
}

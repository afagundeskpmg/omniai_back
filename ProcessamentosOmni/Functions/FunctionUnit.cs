using Aplicacao.Aplicacao;
using Aplicacao.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ProcessamentosOmni
{
    public static class FunctionUnit
    {
        [FunctionName("FunctionUnit")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            var responseMessage = string.Empty;

            try
            {
                var configuration = Util.GerarConfiguration();
                Util.InicializarLog(configuration);

                responseMessage += "vai tentar instanciar unit";
                using (IUnitOfWorkAplicacao _aplicacao = new UnitOfWorkAplicacao(configuration))
                {
                    responseMessage += "\ninstanciou unit, vai tentar selcionar pessoa";


                    try
                    {
                        var pessoa = _aplicacao.Pessoa.SelecionarFirstOrDefault();

                        responseMessage += "\nconseguiu selecionar pessoa " + pessoa.Nome;
                    }
                    catch (Exception ex2)
                    {
                        responseMessage += "\nerro ao selecionar pessoa " + ex2.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                responseMessage += "\nerro ao instanciar unit " + ex.ToString();
            }

            return new OkObjectResult(responseMessage);
        }
    }
}

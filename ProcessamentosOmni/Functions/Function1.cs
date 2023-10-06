using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProcessamentosOmni
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string responseMessage = "";

            try
            {
                try
                {
                    responseMessage += "\nvai tentar iniciar o configuration";

                    var configuration = Util.GerarConfiguration();

                    try
                    {
                        string executableLocation = Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                        var files = Directory.GetFiles(executableLocation);

                        var arquivo = files?.FirstOrDefault(f => f.Contains("local.settings.json"));

                        if (string.IsNullOrEmpty(arquivo))
                            responseMessage += "\nnão encontrou o arquivo local.settings.json";
                        else
                        {
                            var texto = File.ReadAllText(arquivo);
                            responseMessage += "\nArquivo local.settings.json: \n" + texto;
                        }

                        responseMessage += "\n\ncopnseguiu instanciar o configrautio";

                        responseMessage += "\nvai tentar inicializar logs";
                        Util.InicializarLog(configuration);

                        try
                        {
                            responseMessage += "\nvai tentar enviar log";

                            responseMessage += "\n" + Log.LogArquivo.Log(null, "Teste cai aqui!");
                        }
                        catch (Exception ex)
                        {
                            responseMessage += "\nfalha ao enviar logs " + ex.ToString();
                        }
                    }
                    catch (Exception ex1)
                    {
                        responseMessage += "\nfalha ao inicializar logs " + ex1.ToString();
                    }
                }
                catch (Exception ex2)
                {
                    responseMessage += "\nfalha ao gerar configuration " + ex2.ToString();
                }
            }
            catch (Exception ex3)
            {
                responseMessage += ex3.Message;
            }

            return new OkObjectResult(responseMessage);
        }

        public static string ListarArquivosEDiretorios(string diretorio)
        {
            var retorno = string.Empty;
            try
            {
                // Lista todos os arquivos no diretório atual
                string[] arquivos = Directory.GetFiles(diretorio);
                foreach (string arquivo in arquivos)
                {
                    retorno += "\nArquivo: " + arquivo;
                }

                // Lista todos os subdiretórios no diretório atual
                string[] subdiretorios = Directory.GetDirectories(diretorio);
                foreach (string subdiretorio in subdiretorios)
                {
                    retorno += "\nDiretório: " + subdiretorio;

                    // Chama a função recursivamente para listar arquivos e subdiretórios do subdiretório atual
                    retorno += ListarArquivosEDiretorios(subdiretorio);
                }
            }
            catch (Exception ex)
            {
                retorno += "\nErro: " + ex.ToString();
            }

            return retorno;
        }
    }
}

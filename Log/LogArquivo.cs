using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Reflection;
using System.Text;



namespace Log
{
    public static class LogArquivo
    {
        private static LogConfig _logConfig;
        public static void Initialize(LogConfig logConfig)
        {
            _logConfig = logConfig;
        }
        public static string Log(Exception ex, string comment)
        {
            var retorno = string.Empty;

            try
            {
                var texto = new StringBuilder();

                if (!string.IsNullOrEmpty(comment))
                    texto.AppendLine("Comentarios: " + comment);

                if (ex != null)
                {
                    texto.AppendLine(ex.ToString());
                    texto.AppendLine(ex.StackTrace);
                    texto.AppendLine(ex.Message);
                }

                if (!string.IsNullOrEmpty(texto.ToString()))
                {
                    var conn = _logConfig.StorageConnection;
                    var container = _logConfig.BlobContainerName;

                    var caminhoArquivo = Path.Combine(DateTime.Today.ToString("yyyy-MM"), DateTime.Today.ToString("dd"), DateTime.Now.ToString("HH-mm-ss") + "_" + Guid.NewGuid().ToString() + ".txt");

                    BlobServiceClient blobServiceClient = new BlobServiceClient(conn);
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(container);
                    BlobClient blobClient = containerClient.GetBlobClient(caminhoArquivo);

                    containerClient.CreateIfNotExists();

                    using (var ms = new MemoryStream())
                    {
                        using (var sw = new StreamWriter(ms, Encoding.UTF8))
                        {
                            sw.WriteLine(texto);
                            sw.Flush();
                            ms.Position = 0;

                            BlobUploadOptions options = new BlobUploadOptions() { HttpHeaders = new BlobHttpHeaders { ContentType = "text/txt" } };
                            options.Tags = new Dictionary<string, string>()
                            {
                                 { "DataLong", DateTime.Today.Ticks.ToString() },
                                 { "Assembly", Assembly.GetCallingAssembly().GetName().Name }
                            };
                            blobClient.Upload(ms, options);

                            sw.Close();
                            ms.Close();
                        }
                    }
                }
            }
            catch (Exception ex1)
            {
                retorno += "Houve um problema no tratamento de uma exceção";

                if (ex != null)
                    retorno += "\nExceção original: " + ex.ToString() + "\n" + "Comentários: " + comment;

                retorno += "\nExceção no controle: " + ex1.ToString();
            }

            return retorno;
        }
    }
}
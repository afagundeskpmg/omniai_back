using System.Globalization;
using System.Reflection;
using System.Text;


namespace Aplicacao.Util
{
    public class AssemblyUtil
    {
        public static string LerArquivoEmbutido(string trechoNomeArquivo, Assembly assembly)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            List<string> arquivosCarga = assembly.GetManifestResourceNames().ToList();
            var resourceName = arquivosCarga.FirstOrDefault(a => a.Contains(trechoNomeArquivo));
            if (resourceName != null)
            {
                string texto = string.Empty;

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage)))
                {
                    texto = reader.ReadToEnd();
                }

                return texto;
            }
            return string.Empty;
        }
    }
}

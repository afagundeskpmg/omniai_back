using Log;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace ProcessamentosOmni
{
    public static class Util
    {
        public static void InicializarLog(IConfiguration configuration)
        {
            var logProv = new LogConfigProvider(configuration);
            var logConfig = logProv.GetLogConfig();
            LogArquivo.Initialize(logConfig);
        }

        public static IConfiguration GerarConfiguration()
        {
            string executableLocation = Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            return new ConfigurationBuilder()
                .SetBasePath(executableLocation)
                .AddJsonFile("local.settings.json")
                .Build();
        }

        public static void SetarIdioma(string culture)
        {
            var cultures = new string[] { "en-US", "es-ES", "pt-BR" };

            if (culture == null || !cultures.Any(c => c == culture))
                culture = cultures.First();

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(culture);
        }
    }
}

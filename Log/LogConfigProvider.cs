using Microsoft.Extensions.Configuration;

namespace Log
{
    public class LogConfigProvider
    {
        private readonly IConfiguration _configuration;

        public LogConfigProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public LogConfig GetLogConfig()
        {
            var logConfig = new LogConfig();
            logConfig.StorageConnection = _configuration.GetSection("LogConfig")["StorageConnection"];
            logConfig.BlobContainerName = _configuration.GetSection("LogConfig")["BlobContainerName"];
            return logConfig;
        }
    }
}
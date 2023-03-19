using Microsoft.Extensions.Configuration;
using NLog;

namespace Api.Infrastructure.Logger;

public class BaseCustomLogger : ICustomLogger 
{
    public string LogPath { get; set; }
    public NLog.Logger Manager { get; set; }
    
    protected void ConfigureManager(IConfiguration configuration)
    {
        LogPath = configuration["LoggerPath"];
        LogManager.Configuration.Variables["path"] = LogPath;
    }
}
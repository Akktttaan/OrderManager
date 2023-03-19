using Microsoft.Extensions.Configuration;
using NLog;

namespace Api.Infrastructure.Logger;

public class NLogger : BaseCustomLogger
{
    public NLogger(IConfiguration configuration) 
    {
        ConfigureManager(configuration);
        Manager = LogManager.GetCurrentClassLogger();
    }
}
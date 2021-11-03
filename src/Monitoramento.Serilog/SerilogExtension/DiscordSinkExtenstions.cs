using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace Monitoramento.SerilogExtension
{
    internal static class DiscordSinkExtenstions
    {
        public static LoggerConfiguration Discord(
                this LoggerSinkConfiguration loggerConfiguration,
                IConfiguration configuration,
                LogEventLevel restrictedToMinimumLevel = LogEventLevel.Information)
        {
            return loggerConfiguration.Sink(
                new DiscordSink(configuration, restrictedToMinimumLevel));
        }
    }
}
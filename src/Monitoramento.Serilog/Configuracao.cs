using Microsoft.Extensions.Configuration;
using Monitoramento.Serilog;
using Monitoramento.Serilog.Extensions;
using Monitoramento.Serilog.Migrations;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using System;

namespace Monitoramento
{
    public static class Configuracao
    {
        public static ILogger ConfigurarSerilog(IConfiguration configuration, bool enableConsoleSerilogDebugger = false)
        {
            Runner
                .RodarMigrations(ConfiguracaoVo.Configuracao.GetConnectionString("LogConnection"));
            //
            var logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.With(new EnrichValidator(configuration))
                .WriteTo.Discord(configuration, LogEventLevel.Error)
                .CreateLogger();

            if (enableConsoleSerilogDebugger)
                SelfLog.Enable(Console.Out);

            return logger;
        }
    }
}

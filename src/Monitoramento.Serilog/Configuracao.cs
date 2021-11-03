using Microsoft.Extensions.Configuration;
using Monitoramento.Infra.Data.Migrations;
using Monitoramento.Infra.IoC;
using Monitoramento.SerilogExtension;
using Serilog;
using Serilog.Debugging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoramento
{
    public static class Configuracao
    {
        public static ILogger ConfigurarSerilog(IConfiguration configuration, bool enableConsoleSerilogDebugger = false)
        {
            var cs = configuration.GetConnectionString("LogConnection");

            if (string.IsNullOrEmpty(cs))
                cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjetoCV;Integrated Security=True;";

            Runner
                .RodarMigrations(cs);

            var logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.With<EnrichValidator>()
                .WriteTo.Discord(configuration, Serilog.Events.LogEventLevel.Error)
                .CreateLogger();

            if (enableConsoleSerilogDebugger)
                SelfLog.Enable(Console.Out);

            return logger;
        }
    }
}

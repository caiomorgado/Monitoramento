using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Monitoramento.Domain;
using Monitoramento.Domain.Interface;
using Monitoramento.Infra.Data.Migrations;
using System;

namespace Monitoramento.Infra.IoC
{
    public static class Runner
    {
        private static IServiceProvider CreateServices(string connectionString)
        {
            var teste = typeof(Teste).Assembly;

            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(teste).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);        
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }

        public static void RodarMigrations(string connectionString)
        {
            var serviceProvider = CreateServices(connectionString);

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }
    }
}

using Microsoft.Extensions.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;

namespace Monitoramento.Serilog.Extensions
{
    internal class EnrichValidator : ILogEventEnricher
    {
        private readonly IConfiguration _configuration;

        public EnrichValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            bool.TryParse(_configuration.GetSection("GravarRequisicao").Value, out var gravarRequisicao);

            if (!gravarRequisicao && logEvent.Exception == null)
            {
                logEvent.RemovePropertyIfPresent("RequisicaoHttp");
                logEvent.RemovePropertyIfPresent("RespostaHttp");
            }

            var versao = Environment.GetEnvironmentVariable("RELEASE_NUMBER") ?? "local";
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Versao", versao));
        }
    }
}

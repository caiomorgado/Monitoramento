using Discord;
using Discord.Webhook;
using Microsoft.Extensions.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Linq;

namespace Monitoramento.Serilog.Extensions
{
    internal class DiscordSink : ILogEventSink
    {
        private readonly IConfiguration _configuration;
        private readonly LogEventLevel _restrictedToMinimumLevel;
        public static readonly string[] _listaPropriedadesPreDefinidas = { "Versao", "UsuarioId", "ActionName", "RequisicaoHttp", "Aplicacao" };

        public DiscordSink(
            IConfiguration configuration,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Information)
        {
            _configuration = configuration;
            _restrictedToMinimumLevel = restrictedToMinimumLevel;
        }

        public void Emit(LogEvent logEvent)
        {
            SendMessage(logEvent);
        }

        private void SendMessage(LogEvent logEvent)
        {
            DiscordWebhookClient webHook = null;

            try
            {
                if (ShouldNotLogMessage(_restrictedToMinimumLevel, logEvent.Level))
                    return;

                var webhookIdConfig = _configuration
                ?.GetSection("Discord")
                ?.GetSection("webhookId")
                ?.Value;

                var webhookTokenConfig = _configuration
                    ?.GetSection("Discord")
                    ?.GetSection("webhookToken")
                    ?.Value;

                ulong.TryParse(webhookIdConfig, out var webhookId);

                if (webhookId == 0 || string.IsNullOrEmpty(webhookTokenConfig))
                    return;

                var embedBuilder = new EmbedBuilder();
                webHook = new DiscordWebhookClient(webhookId, webhookTokenConfig);

                SpecifyEmbedLevel(logEvent.Level, embedBuilder);

                if (logEvent.Properties?.Any() ?? false)
                {
                    logEvent.Properties?.ToList()?.ForEach(propriedade =>
                    {
                        if (_listaPropriedadesPreDefinidas.Contains(propriedade.Key.ToString()))
                            embedBuilder
                            .AddField(propriedade.Key.ToString(), FormatMessage(propriedade.Value.ToString(), 1000));
                    });
                }

                embedBuilder
                    .AddField("Mensagem:", FormatMessage(logEvent.MessageTemplate?.Text, 1000));

                if (logEvent.Exception != null)
                    embedBuilder
                        .AddField("Exceção:", FormatMessage(logEvent?.Exception?.Message, 1000));

                webHook.SendMessageAsync(null, false, new Embed[] { embedBuilder.Build() })
                        .GetAwaiter()
                        .GetResult();
            }
            catch (Exception ex)
            {
                if (webHook != null)
                    webHook.SendMessageAsync(
                        $"ooo snap, {ex.Message}", false)
                        .GetAwaiter()
                        .GetResult();
            }
        }
        private static void SpecifyEmbedLevel(LogEventLevel level, EmbedBuilder embedBuilder)
        {
            switch (level)
            {
                case LogEventLevel.Verbose:
                    embedBuilder.Title = $":loud_sound: {LogEventLevel.Verbose}";
                    embedBuilder.Color = Color.LightGrey;
                    break;
                case LogEventLevel.Debug:
                    embedBuilder.Title = $":mag: {LogEventLevel.Debug}";
                    embedBuilder.Color = Color.LightGrey;
                    break;
                case LogEventLevel.Information:
                    embedBuilder.Title = $":information_source: {LogEventLevel.Information}";
                    embedBuilder.Color = new Color(0, 186, 255);
                    break;
                case LogEventLevel.Warning:
                    embedBuilder.Title = $":warning: {LogEventLevel.Warning}";
                    embedBuilder.Color = new Color(255, 204, 0);
                    break;
                case LogEventLevel.Error:
                    embedBuilder.Title = $":x: {LogEventLevel.Error}";
                    embedBuilder.Color = Color.Red;
                    break;
                case LogEventLevel.Fatal:
                    embedBuilder.Title = $":skull_crossbones: {LogEventLevel.Fatal}";
                    embedBuilder.Color = Color.DarkRed;
                    break;
                default:
                    break;
            }
        }

        public static string FormatMessage(string message, int maxLenght)
        {
            if (message.Length > maxLenght)
                message = $"{message.Substring(0, maxLenght)} ...";

            if (!string.IsNullOrWhiteSpace(message))
                message = $"```{message}```";

            return message;
        }

        private static bool ShouldNotLogMessage(LogEventLevel minimumLogEventLevel, LogEventLevel messageLogEventLevel)
        {
            if ((int)messageLogEventLevel < (int)minimumLogEventLevel)
                return true;
            return false;
        }
    }
}

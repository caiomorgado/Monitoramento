using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Monitoramento.Middleware
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public ExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.Error(
                context?.Exception,
                context?.Exception?.Message);
        }
    }
}

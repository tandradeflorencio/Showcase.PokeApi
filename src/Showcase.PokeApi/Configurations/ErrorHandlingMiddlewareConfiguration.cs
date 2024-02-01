using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Configurations
{
    [ExcludeFromCodeCoverage]
    public class ErrorHandlingMiddlewareConfiguration
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ErrorHandlingMiddlewareConfiguration> _logger;

        private readonly string MensagemErroPadrao = "Ocorreu um erro ao processar a solicitação. Por favor, tente novamente mais tarde.";

        public ErrorHandlingMiddlewareConfiguration(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<ErrorHandlingMiddlewareConfiguration>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, message: ex.Message);
                await HandleExceptionAsync(httpContext);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { mensagem = MensagemErroPadrao }));
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UtilizarManipulacaoDeErros(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddlewareConfiguration>();
        }
    }
}
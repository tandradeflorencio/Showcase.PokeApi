using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Showcase.PokeApi.Models.Responses;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection ConfigurarHealthChecks(this IServiceCollection services, IConfiguration configuracao)
        {
            services.AddHealthChecks()
                .AddSqlite(configuracao.GetConnectionString("SQLite"), name: "Base SQLite");

            return services;
        }

        public static Task EditarResposta(HttpContext context, HealthReport relatorio)
        {
            context.Response.ContentType = "application/json";

            var resultado = new HealthCheckResponse
            {
                Status = relatorio.Status.ToString(),
                Descricao = "Showcase PokeApi",
                Resultados = relatorio.Entries.Select(pair =>
                    new HealthCheckResponse
                    {
                        Status = pair.Value.Status.ToString(),
                        Descricao = pair.Key
                    }).ToList()
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(resultado));
        }
    }
}
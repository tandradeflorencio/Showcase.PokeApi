using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Showcase.PokeApi.Filters;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Showcase.PokeApi.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerConfiguration
    {
        public static IServiceCollection ConfigurarSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var metadata = apiDesc.ActionDescriptor.EndpointMetadata;
                    var versaoApi = metadata.Where(x => x.GetType() == typeof(ApiVersionAttribute)).Cast<ApiVersionAttribute>();
                    if (versaoApi == null)
                    {
                        return false;
                    }

                    var apiVersions = versaoApi.SelectMany(x => x.Versions.Select(y => y.MajorVersion));

                    return apiVersions.Any(v => $"v{v}" == docName);
                });
                swagger.DocumentFilter<SubistituiVersaoComValorExatoNoCaminhoFilter>();
                swagger.OperationFilter<RemoverParametroDaVersaoFilter>();
                var versoesSuportadas = ObterVersoesSuportadas(configuration);

                foreach (var versao in versoesSuportadas)
                {
                    swagger.SwaggerDoc($"v{versao}", new OpenApiInfo()
                    {
                        Version = $"v{versao}",
                        Title = $"{configuration.GetValue<string>("Swagger:Titulo")} v{versao}",
                        Description = configuration.GetValue<string>("Swagger:Descricao"),
                        Contact = new OpenApiContact
                        {
                            Name = configuration.GetValue<string>("Swagger:Empresa"),
                            Url = new Uri(configuration.GetValue<string>("Swagger:Url"))
                        }
                    });
                };

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        private static string[] ObterVersoesSuportadas(IConfiguration configuration)
        {
            var versoesSuportadas = configuration.GetValue<string>("Swagger:VersoesSuportadas");
            var todasVersoesSuportadas = versoesSuportadas != null && versoesSuportadas.Contains(',') ? versoesSuportadas.Split(',') : new string[] { "1" };
            return todasVersoesSuportadas;
        }

        public static IApplicationBuilder UtilizarConfiguracaoSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var versoesSuportadas = ObterVersoesSuportadas(configuration);

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                foreach (var versao in versoesSuportadas)
                    c.SwaggerEndpoint($"v{versao}/swagger.json", $"{configuration.GetValue<string>("Swagger:Titulo")} V{versao}");
            });

            return app;
        }
    }
}
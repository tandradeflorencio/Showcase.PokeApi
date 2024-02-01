using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Showcase.PokeApi.Repositories;
using Showcase.PokeApi.Repositories.Interfaces;
using Showcase.PokeApi.Services;
using Showcase.PokeApi.Services.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace Showcase.PokeApi.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigurarDependencias(this IServiceCollection services)
        {
            services.AddHttpClient<IHttpClientService, HttpClientService>().SetHandlerLifetime(TimeSpan.FromMinutes(30)).AddPolicyHandler(ObterPoliticaDeRetentativas());

            services.AddSingleton<ICapturadoRepository, CapturadoRepository>();
            services.AddSingleton<IMestreRepository, MestreRepository>();

            services.AddSingleton<IMestreService, MestreService>();
            services.AddSingleton<IPokemonService, PokemonService>();
            services.AddSingleton<ITelemetriaService, TelemetriaService>();

            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> ObterPoliticaDeRetentativas()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(3));
        }
    }
}
using AutoFixture;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Serilog;
using Showcase.PokeApi.Repositories.Interfaces;
using Showcase.PokeApi.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace Showcase.PokeApi.Test
{
    [ExcludeFromCodeCoverage]
    public class BaseTest
    {
        protected readonly Fixture _fixture;

        protected readonly IConfiguration _configuracao;

        protected readonly IHttpClientService _httpClientService;

        protected readonly IPokemonRepository _pokemonRepository;

        protected readonly HttpClient _httpClient;

        protected readonly ILogger _logger;

        protected readonly ITelemetriaService _telemetriaService;

        public BaseTest()
        {
            _fixture = new Fixture();
            _configuracao = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();
            _httpClientService = Substitute.For<IHttpClientService>();
            _pokemonRepository = Substitute.For<IPokemonRepository>();
            _telemetriaService = Substitute.For<ITelemetriaService>();

            _logger = Substitute.For<ILogger>();
            _httpClient = Substitute.For<HttpClient>();
        }
    }
}
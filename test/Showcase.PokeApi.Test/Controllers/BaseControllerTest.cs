using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Polly.CircuitBreaker;
using Serilog;
using Showcase.PokeApi.Controllers;
using Showcase.PokeApi.Models;
using Showcase.PokeApi.Models.Responses;
using Showcase.PokeApi.Services.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Showcase.PokeApi.Test.Controllers
{
    [ExcludeFromCodeCoverage]
    public class BaseControllerTest
    {
        protected readonly Fixture _fixture;

        protected readonly IPokemonService _pokemonService;

        protected readonly ILogger _logger;

        protected readonly PokemonsController _pokemonsController;

        protected readonly BaseResponse _baseResponse;

        protected readonly ListaDePokemonsResponse _listaDePokemonsResponse;

        public BaseControllerTest()
        {
            _logger = Substitute.For<ILogger>();
            _fixture = new Fixture();

            _pokemonService = Substitute.For<IPokemonService>();

            _pokemonsController = new PokemonsController(_logger, _pokemonService);

            _baseResponse = _fixture.Create<BaseResponse>();

            _listaDePokemonsResponse = _fixture.Create<ListaDePokemonsResponse>();
        }

        [Fact]
        public async Task TratarResultadoAsync_QuandoOcorrerExcecao_DeveRetornarStatus500InternalServerError()
        {
            //Arrange
            _pokemonService.ListarAsync().Throws(new Exception());

            //Act
            var resultado = (ObjectResult)await _pokemonsController.ListarAsync();

            //Assert
            resultado.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task TratarResultadoAsync_QuandoOcorrerExcecaoCircuito_DeveRetornarStatus500InternalServerError()
        {
            //Arrange
            _pokemonService.ListarAsync().Throws(new BrokenCircuitException());

            //Act
            var resultado = (ObjectResult)await _pokemonsController.ListarAsync();

            //Assert
            resultado.StatusCode.Should().Be(StatusCodes.Status502BadGateway);
        }
    }
}
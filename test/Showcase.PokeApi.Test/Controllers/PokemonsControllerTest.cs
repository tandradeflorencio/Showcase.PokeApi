using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Showcase.PokeApi.Test.Controllers
{
    [ExcludeFromCodeCoverage]
    public class PokemonsControllerTest : BaseControllerTest
    {
        [Fact]
        public async Task ListarAsync_QuandoListar_DeveRetornarStatus200Ok()
        {
            //Arrange
            _pokemonService.ListarAsync().Returns(_listaDePokemonsResponse);
            _listaDePokemonsResponse.StatusCode = StatusCodes.Status200OK;

            //Act
            var resultado = (ObjectResult)await _pokemonsController.ListarAsync();

            //Assert
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
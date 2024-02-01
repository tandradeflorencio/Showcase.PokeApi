using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Showcase.PokeApi.Services;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Showcase.PokeApi.Test.Services
{
    [ExcludeFromCodeCoverage]
    public class PokemonServiceTest : BaseTest
    {
        [Fact]
        public async Task ListarAsync_QuandoRequisicaoBemSucedida_DeveRetornarStatus200OK()
        {
            //Arrange
            var servico = ObterPokemonService();

            //Act
            var resultado = await servico.ListarAsync();

            //Assert
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        private PokemonService ObterPokemonService()
        {
            return new PokemonService(_logger, _httpClientService, _configuracao, _capturadoRepository);
        }
    }
}
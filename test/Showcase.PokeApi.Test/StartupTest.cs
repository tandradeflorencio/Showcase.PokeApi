using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Showcase.PokeApi.Test
{
    [ExcludeFromCodeCoverage]
    public class StartupTest : BaseTest
    {
        [Fact]
        public void ConfigureServices_QuandoExecutar_DeveFinalizarComSucesso()
        {
            //Arrange
            var service = new ServiceCollection();

            //Act
            new Startup(_configuracao).ConfigureServices(service);

            //Assert
            Assert.True(true);
        }
    }
}
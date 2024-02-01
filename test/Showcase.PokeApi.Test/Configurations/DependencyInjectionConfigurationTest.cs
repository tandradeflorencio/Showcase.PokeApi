using Microsoft.Extensions.DependencyInjection;
using Showcase.PokeApi.Configurations;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Showcase.PokeApi.Test.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DependencyInjectionConfigurationTest : BaseTest
    {
        [Fact]
        public void ConfigurarDependencias_QuandoExecutar_DeveFinalizarSemErros()
        {
            //Arrange
            var service = new ServiceCollection();

            //Act
            DependencyInjectionConfiguration.ConfigurarDependencias(service);

            //Assert
            Assert.True(true);
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Showcase.PokeApi.Configurations;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Showcase.PokeApi.Test.Configurations
{
    [ExcludeFromCodeCoverage]
    public class SwaggerConfigurationTest : BaseTest
    {
        [Fact]
        public void ConfigurarSwagger_QuandoExecutar_DeveFinalizarSemErros()
        {
            //Arrange
            var service = Substitute.For<IServiceCollection>();

            //Act
            service.ConfigurarSwagger(_configuracao);

            //Assert
            Assert.True(true);
        }
    }
}
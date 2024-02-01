using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Showcase.PokeApi.Configurations;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Showcase.PokeApi.Test.Configurations
{
    [ExcludeFromCodeCoverage]
    public class LogConfigurationTest : BaseTest
    {
        [Fact]
        public void ConfigurarLogs_QuandoExecutar_DeveFinalizarSemErros()
        {
            //Arrange
            var service = Substitute.For<IServiceCollection>();

            //Act
            LogConfiguration.ConfigurarLogs(service);

            //Assert
            Assert.True(true);
        }
    }
}
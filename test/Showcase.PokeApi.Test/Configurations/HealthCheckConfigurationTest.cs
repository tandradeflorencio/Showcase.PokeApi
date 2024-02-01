using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NSubstitute;
using Showcase.PokeApi.Configurations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Xunit;

namespace Showcase.PokeApi.Test.Configurations
{
    [ExcludeFromCodeCoverage]
    public class HealthCheckConfigurationTest : BaseTest
    {
        [Fact]
        public void ConfigurarHealthChecks_QuandoExecutar_DeveFinalizarSemErros()
        {
            //Arrange
            var service = Substitute.For<IServiceCollection>();

            //Act
            HealthCheckConfiguration.ConfigurarHealthChecks(service, _configuracao);

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void EditarResposta_QuandoExecutar_DeveFinalizarSemErros()
        {
            //Arrange
            var contexto = new DefaultHttpContext();
            contexto.Response.Body = new MemoryStream();
            var entradas = new Dictionary<string, HealthReportEntry> { { "Entrada", new HealthReportEntry(HealthStatus.Degraded, "Entrada", TimeSpan.MaxValue, null, null) } };
            var relatorio = new HealthReport(entradas, TimeSpan.FromMinutes(1));

            //Act
            HealthCheckConfiguration.EditarResposta(contexto, relatorio);

            //Assert
            Assert.True(true);
        }
    }
}
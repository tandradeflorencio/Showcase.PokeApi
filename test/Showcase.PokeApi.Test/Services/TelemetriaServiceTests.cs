using AutoFixture;
using FluentAssertions;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Showcase.PokeApi.Services;
using Showcase.PokeApi.Test.Fakes;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Showcase.PokeApi.Test.Services
{
    [ExcludeFromCodeCoverage]
    public class TelemetriaServiceTests : BaseTest
    {
        [Fact]
        public void AdicionarMetrica_QuandoExecutarComNomeESomaSemErros_DeveAdicionarMetricaComSucesso()
        {
            // Arrange
            var canalDeTelemetria = new CanalDeTelemetriaFake();
            var telemetryConfiguration = new TelemetryConfiguration
            {
                TelemetryChannel = canalDeTelemetria
            };

            var telemetryClient = new TelemetryClient(telemetryConfiguration);
            var telemetriaService = new TelemetriaService(telemetryClient);

            var nome = _fixture.Create<string>();
            var soma = _fixture.Create<double>();

            // Act
            telemetriaService.AdicionarMetrica(nome, soma);

            // Assert
            canalDeTelemetria.MetricasEnviadas.Should().HaveCount(1);
        }

        [Fact]
        public void AdicionarMetrica_QuandoExecutarSemNome_NaoDeveAdicionarMetrica()
        {
            // Arrange
            var canalDeTelemetria = new CanalDeTelemetriaFake();
            var telemetryConfiguration = new TelemetryConfiguration
            {
                TelemetryChannel = canalDeTelemetria
            };

            var telemetryClient = new TelemetryClient(telemetryConfiguration);
            var telemetriaService = new TelemetriaService(telemetryClient);

            var soma = _fixture.Create<double>();

            // Act
            telemetriaService.AdicionarMetrica(null, soma);

            // Assert
            canalDeTelemetria.MetricasEnviadas.Should().HaveCount(0);
        }

        [Fact]
        public void AdicionarMetrica_QuandoExecutarSemSoma_NaoDeveAdicionarMetrica()
        {
            // Arrange
            var canalDeTelemetria = new CanalDeTelemetriaFake();
            var telemetryConfiguration = new TelemetryConfiguration
            {
                TelemetryChannel = canalDeTelemetria
            };

            var telemetryClient = new TelemetryClient(telemetryConfiguration);
            var telemetriaService = new TelemetriaService(telemetryClient);

            var nome = _fixture.Create<string>();

            // Act
            telemetriaService.AdicionarMetrica(nome, 0);

            // Assert
            canalDeTelemetria.MetricasEnviadas.Should().HaveCount(0);
        }

        [Fact]
        public void AdicionarMetrica_QuandoExecutarSemMetricTelemetry_NaoDeveAdicionarMetrica()
        {
            // Arrange
            var canalDeTelemetria = new CanalDeTelemetriaFake();
            var telemetryConfiguration = new TelemetryConfiguration
            {
                TelemetryChannel = canalDeTelemetria
            };

            var telemetryClient = new TelemetryClient(telemetryConfiguration);
            var telemetriaService = new TelemetriaService(telemetryClient);

            // Act
            telemetriaService.AdicionarMetrica(null);

            // Assert
            canalDeTelemetria.MetricasEnviadas.Should().HaveCount(0);
        }
    }
}
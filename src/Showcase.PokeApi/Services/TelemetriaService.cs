using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Showcase.PokeApi.Services.Interfaces;

namespace Showcase.PokeApi.Services
{
    public class TelemetriaService : ITelemetriaService
    {
        private readonly TelemetryClient _telemetryClient;

        public TelemetriaService(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public bool AdicionarMetrica(string nome, double soma = 1)
        {
            if (string.IsNullOrEmpty(nome) || soma <= 0)
                return false;

            var metrica = new MetricTelemetry
            {
                Name = nome,
                Sum = soma
            };

            _telemetryClient.TrackMetric(metrica);

            return true;
        }
    }
}
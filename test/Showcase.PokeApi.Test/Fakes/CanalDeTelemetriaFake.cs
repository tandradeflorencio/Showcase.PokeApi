using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Showcase.PokeApi.Test.Fakes
{
    [ExcludeFromCodeCoverage]
    public class CanalDeTelemetriaFake : ITelemetryChannel
    {
        private readonly ConcurrentBag<ITelemetry> _telemtriasEnviadas = new();

        public bool? DeveloperMode { get; set; }

        public string EndpointAddress { get; set; }

        public IEnumerable<MetricTelemetry> MetricasEnviadas => ObterTelemetrias<MetricTelemetry>();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void Send(ITelemetry item)
        {
            _telemtriasEnviadas.Add(item);
        }

        private IEnumerable<T> ObterTelemetrias<T>() where T : ITelemetry
        {
            return _telemtriasEnviadas
                .Where(t => t is T)
                .Cast<T>();
        }
    }
}
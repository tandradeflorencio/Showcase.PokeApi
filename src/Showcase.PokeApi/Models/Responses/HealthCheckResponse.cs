using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Showcase.PokeApi.Models.Responses
{
    [ExcludeFromCodeCoverage]
    public class HealthCheckResponse
    {
        public string Status { get; set; }

        public string Descricao { get; set; }

        public object Dados { get; set; }

        public IList<HealthCheckResponse> Resultados { get; set; }
    }
}
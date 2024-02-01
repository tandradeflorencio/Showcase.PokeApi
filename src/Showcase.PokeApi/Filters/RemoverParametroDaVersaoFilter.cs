using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Showcase.PokeApi.Filters
{
    public class RemoverParametroDaVersaoFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versao = operation.Parameters.Single(p => p.Name == "version");
            operation.Parameters.Remove(versao);
        }
    }
}
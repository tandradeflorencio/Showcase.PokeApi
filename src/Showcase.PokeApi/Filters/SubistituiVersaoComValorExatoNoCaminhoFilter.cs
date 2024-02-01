using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Showcase.PokeApi.Filters
{
    public class SubistituiVersaoComValorExatoNoCaminhoFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var caminhos = new OpenApiPaths();

            foreach (var caminho in swaggerDoc.Paths)
                caminhos.Add(caminho.Key.Replace("v{version}", swaggerDoc.Info.Version), caminho.Value);

            swaggerDoc.Paths = caminhos;
        }
    }
}
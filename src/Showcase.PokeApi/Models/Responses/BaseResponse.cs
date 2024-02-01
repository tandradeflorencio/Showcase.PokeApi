using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Showcase.PokeApi.Models
{
    [ExcludeFromCodeCoverage]
    public class BaseResponse
    {
        [JsonIgnore]
        public int StatusCode { get; set; }

        public string Mensagem { get; set; }
    }
}
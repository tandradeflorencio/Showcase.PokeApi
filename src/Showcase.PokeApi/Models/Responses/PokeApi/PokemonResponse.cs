using System.Text.Json.Serialization;

namespace Showcase.PokeApi.Models.Responses.PokeApi
{
    public class PokemonResponse
    {
        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
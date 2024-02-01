using System.Text.Json.Serialization;

namespace Showcase.PokeApi.Models.Responses.PokeApi
{
    public class PokemonSpritesResponse
    {
        [JsonPropertyName("front_default")]
        public string PadraoDeFrente { get; set; }
    }
}
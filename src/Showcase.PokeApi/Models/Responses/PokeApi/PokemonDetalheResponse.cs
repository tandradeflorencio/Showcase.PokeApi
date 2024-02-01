using System.Text.Json.Serialization;

namespace Showcase.PokeApi.Models.Responses.PokeApi
{
    public class PokemonDetalheResponse
    {
        [JsonPropertyName("id")]
        public int Identificador { get; set; }

        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("height")]
        public int Altura { get; set; }

        [JsonPropertyName("weight")]
        public int Peso { get; set; }

        [JsonPropertyName("sprites")]
        public PokemonSpritesResponse Imagens { get; set; }
    }
}
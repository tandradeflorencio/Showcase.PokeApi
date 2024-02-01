using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Showcase.PokeApi.Models.Responses.PokeApi
{
    public class ListaDePokemonsResponse
    {
        [JsonPropertyName("count")]
        public int Quantidade { get; set; }

        [JsonPropertyName("results")]
        public List<PokemonResponse> Resultados { get; set; }
    }
}
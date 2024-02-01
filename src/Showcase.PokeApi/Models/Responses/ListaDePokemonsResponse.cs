using System.Collections.Generic;

namespace Showcase.PokeApi.Models.Responses
{
    public class ListaDePokemonsResponse : BaseResponse
    {
        public ListaDePokemonsResponse()
        {
            Pokemons = new List<PokemonResponse>();
        }

        public List<PokemonResponse> Pokemons { get; set; }
    }
}
using Showcase.PokeApi.Models.Responses.PokeApi;

namespace Showcase.PokeApi.Models.Entities
{
    public class Capturado
    {
        public int IdentificadorDoPokemon { get; set; }
        public string NomeDoPokemon { get; set; }

        internal static Capturado Mapear(PokemonDetalheResponse detalhe)
        {
            if (detalhe == null) return null;

            return new Capturado
            {
                IdentificadorDoPokemon = detalhe.Identificador,
                NomeDoPokemon = detalhe.Nome
            };
        }
    }
}
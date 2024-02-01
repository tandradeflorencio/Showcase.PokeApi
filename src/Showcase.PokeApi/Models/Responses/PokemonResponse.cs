using System.Diagnostics.CodeAnalysis;

namespace Showcase.PokeApi.Models.Responses
{
    [ExcludeFromCodeCoverage]
    public class PokemonResponse : BaseResponse
    {
        public int Identificador { get; set; }

        public string Nome { get; set; }

        public int Altura { get; set; }

        public int Peso { get; set; }

        public string ImagemEmBase64 { get; set; }

        internal static PokemonResponse Mapear(PokeApi.PokemonDetalheResponse item)
        {
            if (item == null) return null;

            return new PokemonResponse
            {
                Identificador = item.Identificador,
                Nome = item.Nome,
                Altura = item.Altura,
                Peso = item.Peso,
                ImagemEmBase64 = item?.Imagens?.PadraoDeFrente
            };
        }
    }
}
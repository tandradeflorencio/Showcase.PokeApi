using Showcase.PokeApi.Models.Entities;

namespace Showcase.PokeApi.Models.Responses
{
    public class InserirMestreResponse : BaseResponse
    {
        public int Identificador { get; set; }

        internal static InserirMestreResponse Mapear(Mestre mestre)
        {
            if (mestre == null) return null;

            return new InserirMestreResponse
            {
                Identificador = mestre.Identificador
            };
        }
    }
}
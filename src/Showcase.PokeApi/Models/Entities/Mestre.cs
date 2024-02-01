using Showcase.PokeApi.Models.Requests;

namespace Showcase.PokeApi.Models.Entities
{
    public class Mestre
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string DataDeNascimento { get; set; }

        internal static Mestre Mapear(InserirMestreRequest requisicao)
        {
            if (requisicao == null) return null;

            return new Mestre
            {
                Documento = requisicao.Documento,
                Nome = requisicao.Nome,
                DataDeNascimento = requisicao.DataDeNascimento.ToString("u")
            };
        }
    }
}
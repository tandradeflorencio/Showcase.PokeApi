using System;

namespace Showcase.PokeApi.Models.Requests
{
    public class InserirMestreRequest
    {
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Documento { get; set; }
    }
}

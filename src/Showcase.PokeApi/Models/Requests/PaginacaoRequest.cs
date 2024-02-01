namespace Showcase.PokeApi.Models.Requests
{
    public class PaginacaoRequest
    {
        public int Pagina { get; set; } = 1;

        public int QuantidadeDeRegistros { get; set; } = 10;
    }
}
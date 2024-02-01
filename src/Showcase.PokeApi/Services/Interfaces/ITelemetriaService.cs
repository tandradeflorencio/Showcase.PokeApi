namespace Showcase.PokeApi.Services.Interfaces
{
    public interface ITelemetriaService
    {
        bool AdicionarMetrica(string nome, double soma = 1);
    }
}
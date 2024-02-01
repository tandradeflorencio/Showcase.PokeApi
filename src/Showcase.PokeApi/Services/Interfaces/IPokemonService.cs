using Showcase.PokeApi.Models;
using Showcase.PokeApi.Models.Requests;
using Showcase.PokeApi.Models.Responses;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Services.Interfaces
{
    public interface IPokemonService
    {
        Task<BaseResponse> InserirCapturadoAsync(int identificador);

        Task<ListaDePokemonsResponse> ListarAsync();

        Task<ListaDePokemonsResponse> ListarCapturadosAsync(PaginacaoRequest requisicao);

        Task<BaseResponse> ObterAsync(int identificador);
    }
}
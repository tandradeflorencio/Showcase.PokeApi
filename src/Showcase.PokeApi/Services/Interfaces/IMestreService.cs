using Showcase.PokeApi.Models;
using Showcase.PokeApi.Models.Requests;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Services.Interfaces
{
    public interface IMestreService
    {
        Task<BaseResponse> InserirAsync(InserirMestreRequest requisicao);
    }
}
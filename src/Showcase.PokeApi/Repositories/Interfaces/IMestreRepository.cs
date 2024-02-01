using Showcase.PokeApi.Models.Entities;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Repositories.Interfaces
{
    public interface IMestreRepository
    {
        Task<bool> InicializarTabelaAsync();

        Task<Mestre> InserirAsync(Mestre entidade);

        Task<Mestre> ObterAsync(Mestre entidade);
    }
}
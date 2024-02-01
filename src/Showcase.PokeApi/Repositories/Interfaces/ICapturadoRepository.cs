using Showcase.PokeApi.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Repositories.Interfaces
{
    public interface ICapturadoRepository
    {
        Task<bool> InicializarTabelaAsync();

        Task<int> InserirAsync(Capturado entidade);

        Task<IEnumerable<Capturado>> ListarAsync();
    }
}
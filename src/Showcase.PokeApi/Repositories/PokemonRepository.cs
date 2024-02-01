using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Showcase.PokeApi.Models.Entities;
using Showcase.PokeApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Repositories
{
    [ExcludeFromCodeCoverage]
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IConfiguration _configuracao;

        public PokemonRepository(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public async Task<IEnumerable<Pokemon>> ListarCapturadosAsync(string identificador)
        {
            const string query = @"";

            using var conexaoSql = new SqliteConnection(_configuracao.GetConnectionString("SQLite"));
            await conexaoSql.OpenAsync();

            return await conexaoSql.QueryAsync<Pokemon>(query, new { identificador }, commandTimeout: 90, commandType: CommandType.Text);
        }
    }
}
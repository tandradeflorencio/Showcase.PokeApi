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
    public class CapturadoRepository : ICapturadoRepository
    {
        private readonly IConfiguration _configuracao;

        public CapturadoRepository(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public async Task<bool> InicializarTabelaAsync()
        {
            const string command = @"CREATE TABLE IF NOT EXISTS
                                    Capturado (
                                        IdentificadorDoPokemon INTEGER NOT NULL,
                                        NomeDoPokemon TEXT NOT NULL
                                    );";

            using var conexaoSql = new SqliteConnection(_configuracao.GetConnectionString("SQLite"));
            await conexaoSql.OpenAsync();

            var linhasAfetadas = await conexaoSql.ExecuteAsync(command, commandTimeout: 90, commandType: CommandType.Text);

            return linhasAfetadas > 0;
        }

        public async Task<int> InserirAsync(Capturado entidade)
        {
            const string command = @"INSERT INTO Capturado (IdentificadorDoPokemon, NomeDoPokemon)
                                     VALUES (@IdentificadorDoPokemon, @NomeDoPokemon);";

            using var conexaoSql = new SqliteConnection(_configuracao.GetConnectionString("SQLite"));
            await conexaoSql.OpenAsync();

            return await conexaoSql.ExecuteAsync(command, entidade, commandTimeout: 90, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<Capturado>> ListarAsync()
        {
            const string query = @"SELECT   IdentificadorDoPokemon,
                                            NomeDoPokemon
                                   FROM     Capturado;";

            using var conexaoSql = new SqliteConnection(_configuracao.GetConnectionString("SQLite"));
            await conexaoSql.OpenAsync();

            return await conexaoSql.QueryAsync<Capturado>(query, commandTimeout: 90, commandType: CommandType.Text);
        }
    }
}
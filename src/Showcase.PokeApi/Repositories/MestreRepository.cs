using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Showcase.PokeApi.Models.Entities;
using Showcase.PokeApi.Repositories.Interfaces;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Repositories
{
    [ExcludeFromCodeCoverage]
    public class MestreRepository : IMestreRepository
    {
        private readonly IConfiguration _configuracao;

        public MestreRepository(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public async Task<bool> InicializarTabelaAsync()
        {
            const string command = @"CREATE TABLE IF NOT EXISTS
                                    Mestre (
                                        Identificador INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                        Nome TEXT,
                                        Documento TEXT,
                                        DataDeNascimento TEXT
                                    );";

            using var conexaoSql = new SqliteConnection(_configuracao.GetConnectionString("SQLite"));
            await conexaoSql.OpenAsync();

            var linhasAfetadas = await conexaoSql.ExecuteAsync(command, commandTimeout: 90, commandType: CommandType.Text);

            return linhasAfetadas > 0;
        }

        public async Task<Mestre> InserirAsync(Mestre entidade)
        {
            const string command = @"INSERT INTO Mestre (Nome, Documento, DataDeNascimento)
                                     VALUES (@Nome, @Documento, @DataDeNascimento)
                                     RETURNING Identificador;";

            using var conexaoSql = new SqliteConnection(_configuracao.GetConnectionString("SQLite"));
            await conexaoSql.OpenAsync();

            var identificador = await conexaoSql.ExecuteScalarAsync<int>(command, entidade, commandTimeout: 90, commandType: CommandType.Text);

            entidade.Identificador = identificador;

            return entidade;
        }

        public async Task<Mestre> ObterAsync(Mestre entidade)
        {
            const string query = @"SELECT   Identificador,
                                            Nome,
                                            Documento,
                                            DataDeNascimento
                                   FROM     Mestre
                                   WHERE    Nome = @Nome
                                            AND Documento = @Documento";

            using var conexaoSql = new SqliteConnection(_configuracao.GetConnectionString("SQLite"));
            await conexaoSql.OpenAsync();

            return await conexaoSql.QueryFirstOrDefaultAsync<Mestre>(query, entidade, commandTimeout: 90, commandType: CommandType.Text);
        }
    }
}
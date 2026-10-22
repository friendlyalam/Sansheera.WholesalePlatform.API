using Dapper;
using Npgsql;

namespace Sansheera.WholesalePlatform.Infrastructure.Persistence.Dapper
{
    public sealed class DapperService : IDapper
    {
        private readonly NpgsqlDataSource _dataSource;

        public DapperService(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<T?> QuerySingleOrDefaultAsync<T>(
            string sql,
            object? parameters,
            CancellationToken cancellationToken)
        {
            await using var connection =
                await _dataSource.OpenConnectionAsync(cancellationToken);

            var command = new CommandDefinition(
                commandText: sql,
                parameters: parameters,
                cancellationToken: cancellationToken);

            return await connection.QuerySingleOrDefaultAsync<T>(command);
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(
    string sql,
    object? parameters,
    CancellationToken cancellationToken)
        {
            await using var connection =
                await _dataSource.OpenConnectionAsync(cancellationToken);

            var command = new CommandDefinition(
                commandText: sql,
                parameters: parameters,
                cancellationToken: cancellationToken);

            var result = await connection.QueryAsync<T>(command);

            return result.AsList();
        }
    }
}

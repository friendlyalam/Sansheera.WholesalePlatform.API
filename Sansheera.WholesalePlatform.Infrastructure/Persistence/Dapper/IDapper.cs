namespace Sansheera.WholesalePlatform.Infrastructure.Persistence.Dapper
{
    public interface IDapper
    {
        Task<T?> QuerySingleOrDefaultAsync<T>(
            string sql,
            object? parameters,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<T>> QueryAsync<T>(
    string sql,
    object? parameters,
    CancellationToken cancellationToken);
    }
}

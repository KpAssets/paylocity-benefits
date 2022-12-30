using System.Data;
using Microsoft.Extensions.Options;
using Data.Client.Strategy.Factory;

namespace Data.Client;

internal sealed class DatabaseClient : IDatabaseClient
{
    private readonly IDatabaseClientStrategyFactory _factory;
    private readonly Settings _settings;

    public DatabaseClient(
        IDatabaseClientStrategyFactory factory,
        IOptions<Settings> settings)
    {
        _factory = factory;
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings), "Data.Client.Settings cannot be null!");
    }

    public Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func, string connectionName)
    {
        var context = GetDataContextForConnectionName(connectionName);

        return _factory
            .GetDatabaseClientStrategy(context)
            .WithConnectionAsync(func, context);
    }

    public Task<T> WithTransactionAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> func, string connectionName)
    {
        var context = GetDataContextForConnectionName(connectionName);

        return _factory
            .GetDatabaseClientStrategy(context)
            .WithTransactionAsync(func, context);
    }

    private DataContext GetDataContextForConnectionName(string connectionName) =>
        _settings
            .Connections
            .Single(x => x.Key.ToLower() == connectionName)
            .Value;
}
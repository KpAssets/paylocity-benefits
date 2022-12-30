using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Data.Client;
using Data.Client.Strategy;
using Data.Client.Strategy.Sqlite;
using Data.Client.Strategy.Factory;

namespace Data.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDataClientServices(this IServiceCollection services, IConfigurationSection configurationSection) =>
        services
            .Configure<Data.Client.Settings>(configurationSection)
            .AddDatabaseStrategyFactory()
            .AddDatabaseStrategies()
            .AddDatabaseClient();

    private static IServiceCollection AddDatabaseStrategyFactory(this IServiceCollection services) =>
        services
            .AddSingleton<IDatabaseClientStrategyFactory, DatabaseClientStrategyFactory>();

    private static IServiceCollection AddDatabaseStrategies(this IServiceCollection services) =>
        services
            .AddSingleton<IDatabaseClientStrategy, SqliteDatabaseClientStrategy>();

    private static IServiceCollection AddDatabaseClient(this IServiceCollection services) =>
        services
            .AddSingleton<IDatabaseClient, DatabaseClient>();
}
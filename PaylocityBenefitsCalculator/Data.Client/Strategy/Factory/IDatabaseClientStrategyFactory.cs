namespace Data.Client.Strategy.Factory;

internal interface IDatabaseClientStrategyFactory
{
    IDatabaseClientStrategy GetDatabaseClientStrategy(DataContext context);
}
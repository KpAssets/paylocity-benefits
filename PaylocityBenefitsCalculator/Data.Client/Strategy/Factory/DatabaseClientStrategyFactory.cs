namespace Data.Client.Strategy.Factory;

internal sealed class DatabaseClientStrategyFactory : IDatabaseClientStrategyFactory
{
    private readonly IEnumerable<IDatabaseClientStrategy> _strats;
    public DatabaseClientStrategyFactory(IEnumerable<IDatabaseClientStrategy> strats)
    {
        _strats = strats;
    }

    public IDatabaseClientStrategy GetDatabaseClientStrategy(DataContext context)
    {
        try
        {
            return _strats
                .Single(x =>
                    x
                        ?.GetType()
                        ?.Name
                        ?.StartsWith(
                            context.DatabaseType.ToString())
                        ?? false);
        }
        catch (Exception ex)
        {
            // log!
            throw;
        }
    }
}
namespace Data.Client.Strategy.Factory;

// Since we can have multiple IDatabaseClientStrategy's in use we need an easy way to select the appropriate one
// For a given DataContext we return a IDatabaseClientStrategy that matches DataContext.DatabaseType
internal interface IDatabaseClientStrategyFactory
{
    IDatabaseClientStrategy GetDatabaseClientStrategy(DataContext context);
}
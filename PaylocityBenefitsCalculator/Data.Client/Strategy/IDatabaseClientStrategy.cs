using System.Data;

namespace Data.Client.Strategy;

internal interface IDatabaseClientStrategy
{
    Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func, DataContext context);
    Task<T> WithTransactionAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> func, DataContext context);
}
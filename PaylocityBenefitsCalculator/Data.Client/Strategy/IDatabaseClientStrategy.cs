using System.Data;

namespace Data.Client.Strategy;

// Each IDatabaseClientStrategy is responsible for constructing the IDbConnection for the underlying database
internal interface IDatabaseClientStrategy
{
    Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func, DataContext context);
    Task WithTransactionAsync(Func<IDbConnection, IDbTransaction, Task> func, DataContext context);
}
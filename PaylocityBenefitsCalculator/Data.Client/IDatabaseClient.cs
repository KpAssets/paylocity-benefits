using System.Data;

namespace Data.Client;

public interface IDatabaseClient
{
    Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func, string connectionName);
    Task WithTransactionAsync(Func<IDbConnection, IDbTransaction, Task> func, string connectionName);
}
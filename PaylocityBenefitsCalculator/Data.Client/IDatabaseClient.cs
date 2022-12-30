using System.Data;

namespace Data.Client;

public interface IDatabaseClient
{
    Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func, string connectionName);
    Task<T> WithTransactionAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> func, string connectionName);
}
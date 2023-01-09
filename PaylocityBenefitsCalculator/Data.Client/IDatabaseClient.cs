using System.Data;

namespace Data.Client;

// The IDatabaseClient abstracts away details about how the IDbConnection is created
// Consumers only need to worry about using the IDbConnection to perform their work
public interface IDatabaseClient
{
    Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func, string connectionName);
    Task WithTransactionAsync(Func<IDbConnection, IDbTransaction, Task> func, string connectionName);
}
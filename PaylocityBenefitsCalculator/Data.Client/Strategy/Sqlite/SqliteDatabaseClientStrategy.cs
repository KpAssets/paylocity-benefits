using System.Data;
using Microsoft.Data.Sqlite;

namespace Data.Client.Strategy.Sqlite;

internal sealed class SqliteDatabaseClientStrategy : IDatabaseClientStrategy
{
    public async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func, DataContext context)
    {
        try
        {
            using (var connection = new SqliteConnection(CreateConnectionString(context)))
            {
                connection.Open();

                return await func(connection);
            }
        }
        catch (Exception ex)
        {
            // log!
            throw;
        }
    }

    public async Task<T> WithTransactionAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> func, DataContext context)
    {
        try
        {
            using (var connection = new SqliteConnection(CreateConnectionString(context)))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var result = await func(connection, transaction);

                        transaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        // log!
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // log!
            throw;
        }
    }

    private string CreateConnectionString(DataContext context) =>
        new SqliteConnectionStringBuilder
        {
            DataSource = Path.Combine(context.Path, context.DatabaseName),
            ForeignKeys = true
        }.ToString();
}
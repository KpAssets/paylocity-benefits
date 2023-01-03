using System.Data;
using Microsoft.Data.Sqlite;

namespace Data.Client.Strategy.Sqlite;

internal sealed class SqliteDatabaseClientStrategy : IDatabaseClientStrategy
{
    public async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func, DataContext context)
    {
        using (var connection = new SqliteConnection(CreateConnectionString(context)))
        {
            OpenDatabase(connection);

            try
            {
                return await func(connection);
            }
            catch (Exception ex)
            {
                // log!
                throw;
            }
        }
    }

    public async Task WithTransactionAsync(Func<IDbConnection, IDbTransaction, Task> func, DataContext context)
    {
        using (var connection = new SqliteConnection(CreateConnectionString(context)))
        {
            OpenDatabase(connection);

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    await func(connection, transaction);

                    transaction.Commit();
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

    private string CreateConnectionString(DataContext context) =>
        new SqliteConnectionStringBuilder
        {
            DataSource = Path.Combine(context.Path, context.DatabaseName),
            ForeignKeys = true
        }.ToString();

    private void OpenDatabase(IDbConnection connection)
    {
        try
        {
            connection.Open();
        }
        catch (Exception ex)
        {
            // log!
            throw;
        }
    }
}
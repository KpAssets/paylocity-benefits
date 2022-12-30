using Dapper;
using Business.Employees;
using Data.Client;

namespace Data.Employees;

internal sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly IDatabaseClient _client;

    public EmployeeRepository(IDatabaseClient client)
    {
        _client = client;
    }

    public Task DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task GetAsync(uint id)
    {
        var result = await _client.WithConnectionAsync(
            c => c.QueryFirstOrDefaultAsync<int>("SELECT 1"),
            Constants.BENEFITS_CONNECTION);
    }

    public Task GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpsertAsync()
    {
        throw new NotImplementedException();
    }
}
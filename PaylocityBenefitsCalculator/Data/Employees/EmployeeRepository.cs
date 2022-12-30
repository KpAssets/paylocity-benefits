using AutoMapper;
using Dapper;
using Business.Employees;
using Data.Client;

namespace Data.Employees;

internal sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly IDatabaseClient _client;
    private readonly IMapper _mapper;

    private const string SELECT_EMPLOYEES_BY_ID = @"
        SELECT id, first_name, last_name, date_of_birth, salary
        FROM employees
        WHERE id=@Id;";

    public EmployeeRepository(
        IDatabaseClient client,
        IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public Task DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> GetAsync(uint id)
    {
        var employee = await _client.WithConnectionAsync(
            c => c.QueryFirstOrDefaultAsync<EmployeeEntity>(SELECT_EMPLOYEES_BY_ID, new { Id = id }),
            Constants.BENEFITS_CONNECTION);

        return _mapper.Map<Employee>(employee);
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
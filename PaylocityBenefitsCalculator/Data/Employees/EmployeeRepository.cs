using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using Business.Employees;
using Data.Client;

namespace Data.Employees;

internal sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly IDatabaseClient _client;
    private readonly IMapper _mapper;

    private const string SELECT_ALL_EMPLOYEES = @"
        SELECT id, first_name, last_name, date_of_birth, salary
        FROM employees;
    ";

    private const string SELECT_EMPLOYEES_BY_ID = @"
        SELECT id, first_name, last_name, date_of_birth, salary
        FROM employees
        WHERE id=@Id;
    ";

    private const string DELETE_EMPLOYEE_BY_ID = @"
        DELETE FROM employee WHERE id=@Id;
    ";

    public EmployeeRepository(
        IDatabaseClient client,
        IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public async Task<Employee> DeleteAsync(Employee employee)
    {
        await _client.WithConnectionAsync(
            c => c.DeleteAsync<EmployeeEntity>(_mapper.Map<EmployeeEntity>(employee)),
            Constants.BENEFITS_CONNECTION);

        return employee;
    }

    public async Task<Employee> GetAsync(uint id)
    {
        var employee = await _client.WithConnectionAsync(
            c => c.QuerySingleOrDefaultAsync<EmployeeEntity>(SELECT_EMPLOYEES_BY_ID, new { Id = id }),
            Constants.BENEFITS_CONNECTION);

        return _mapper.Map<Employee>(employee);
    }

    public async Task<IEnumerable<Employee>> GetAsync()
    {
        var employees = await _client.WithConnectionAsync(
            c => c.QueryAsync<EmployeeEntity>(SELECT_ALL_EMPLOYEES),
            Constants.BENEFITS_CONNECTION);

        return _mapper.Map<IEnumerable<Employee>>(employees);
    }

    public Task<Employee> UpsertAsync(Employee employee)
    {
        if (employee.Id <= 0)
            return InsertAsync(employee);

        return UpdateAsync(employee);
    }

    private async Task<Employee> InsertAsync(Employee employee)
    {
        var insertedId = await _client.WithConnectionAsync(
                c => c.InsertAsync<EmployeeEntity>(_mapper.Map<EmployeeEntity>(employee)),
                Constants.BENEFITS_CONNECTION);

        employee.Id = (uint)insertedId;

        return employee;
    }

    private async Task<Employee> UpdateAsync(Employee employee)
    {
        await _client.WithConnectionAsync(
                c => c.UpdateAsync<EmployeeEntity>(_mapper.Map<EmployeeEntity>(employee)),
                Constants.BENEFITS_CONNECTION);

        return employee;
    }
}
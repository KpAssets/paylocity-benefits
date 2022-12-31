using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using Business.Employees;
using Data.Client;
using Data.Dependents;

namespace Data.Employees;

internal sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly IDatabaseClient _client;
    private readonly IMapper _mapper;

    #region Queries
    private const string SELECT_ALL_EMPLOYEES = @"
        SELECT
            ee.id,
            ee.first_name,
            ee.last_name,
            ee.date_of_birth,
            ee.salary,
            dep.id,
            dep.first_name,
            dep.last_name,
            dep.date_of_birth,
            dep.relationship
        FROM employees AS ee
        LEFT JOIN dependents AS dep ON dep.employees_id = ee.id;
    ";

    private const string SELECT_EMPLOYEES_BY_ID = @"
        SELECT
            ee.id,
            ee.first_name,
            ee.last_name,
            ee.date_of_birth,
            ee.salary,
            dep.id,
            dep.first_name,
            dep.last_name,
            dep.date_of_birth,
            dep.relationship
        FROM employees AS ee
        LEFT JOIN dependents AS dep ON dep.employees_id = ee.id
        WHERE ee.id=@Id;
    ";

    private const string DELETE_EMPLOYEE_BY_ID = @"
        DELETE FROM employee WHERE id=@Id;
    ";
    #endregion Queries

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
        var lookup = new Dictionary<uint, EmployeeEntity>();

        await _client.WithConnectionAsync(
            c => c.QueryAsync<EmployeeEntity, DependentEntity, EmployeeEntity>(
                SELECT_EMPLOYEES_BY_ID,
                (ee, dep) => MapDependentsToEmployee(lookup, ee, dep),
                new { Id = id }),
            Constants.BENEFITS_CONNECTION);

        return _mapper.Map<Employee>(lookup.Values.SingleOrDefault());
    }

    public async Task<IEnumerable<Employee>> GetAsync()
    {
        var lookup = new Dictionary<uint, EmployeeEntity>();

        await _client.WithConnectionAsync(
            c => c.QueryAsync<EmployeeEntity, DependentEntity, EmployeeEntity>(
                SELECT_ALL_EMPLOYEES,
                (ee, dep) => MapDependentsToEmployee(lookup, ee, dep)),
            Constants.BENEFITS_CONNECTION);

        return _mapper.Map<IEnumerable<Employee>>(lookup.Values);
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

    private EmployeeEntity MapDependentsToEmployee(
        Dictionary<uint, EmployeeEntity> lookup,
        EmployeeEntity ee,
        DependentEntity dep)
    {
        if (!lookup.TryGetValue(ee.id, out EmployeeEntity employee))
        {
            employee = ee;
            lookup.Add(employee.id, employee);
        }

        if (dep != null)
        {
            dep.employees_id = ee.id;
            dep.employee = ee;
            employee.dependents.Add(dep);
        }

        return employee;
    }
}
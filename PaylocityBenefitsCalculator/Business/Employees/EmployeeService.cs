using Business.Dependents;

namespace Business.Employees;

internal sealed class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repo;
    private readonly IDependentValidatorService _dependentValidator;

    public EmployeeService(
        IEmployeeRepository repo,
        IDependentValidatorService dependentValidator)
    {
        _repo = repo;
        _dependentValidator = dependentValidator;
    }

    public async Task<Employee> DeleteAsync(uint id)
    {
        var employee = await _repo.GetAsync(id);

        return await _repo.DeleteAsync(employee);
    }

    public Task<Employee> GetAsync(uint id) => _repo.GetAsync(id);

    public Task<IEnumerable<Employee>> GetAsync() => _repo.GetAsync();

    public Task<Employee> UpsertAsync(Employee employee)
    {
        _dependentValidator.ValidateDependents(employee.Dependents);
        return _repo.UpsertAsync(employee);
    }
}
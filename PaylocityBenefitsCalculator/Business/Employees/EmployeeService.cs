namespace Business.Employees;

internal sealed class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repo;

    public EmployeeService(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public async Task<Employee> DeleteAsync(uint id)
    {
        var employee = await _repo.GetAsync(id);

        return await _repo.DeleteAsync(employee);
    }

    public Task<Employee> GetAsync(uint id) => _repo.GetAsync(id);

    public Task<IEnumerable<Employee>> GetAsync() => _repo.GetAsync();

    public Task<Employee> UpsertAsync(Employee employee) => _repo.UpsertAsync(employee);
}
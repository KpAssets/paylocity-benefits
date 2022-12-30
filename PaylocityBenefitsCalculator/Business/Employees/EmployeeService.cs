namespace Business.Employees;

internal sealed class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repo;

    public EmployeeService(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public Task DeleteAsync() => _repo.DeleteAsync();

    public Task GetAsync(uint id) => _repo.GetAsync(id);

    public Task GetAsync() => _repo.GetAsync();

    public Task UpsertAsync() => _repo.UpsertAsync();
}
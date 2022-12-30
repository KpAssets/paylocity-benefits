namespace Business.Employees;

public interface IEmployeeRepository
{
    Task<Employee> GetAsync(uint id);
    Task GetAsync();
    Task UpsertAsync();
    Task DeleteAsync();
}
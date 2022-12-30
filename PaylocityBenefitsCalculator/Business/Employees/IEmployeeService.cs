namespace Business.Employees;

public interface IEmployeeService
{
    Task<Employee> GetAsync(uint id);
    Task GetAsync();
    Task UpsertAsync();
    Task DeleteAsync();
}
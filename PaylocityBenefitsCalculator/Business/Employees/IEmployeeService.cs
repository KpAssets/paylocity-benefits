namespace Business.Employees;

public interface IEmployeeService
{
    Task GetAsync(uint id);
    Task GetAsync();
    Task UpsertAsync();
    Task DeleteAsync();
}
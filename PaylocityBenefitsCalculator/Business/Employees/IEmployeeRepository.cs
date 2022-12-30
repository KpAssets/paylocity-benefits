namespace Business.Employees;

public interface IEmployeeRepository
{
    Task GetAsync(uint id);
    Task GetAsync();
    Task UpsertAsync();
    Task DeleteAsync();
}
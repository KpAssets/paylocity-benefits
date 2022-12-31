namespace Business.Employees;

public interface IEmployeeService
{
    Task<Employee> GetAsync(uint id);
    Task<IEnumerable<Employee>> GetAsync();
    Task<Employee> UpsertAsync(Employee employee);
    Task<Employee> DeleteAsync(uint id);
}
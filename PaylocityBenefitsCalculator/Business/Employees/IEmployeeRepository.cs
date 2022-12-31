namespace Business.Employees;

public interface IEmployeeRepository
{
    Task<Employee> GetAsync(uint id);
    Task<IEnumerable<Employee>> GetAsync();
    Task<Employee> UpsertAsync(Employee employee);
    Task<Employee> DeleteAsync(Employee employee);
}
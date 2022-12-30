using Business.Dependents;

namespace Business.Employees;

public class Employee : Person
{
    public uint Id { get; set; }
    public decimal Salary { get; set; }
    public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();
}
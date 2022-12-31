using Business.Employees;

namespace Business.Dependents;

public class Dependent : Person
{
    public Relationship Relationship { get; set; }
    public uint EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
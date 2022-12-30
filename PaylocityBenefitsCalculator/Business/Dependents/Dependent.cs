using Business.Employees;

namespace Business.Dependents;

public class Dependent : Person
{
    public uint Id { get; set; }
    public Relationship Relationship { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
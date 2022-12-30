using Api.Models.Dependents;

namespace Api.Models.Employees;

public class AddEmployeeDto : PersonDto
{
    public decimal Salary { get; set; }
    public ICollection<AddDependentDto>? Dependents { get; set; }
}
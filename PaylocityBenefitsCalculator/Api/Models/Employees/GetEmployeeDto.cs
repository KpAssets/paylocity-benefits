using Api.Models.Dependents;

namespace Api.Models.Employees;

public class GetEmployeeDto : PersonDto
{
    public int Id { get; set; }
    public decimal Salary { get; set; }
    public ICollection<GetDependentDto> Dependents { get; set; } = new List<GetDependentDto>();
}
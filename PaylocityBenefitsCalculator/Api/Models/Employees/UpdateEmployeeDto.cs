namespace Api.Models.Employees;

public class UpdateEmployeeDto : PersonDto
{
    public decimal Salary { get; set; }
}
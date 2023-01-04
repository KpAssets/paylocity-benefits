using Api.Models.Dependents;
using Api.Models.Validations;

namespace Api.Models.Employees;

public class AddEmployeeDto : PersonDto
{
    public decimal Salary { get; set; }
    public ICollection<AddDependentDto>? Dependents { get; set; }

    protected override void GatherInvalidReasons()
    {
        Salary.GreaterThan(InvalidReasons, 0);
        if (Dependents?.Any(x => x != null) ?? false)
        {
            foreach (var dep in Dependents)
                InvalidReasons.AddRange(dep.Validate().InvalidReasons);
        }

        base.GatherInvalidReasons();
    }
}
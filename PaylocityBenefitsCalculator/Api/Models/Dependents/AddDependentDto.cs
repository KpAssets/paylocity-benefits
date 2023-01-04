using Api.Models.Validations;
using Business.Dependents;

namespace Api.Models.Dependents;

public class AddDependentDto : PersonDto
{
    public string Relationship { get; set; } = "none";

    public override ValidationResult Validate()
    {
        var invalidReasons = new List<string>();

        Relationship.NonNullNonEmpty(invalidReasons);
        Relationship.IsEnum<Relationship>(invalidReasons);

        invalidReasons.AddRange(base.Validate().InvalidReasons);

        return new ValidationResult(invalidReasons);
    }
}
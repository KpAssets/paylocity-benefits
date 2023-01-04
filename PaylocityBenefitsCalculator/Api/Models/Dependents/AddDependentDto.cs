using Api.Models.Validations;
using Business.Dependents;

namespace Api.Models.Dependents;

public class AddDependentDto : PersonDto
{
    public string Relationship { get; set; } = "none";

    protected override void GatherInvalidReasons()
    {
        Relationship.NonNullNonEmpty(InvalidReasons);
        Relationship.IsEnum<Relationship>(InvalidReasons);
        base.GatherInvalidReasons();
    }
}
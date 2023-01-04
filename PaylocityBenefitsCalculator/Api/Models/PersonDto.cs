using Api.Models.Validations;

namespace Api.Models;

public abstract class PersonDto : IValidatable
{
    private const uint NameCharacterLimit = 100;
    public uint Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public virtual ValidationResult Validate()
    {
        var invalidReasons = new List<string>();

        FirstName.NonNullNonEmpty(invalidReasons);
        FirstName.LessThanOrEqual(invalidReasons, NameCharacterLimit);
        LastName.NonNullNonEmpty(invalidReasons);
        LastName.LessThanOrEqual(invalidReasons, NameCharacterLimit);

        return new ValidationResult(invalidReasons);
    }
}
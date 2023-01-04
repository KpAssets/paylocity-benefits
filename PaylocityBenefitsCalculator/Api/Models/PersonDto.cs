using Api.Models.Validations;

namespace Api.Models;

public abstract class PersonDto : AbstractValidatable
{
    private const uint NameCharacterLimit = 100;
    public uint Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    protected override void GatherInvalidReasons()
    {
        FirstName.NonNullNonEmpty(InvalidReasons);
        FirstName.LessThanOrEqual(InvalidReasons, NameCharacterLimit);
        LastName.NonNullNonEmpty(InvalidReasons);
        LastName.LessThanOrEqual(InvalidReasons, NameCharacterLimit);
    }
}
namespace Api.Models.Validations;

public abstract class AbstractValidatable : IValidatable
{
    protected List<string> InvalidReasons { get; } = new List<string>();

    protected abstract void GatherInvalidReasons();

    public ValidationResult Validate()
    {
        GatherInvalidReasons();
        return new ValidationResult(InvalidReasons);
    }
}
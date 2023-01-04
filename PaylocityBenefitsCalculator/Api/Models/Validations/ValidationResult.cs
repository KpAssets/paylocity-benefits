namespace Api.Models.Validations;

public sealed class ValidationResult
{
    public ValidationResult(List<string>? invalidReasons = null)
    {
        InvalidReasons = invalidReasons
            ?.Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList()
            ?? new List<string>();
    }

    public bool IsInvalid
    {
        get => InvalidReasons.Any();
    }

    public List<string> InvalidReasons { get; private set; }
}
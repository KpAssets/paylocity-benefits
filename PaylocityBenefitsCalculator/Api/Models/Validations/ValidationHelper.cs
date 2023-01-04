namespace Api.Models.Validations;

internal static class ValidationHelper
{
    public static void Validate(IValidatable validatable)
    {
        var validationResult = validatable?.Validate() ?? new ValidationResult(new List<string> { "Null IValidatable passed to ValidationHelper.Validate" });

        if (validationResult.IsInvalid)
        {
            throw new InvalidDataException(string.Join(",", validationResult.InvalidReasons));
        }
    }
}
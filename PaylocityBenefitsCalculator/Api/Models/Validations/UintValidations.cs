namespace Api.Models.Validations;

internal static class UintValidations
{
    public static bool GreaterThan(
        this uint input,
        List<string> invalidReasons,
        uint floor,
        [System.Runtime.CompilerServices.CallerArgumentExpression("input")] string parameterName = "")
    {
        invalidReasons ??= new List<string>();

        if (input <= floor)
        {
            invalidReasons.Add($"{parameterName} must be greater than {floor}");
            return false;
        }

        return true;
    }
}
namespace Api.Models.Validations;

internal static class DecimalValidations
{
    public static bool GreaterThan(
        this decimal input,
        List<string> invalidReasons,
        decimal floor,
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
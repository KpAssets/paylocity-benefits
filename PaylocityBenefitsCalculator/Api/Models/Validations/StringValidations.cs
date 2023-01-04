namespace Api.Models.Validations;

internal static class StringValidations
{
    public static bool NonNullNonEmpty(
        this string input,
        List<string> invalidReasons,
        [System.Runtime.CompilerServices.CallerArgumentExpression("input")] string parameterName = "")
    {
        invalidReasons ??= new List<string>();

        if (string.IsNullOrWhiteSpace(input))
        {
            invalidReasons.Add($"Missing {parameterName}");
            return false;
        }

        return true;
    }

    public static bool LessThanOrEqual(
        this string input,
        List<string> invalidReasons,
        uint ceiling,
        [System.Runtime.CompilerServices.CallerArgumentExpression("input")] string parameterName = "")
    {
        invalidReasons ??= new List<string>();

        if ((input?.Length ?? 0) > ceiling)
        {
            invalidReasons.Add($"{parameterName} cannot be greater than {ceiling}");
            return false;
        }

        return true;
    }

    public static bool IsEnum<T>(
        this string input,
        List<string> invalidReasons,
        [System.Runtime.CompilerServices.CallerArgumentExpression("input")] string parameterName = "") where T : struct
    {
        if (!Enum.TryParse<T>(input ?? string.Empty, true, out T _))
        {
            invalidReasons.Add($"'{input}' is not a valid {parameterName}");
            return false;
        }

        return true;
    }
}
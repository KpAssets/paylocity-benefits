using Business.Payroll.Deductions;

namespace Tests.Business.Payroll.Deductions;

internal static class DeductionTestHelpers
{
    public static void AssertEqual(Deduction expected, Deduction actual)
    {
        Assert.Equal(expected.Amount, actual.Amount);
        Assert.Equal(expected.Description, actual.Description);
    }
}
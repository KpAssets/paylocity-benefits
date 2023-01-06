using Business.Payroll.Deductions;

namespace Tests.Business.Payroll.Deductions;

internal static class DeductionTestHelpers
{
    public static void AssertEqual(IEnumerable<Deduction> expectedDeductions, IEnumerable<Deduction> actualDeductions)
    {
        Assert.Equal(expectedDeductions.Count(), actualDeductions.Count());

        foreach (var expectedDeduction in expectedDeductions)
        {
            var actualDeduction = actualDeductions.FirstOrDefault(x => x.Description == expectedDeduction.Description);

            Assert.NotNull(actualDeduction);
            AssertEqual(expectedDeduction, actualDeduction);
        }
    }

    public static void AssertEqual(Deduction expected, Deduction actual)
    {
        Assert.Equal(expected.Amount, actual.Amount);
        Assert.Equal(expected.Description, actual.Description);
    }
}
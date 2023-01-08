using Microsoft.Extensions.Options;
using Business;
using Business.Employees;
using Business.Payroll;
using Business.Payroll.Deductions;

namespace Tests.Business.Payroll.Deductions;

public class BaseDeductionCalculationStepTests
{
    [Fact]
    public async Task CanCalculateBaseDeductionTests()
    {
        var target = BuildTestTarget();

        var actual = await target.Process(new Paycheck(new Employee()));

        DeductionTestHelpers.AssertEqual(
            new Deduction
            {
                Amount = 461.54m,
                Description = "Base benefits deduction"
            },
            actual.Deductions.FirstOrDefault());
    }

    private IDeductionCalculationStep BuildTestTarget() =>
        new BaseDeductionCalculationStep(Options.Create(new Settings()));
}
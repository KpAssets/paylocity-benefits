using Business.Employees;
using Business.Payroll;
using Business.Payroll.Deductions;

namespace Tests.Business.Payroll.Deductions;

public class HighlyCompensatedEmployeeDeductionCalculationStepTests
{
    [
        Theory,
        ClassData(typeof(CanCalculateHighlyCompensatedEmployeeDeductionTheoryData))
    ]
    public async Task CanCalculateHighlyCompensatedEmployeeDeductionTests(Paycheck check, Deduction expectedDeduction)
    {
        var target = BuildTestTarget();

        var actual = await target.Process(check);

        if (expectedDeduction == null)
            Assert.Null(actual.Deductions.FirstOrDefault());
        else
            DeductionTestHelpers.AssertEqual(expectedDeduction, actual.Deductions.FirstOrDefault());
    }

    private IDeductionCalculationStep BuildTestTarget() =>
        new HighlyCompensatedEmployeeDeductionCalculationStep();

    #region Theories
    private class CanCalculateHighlyCompensatedEmployeeDeductionTheoryData : TheoryData<Paycheck, Deduction>
    {
        public CanCalculateHighlyCompensatedEmployeeDeductionTheoryData()
        {
            Add(
                new Paycheck(new Employee()),
                null);
            Add(
                new Paycheck(new Employee { Salary = 10000 }),
                null);
            Add(
                new Paycheck(new Employee { Salary = 80000 }),
                null);
            Add(
                new Paycheck(new Employee { Salary = 100000 }),
                new Deduction
                {
                    Amount = 76.92m,
                    Description = "Highly compensated employee deduction"
                });
            Add(
                new Paycheck(new Employee { Salary = 123456.78m }),
                new Deduction
                {
                    Amount = 94.97m,
                    Description = "Highly compensated employee deduction"
                });
        }
    }
    #endregion Theories
}
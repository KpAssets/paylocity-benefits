using Business.Employees;
using Business.Payroll;
using Business.Payroll.Earnings;

namespace Tests.Business.Payroll.Earnings;

public class SalaryCalculationStepTests
{
    [
        Theory,
        ClassData(typeof(CanCalculateSalaryEarningTheoryData))
    ]
    public async Task CanCalculateSalaryEarning(Paycheck check, Earning expectedEarning)
    {
        var target = BuildTestTarget();

        var actual = await target.Process(check);

        var actualEarning = actual.Earnings.FirstOrDefault();

        Assert.Equal(expectedEarning.Amount, actualEarning.Amount);
        Assert.Equal(expectedEarning.Description, actualEarning.Description);
    }

    private IEarningCalculationStep BuildTestTarget() =>
        new SalaryCalculationStep();

    #region Theories
    private class CanCalculateSalaryEarningTheoryData : TheoryData<Paycheck, Earning>
    {
        private const string EXPECTED_EARNING_DESCRIPTION = "Salary";

        public CanCalculateSalaryEarningTheoryData()
        {
            Add(
                new Paycheck(new Employee
                {
                    Salary = 50000
                }),
                new Earning
                {
                    Amount = 1923.08m,
                    Description = EXPECTED_EARNING_DESCRIPTION
                });
            Add(
                new Paycheck(new Employee
                {
                    Salary = 150000
                }),
                new Earning
                {
                    Amount = 5769.23m,
                    Description = EXPECTED_EARNING_DESCRIPTION
                });
            Add(
                new Paycheck(new Employee
                {
                    Salary = 123456.78m
                }),
                new Earning
                {
                    Amount = 4748.34m,
                    Description = EXPECTED_EARNING_DESCRIPTION
                });
        }
    }
    #endregion Theories
}
namespace Business.Calculations.Payroll.Earnings;

// SalaryCalculationStep is responsible for generating the Employee's
// base salary for the pay period
internal sealed class SalaryCalculationStep : IEarningCalculationStep
{
    public Task<Check> Process(Check check)
    {
        try
        {
            check.Earnings.Add(
                BuildSalaryEarningForPayPeriod(check.Employee.Salary));
        }
        catch (Exception ex)
        {
            // log!
            throw;
        }

        return Task.FromResult(check);
    }

    private Earning BuildSalaryEarningForPayPeriod(decimal salary) =>
        new Earning
        {
            Amount = salary / 26,
            Description = "Salary"
        };
}
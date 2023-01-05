namespace Business.Payroll.Earnings;

// SalaryCalculationStep is responsible for generating the Employee's
// base salary for the pay period
internal sealed class SalaryCalculationStep : IEarningCalculationStep
{
    public Task<Paycheck> Process(Paycheck check)
    {
        try
        {
            check.Earnings.Add(
                BuildSalaryEarningForPayPeriod(check.Employee.Salary));
        }
        catch (Exception)
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
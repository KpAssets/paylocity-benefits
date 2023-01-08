using Microsoft.Extensions.Options;

namespace Business.Payroll.Earnings;

// SalaryCalculationStep is responsible for generating the Employee's
// base salary for the pay period
internal sealed class SalaryCalculationStep : IEarningCalculationStep
{
    private readonly Settings _settings;

    public SalaryCalculationStep(IOptions<Settings> settings)
    {
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
    }

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
            Amount = salary / _settings.PayPeriods,
            Description = "Salary"
        };
}
using Microsoft.Extensions.Options;

namespace Business.Payroll.Deductions;

internal sealed class HighlyCompensatedEmployeeDeductionCalculationStep : IDeductionCalculationStep
{
    private readonly Settings _settings;

    public HighlyCompensatedEmployeeDeductionCalculationStep(IOptions<Settings> settings)
    {
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public Task<Paycheck> Process(Paycheck check)
    {
        try
        {
            if (EmployeesSalaryGreaterThanSalaryThreshold(check.Employee.Salary))
            {
                check.Deductions.Add(
                    BuildHighlyCompensatedEmployeeDeduction(check.Employee.Salary));
            }
        }
        catch (Exception)
        {
            // log!
            throw;
        }

        return Task.FromResult(check);
    }

    private bool EmployeesSalaryGreaterThanSalaryThreshold(decimal salary) =>
        salary > _settings.HighlyCompensatedEmployeeSalaryThreshold;

    private Deduction BuildHighlyCompensatedEmployeeDeduction(decimal salary) =>
        new Deduction
        {
            Amount = (salary * _settings.HighlyCompensatedEmployeeDeductionPercentage) / _settings.PayPeriods,
            Description = "Highly compensated employee deduction"
        };
}
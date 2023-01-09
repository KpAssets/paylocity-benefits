using Microsoft.Extensions.Options;

namespace Business.Payroll.Deductions;

internal sealed class BaseDeductionCalculationStep : IDeductionCalculationStep
{
    private readonly Settings _settings;

    public BaseDeductionCalculationStep(IOptions<Settings> settings)
    {
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public Task<Paycheck> Process(Paycheck check)
    {
        try
        {
            check.Deductions.Add(BuildBaseDeductionForPayPeriod());
        }
        catch (Exception)
        {
            // log!
            throw;
        }

        return Task.FromResult(check);
    }

    private Deduction BuildBaseDeductionForPayPeriod() =>
        new Deduction
        {
            Amount = _settings.YearlyBaseBenefitCost / _settings.PayPeriods,
            Description = "Base benefits deduction"
        };
}

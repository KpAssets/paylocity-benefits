namespace Business.Calculations.Payroll.Deductions;

internal sealed class BaseDeductionCalculationStep : IDeductionCalculationStep
{
    private const decimal YEARLY_BASE_BENEFIT_COST = 12000;

    public Task<Check> Process(Check check)
    {
        try
        {
            check.Deductions.Add(BuildBaseDeductionForPayPeriod());
        }
        catch (Exception ex)
        {
            // log!
            throw;
        }

        return Task.FromResult(check);
    }

    private Deduction BuildBaseDeductionForPayPeriod() =>
        new Deduction
        {
            Amount = YEARLY_BASE_BENEFIT_COST / 26,
            Description = "Base benefits deduction"
        };
}

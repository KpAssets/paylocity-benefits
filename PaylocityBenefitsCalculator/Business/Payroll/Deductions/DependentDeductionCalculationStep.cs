using Business.Dependents;

namespace Business.Payroll.Deductions;

internal sealed class DependentDeductionCalculationStep : IDeductionCalculationStep
{
    private const decimal YEARLY_BENEFIT_COST_PER_DEPENDENT = 7200;

    public Task<Paycheck> Process(Paycheck check)
    {
        try
        {
            if (check.Employee.Dependents?.Any(x => x != null) ?? false)
            {
                check.Deductions.AddRange(
                    check.Employee.Dependents
                        .Select(
                            x => BuildDependentDeductionForPayPeriod(x)));
            }
        }
        catch (Exception)
        {
            // log!
            throw;
        }

        return Task.FromResult(check);
    }

    private Deduction BuildDependentDeductionForPayPeriod(Dependent dependent) =>
        new Deduction
        {
            Amount = YEARLY_BENEFIT_COST_PER_DEPENDENT / 26,
            Description = $"Dependent deduction for {dependent.FirstName} {dependent.LastName}"
        };
}
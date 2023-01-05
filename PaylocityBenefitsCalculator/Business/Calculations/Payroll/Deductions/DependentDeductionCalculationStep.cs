using Business.Dependents;

namespace Business.Calculations.Payroll.Deductions;

internal sealed class DependentDeductionCalculationStep : IDeductionCalculationStep
{
    private const decimal YEARLY_BENEFIT_COST_PER_DEPENDENT = 7200;

    public Task<Check> Process(Check check)
    {
        try
        {
            if (check.Employee.Dependents.Any(x => x != null))
            {
                check.Deductions.AddRange(
                    check.Employee.Dependents
                        .Select(
                            x => BuildDependentDeductionForPayPeriod(x)));
            }
        }
        catch (Exception ex)
        {
            // log!
            throw;
        }

        return Task.FromResult(check);
    }

    private Deduction BuildDependentDeductionForPayPeriod(Dependent dependent) =>
        new Deduction
        {
            Amount = 7200 / 26,
            Description = $"Dependent deduction for {dependent.FirstName} {dependent.LastName}"
        };
}
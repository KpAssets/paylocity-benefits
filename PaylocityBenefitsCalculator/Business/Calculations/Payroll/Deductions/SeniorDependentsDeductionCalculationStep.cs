using Business.Dependents;

namespace Business.Calculations.Payroll.Deductions;

internal sealed class SeniorDependentsDeductionCalculationStep : IDeductionCalculationStep
{
    private const decimal YEARLY_BENEFIT_COST_PER_SENIOR_DEPENDENT = 2400;

    public Task<Check> Process(Check check)
    {
        try
        {
            check.Deductions.AddRange(
                check.Employee.Dependents
                .Where(DependentIsAtLeastFiftyYearsOldPredicate)
                .Select(BuildSeniorDependentDeductionForPayPeriod));
        }
        catch (Exception ex)
        {
            // log!
            throw;
        }

        return Task.FromResult(check);
    }

    private bool DependentIsAtLeastFiftyYearsOldPredicate(Dependent dependent) =>
        (DateTime.Now.Year - dependent.DateOfBirth.Year) >= 50;

    private Deduction BuildSeniorDependentDeductionForPayPeriod(Dependent dependent) =>
        new Deduction
        {
            Amount = YEARLY_BENEFIT_COST_PER_SENIOR_DEPENDENT / 26,
            Description = $"Senior dependent deduction for {dependent.FirstName} {dependent.LastName}"
        };
}
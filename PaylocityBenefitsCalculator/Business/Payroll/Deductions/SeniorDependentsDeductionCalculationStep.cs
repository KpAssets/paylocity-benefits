using Business.Dependents;

namespace Business.Payroll.Deductions;

internal sealed class SeniorDependentsDeductionCalculationStep : IDeductionCalculationStep
{
    private const decimal YEARLY_BENEFIT_COST_PER_SENIOR_DEPENDENT = 2400;

    public Task<Paycheck> Process(Paycheck check)
    {
        try
        {
            check.Deductions.AddRange(
                check.Employee.Dependents
                .Where(DependentIsAtLeastFiftyYearsOldPredicate)
                .Select(BuildSeniorDependentDeductionForPayPeriod));
        }
        catch (Exception)
        {
            // log!
            throw;
        }

        return Task.FromResult(check);
    }

    private bool DependentIsAtLeastFiftyYearsOldPredicate(Dependent dependent) =>
        AgeInYears(dependent.DateOfBirth) >= 50;

    private Deduction BuildSeniorDependentDeductionForPayPeriod(Dependent dependent) =>
        new Deduction
        {
            Amount = YEARLY_BENEFIT_COST_PER_SENIOR_DEPENDENT / 26,
            Description = $"Senior dependent deduction for {dependent.FirstName} {dependent.LastName}"
        };

    private int AgeInYears(DateTime dateOfBirth)
    {
        var now = DateTime.Today;
        int age = now.Year - dateOfBirth.Year;
        if (dateOfBirth.AddYears(age) > now)
            age--;
        return age;
    }
}
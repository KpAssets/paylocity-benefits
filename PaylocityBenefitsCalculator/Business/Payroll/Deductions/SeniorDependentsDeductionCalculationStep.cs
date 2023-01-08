using Microsoft.Extensions.Options;
using Business.Dependents;

namespace Business.Payroll.Deductions;

internal sealed class SeniorDependentsDeductionCalculationStep : IDeductionCalculationStep
{
    private readonly Settings _settings;

    public SeniorDependentsDeductionCalculationStep(IOptions<Settings> settings)
    {
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public Task<Paycheck> Process(Paycheck check)
    {
        try
        {
            check.Deductions.AddRange(
                check.Employee.Dependents
                    ?.Where(DependentIsAtLeastFiftyYearsOldPredicate)
                    .Select(BuildSeniorDependentDeductionForPayPeriod)
                ?? new List<Deduction>());
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
            Amount = _settings.YearlyBenefitCostPerSeniorDependent / _settings.PayPeriods,
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
namespace Business.Dependents;

internal interface IDependentValidatorService
{
    void ValidateDependents(IEnumerable<Dependent> dependents);
}
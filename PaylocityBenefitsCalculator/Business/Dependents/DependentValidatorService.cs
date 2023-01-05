namespace Business.Dependents;

internal sealed class DependentValidatorService : IDependentValidatorService
{
    public void ValidateDependents(IEnumerable<Dependent> dependents)
    {
        // we have nothing to validate if there are no non-null dependents
        if (!dependents?.Any(x => x != null) ?? true) return;

        if (dependents.Where(DependentIsSpouseOrDomesticPartner).Count() > 1)
        {
            throw new InvalidDataException("An employee can only have one Spouse or Domestic Partner dependent");
        }
    }

    public bool DependentIsSpouseOrDomesticPartner(Dependent dependent) =>
        dependent.Relationship == Relationship.Spouse || dependent.Relationship == Relationship.DomesticPartner;
}
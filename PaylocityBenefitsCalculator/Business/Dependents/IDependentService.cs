namespace Business.Dependents;

public interface IDependentService
{
    Task<Dependent> GetAsync(uint id);
}
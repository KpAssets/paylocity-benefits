namespace Business.Dependents;

public interface IDependentService
{
    Task<Dependent> GetAsync(uint id);
    Task<IEnumerable<Dependent>> GetAsync();
    Task<Dependent> UpsertAsync(Dependent dependent);
    Task<Dependent> DeleteAsync(uint id);
}
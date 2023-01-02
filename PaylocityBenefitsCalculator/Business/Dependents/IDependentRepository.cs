namespace Business.Dependents;

public interface IDependentRepository
{
    Task<Dependent> GetAsync(uint id);
    Task<IEnumerable<Dependent>> GetAsync();
    Task<Dependent> UpsertAsync(Dependent dependent);
    Task<Dependent> DeleteAsync(Dependent dependent);
}
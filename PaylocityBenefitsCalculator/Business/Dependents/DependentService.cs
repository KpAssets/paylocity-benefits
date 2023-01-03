namespace Business.Dependents;

internal sealed class DependentService : IDependentService
{
    private readonly IDependentRepository _repo;

    public DependentService(IDependentRepository repo)
    {
        _repo = repo;
    }

    public async Task<Dependent> DeleteAsync(uint id)
    {
        var dependent = await _repo.GetAsync(id);

        return await _repo.DeleteAsync(dependent);
    }

    public Task<Dependent> GetAsync(uint id) => _repo.GetAsync(id);

    public Task<IEnumerable<Dependent>> GetAsync() => _repo.GetAsync();

    public Task<Dependent> UpsertAsync(Dependent dependent) => _repo.UpsertAsync(dependent);
}
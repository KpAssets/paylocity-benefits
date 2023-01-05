using Business.Employees;

namespace Business.Dependents;

internal sealed class DependentService : IDependentService
{
    private readonly IDependentRepository _repo;
    private readonly IDependentValidatorService _dependentValidator;
    private readonly IEmployeeService _employeeService;

    public DependentService(
        IDependentRepository repo,
        IDependentValidatorService dependentValidator,
        IEmployeeService employeeService)
    {
        _repo = repo;
        _dependentValidator = dependentValidator;
        _employeeService = employeeService;
    }

    public async Task<Dependent> DeleteAsync(uint id)
    {
        var dependent = await _repo.GetAsync(id);

        return await _repo.DeleteAsync(dependent);
    }

    public Task<Dependent> GetAsync(uint id) => _repo.GetAsync(id);

    public Task<IEnumerable<Dependent>> GetAsync() => _repo.GetAsync();

    public async Task<Dependent> UpsertAsync(Dependent dependent)
    {
        await FetchEmployeeIdForExistingDependent(dependent);

        // fetch the employee so we have the list of current dependents...
        var employee = await _employeeService.GetAsync(dependent.EmployeeId);

        // ... and ensure that the incoming dependent doesn't break any of our dependent rules
        _dependentValidator.ValidateDependents(BuildDesiredDependentState(employee.Dependents, dependent));

        return await _repo.UpsertAsync(dependent);
    }

    private async Task FetchEmployeeIdForExistingDependent(Dependent incomingDependent)
    {
        // if we're adding a dependent we already know the employee id
        if (incomingDependent.EmployeeId > 0) return;

        // if we're updating a dependent we need to fetch the employee id
        // so we can fetch the employee's dependent list
        var existingDependent = await _repo.GetAsync(incomingDependent.Id);

        incomingDependent.EmployeeId = existingDependent.EmployeeId;
    }

    private List<Dependent> BuildDesiredDependentState(IEnumerable<Dependent> existingDependents, Dependent incomingDependent)
    {
        var lookup = existingDependents.ToDictionary(x => x.Id);

        // if we're updating a dependent remove it from the lookup
        // so we can replace it with the incoming state
        if (lookup.TryGetValue(incomingDependent.Id, out Dependent _))
            lookup.Remove(incomingDependent.Id);

        lookup.Add(incomingDependent.Id, incomingDependent);

        return lookup.Values.ToList();
    }
}
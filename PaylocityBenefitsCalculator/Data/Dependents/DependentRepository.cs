using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using Business.Dependents;
using Data.Client;
using Data.Employees;

namespace Data.Dependents;

internal sealed class DependentRepository : IDependentRepository
{
    private readonly IDatabaseClient _client;
    private readonly IMapper _mapper;

    #region Queries
    private const string SELECT_DEPENDENT_BY_ID = @"
        SELECT
            dep.id,
            dep.first_name,
            dep.last_name,
            dep.date_of_birth,
            dep.relationship,
            ee.id,
            ee.first_name,
            ee.last_name,
            ee.date_of_birth,
            ee.salary
        FROM dependents AS dep
        LEFT JOIN employees AS ee ON ee.id = dep.employees_id
        WHERE dep.id=@Id;
    ";
    #endregion

    public DependentRepository(
        IDatabaseClient client,
        IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public async Task<Dependent> DeleteAsync(Dependent dependent)
    {
        await _client.WithConnectionAsync(
            c => c.DeleteAsync<DependentEntity>(_mapper.Map<DependentEntity>(dependent)),
            Constants.BENEFITS_CONNECTION);

        return dependent;
    }

    public async Task<Dependent> GetAsync(uint id)
    {
        var lookup = new Dictionary<uint, DependentEntity>();

        await _client.WithConnectionAsync(
            c => c.QueryAsync<DependentEntity, EmployeeEntity, DependentEntity>(
                SELECT_DEPENDENT_BY_ID,
                (ee, dep) => MapEmployeeToDependent(lookup, ee, dep),
                new { Id = id }),
            Constants.BENEFITS_CONNECTION);

        return _mapper.Map<Dependent>(lookup.Values.SingleOrDefault());
    }

    public Task<IEnumerable<Dependent>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Dependent> UpsertAsync(Dependent dependent)
    {
        throw new NotImplementedException();
    }

    private DependentEntity MapEmployeeToDependent(
        Dictionary<uint, DependentEntity> lookup,
        DependentEntity dep,
        EmployeeEntity ee)
    {
        if (!lookup.TryGetValue(dep.id, out DependentEntity dependent))
        {
            dependent = dep;
            lookup.Add(dependent.id, dependent);
        }

        if (ee != null)
        {
            dependent.employees_id = ee.id;
            dependent.employee = ee;
        }

        return dependent;
    }
}
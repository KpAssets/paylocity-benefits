using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using Business.Dependents;
using Business.Exceptions;
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

    private const string SELECT_ALL_DEPENDENTS = @"
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
    ";

    private const string UPDATE_DEPENDENT = @"
        UPDATE dependents 
        SET first_name=@first_name, last_name=@last_name, date_of_birth=@date_of_birth, relationship=@relationship
        WHERE id=@id;
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
                (dep, ee) => MapEmployeeToDependent(lookup, dep, ee),
                new { Id = id }),
            Constants.BENEFITS_CONNECTION);

        if (!lookup.Values.Any())
            throw new NotFoundException($"Dependent with id '{id}' not found");

        return _mapper.Map<Dependent>(lookup.Values.SingleOrDefault());
    }

    public async Task<IEnumerable<Dependent>> GetAsync()
    {
        var lookup = new Dictionary<uint, DependentEntity>();

        await _client.WithConnectionAsync(
            c => c.QueryAsync<DependentEntity, EmployeeEntity, DependentEntity>(
                SELECT_ALL_DEPENDENTS,
                (dep, ee) => MapEmployeeToDependent(lookup, dep, ee)),
            Constants.BENEFITS_CONNECTION);

        return _mapper.Map<IEnumerable<Dependent>>(lookup.Values);
    }

    public Task<Dependent> UpsertAsync(Dependent dependent)
    {
        if (dependent.Id <= 0)
            return InsertAsync(dependent);

        return UpdateAsync(dependent);
    }

    private async Task<Dependent> InsertAsync(Dependent dependent)
    {
        var insertedId = await _client.WithConnectionAsync(
            c => c.InsertAsync<DependentEntity>(_mapper.Map<DependentEntity>(dependent)),
            Constants.BENEFITS_CONNECTION);

        dependent.Id = (uint)insertedId;

        return dependent;
    }

    private async Task<Dependent> UpdateAsync(Dependent dependent)
    {
        await _client.WithConnectionAsync(
            c => c.ExecuteAsync(UPDATE_DEPENDENT, _mapper.Map<DependentEntity>(dependent)),
            Constants.BENEFITS_CONNECTION);

        return dependent;
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
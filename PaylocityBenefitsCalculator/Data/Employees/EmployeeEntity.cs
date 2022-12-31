using Dapper.Contrib.Extensions;
using Data.Dependents;

namespace Data.Employees;

[Table("employees")]
internal class EmployeeEntity : PersonEntity
{
    public decimal salary { get; set; }
    [Write(false)]
    public List<DependentEntity> dependents { get; set; } = new List<DependentEntity>();
}

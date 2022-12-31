using Dapper.Contrib.Extensions;
using Data.Employees;

namespace Data.Dependents;

[Table("dependents")]
internal class DependentEntity : PersonEntity
{
    public string relationship { get; set; } = "none";
    public uint employees_id { get; set; }
    [Write(false)]
    public EmployeeEntity? employee { get; set; }
}
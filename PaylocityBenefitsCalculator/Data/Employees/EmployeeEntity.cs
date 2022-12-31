using Dapper.Contrib.Extensions;

namespace Data.Employees;

[Table("employees")]
internal class EmployeeEntity : PersonEntity
{
    [Key]
    public uint id { get; set; }
    public decimal salary { get; set; }
}

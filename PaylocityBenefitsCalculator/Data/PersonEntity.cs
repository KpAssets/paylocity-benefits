using Dapper.Contrib.Extensions;

namespace Data;

internal abstract class PersonEntity
{
    [Key]
    public uint id { get; set; }
    public string? first_name { get; set; }
    public string? last_name { get; set; }
    public DateTime date_of_birth { get; set; }
}
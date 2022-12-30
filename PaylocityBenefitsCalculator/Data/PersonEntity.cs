namespace Data;

internal abstract class PersonEntity
{
    public string? first_name { get; set; }
    public string? last_name { get; set; }
    public DateTime date_of_birth { get; set; }
}
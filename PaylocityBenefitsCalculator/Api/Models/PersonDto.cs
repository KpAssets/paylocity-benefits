namespace Api.Models;

public abstract class PersonDto
{
    public uint Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}
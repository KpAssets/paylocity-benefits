namespace Api.Models.Dependents;

public class GetDependentDto : PersonDto
{
    public int Id { get; set; }
    public string Relationship { get; set; } = "None";
}
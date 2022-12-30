namespace Api.Models.Dependents;

public class AddDependentDto : PersonDto
{
    public string Relationship { get; set; } = "None";
}
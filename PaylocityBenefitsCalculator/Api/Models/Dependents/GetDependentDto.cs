namespace Api.Models.Dependents;

public class GetDependentDto : PersonDto
{
    public string Relationship { get; set; } = "None";
}
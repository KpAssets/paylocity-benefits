namespace Api.Models.Dependents;

public class UpdateDependentDto : PersonDto
{
    public string Relationship { get; set; } = "None";
}
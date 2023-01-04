namespace Api.Models.Validations;

internal interface IValidatable
{
    ValidationResult Validate();
}
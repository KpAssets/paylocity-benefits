namespace Api.Models.Payroll;

public class PayrollItemDto
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
}
namespace Business.Calculations.Payroll;

internal abstract class PayrollItem
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
}
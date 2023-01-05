namespace Business.Payroll;

public abstract class PayrollItem
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
}
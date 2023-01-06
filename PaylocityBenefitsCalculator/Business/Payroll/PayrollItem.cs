namespace Business.Payroll;

public abstract class PayrollItem
{
    private decimal _amount;
    public decimal Amount
    {
        get
        {
            return decimal.Round(_amount, 2, MidpointRounding.AwayFromZero);
        }
        set
        {
            _amount = value;
        }
    }

    public string Description { get; set; } = string.Empty;
}
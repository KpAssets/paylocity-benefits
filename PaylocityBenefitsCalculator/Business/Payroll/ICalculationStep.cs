namespace Business.Payroll;

internal interface ICalculationStep
{
    Task<Paycheck> Process(Paycheck check);
}
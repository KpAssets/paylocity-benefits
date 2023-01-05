namespace Business.Payroll;

internal interface ICalculationStepService<T> where T : ICalculationStep
{
    Task<Paycheck> Process(Paycheck check);
}
namespace Business.Payroll;

// ICalculationStepService allows us to easily run a 'group' of steps
internal interface ICalculationStepService<T> where T : ICalculationStep
{
    Task<Paycheck> Process(Paycheck check);
}
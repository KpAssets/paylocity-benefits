namespace Business.Calculations;

internal interface ICalculationStep<T> where T : class
{
    Task<T> Process(T input);
}
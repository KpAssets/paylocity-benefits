namespace Business.Payroll;

internal sealed class CalculationStepService<T> : ICalculationStepService<T> where T : ICalculationStep
{
    private readonly IEnumerable<T> _steps;

    public CalculationStepService(IEnumerable<T> steps)
    {
        _steps = steps;
    }

    public async Task<Paycheck> Process(Paycheck check)
    {
        foreach (var step in _steps)
            await step.Process(check);

        return check;
    }
}
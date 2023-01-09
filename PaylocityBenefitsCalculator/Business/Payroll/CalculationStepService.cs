namespace Business.Payroll;

internal sealed class CalculationStepService<T> : ICalculationStepService<T> where T : ICalculationStep
{
    // Gather all the steps for a particular 'group' (T) of calculation steps
    private readonly IEnumerable<T> _steps;

    public CalculationStepService(IEnumerable<T> steps)
    {
        _steps = steps;
    }

    public async Task<Paycheck> Process(Paycheck check)
    {
        // For every step in the group call `Process`
        foreach (var step in _steps)
            await step.Process(check);

        return check;
    }
}
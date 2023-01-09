namespace Business.Payroll;

// I thought of the calculation requirements as a series of steps that needed to be performed
// So, ICalculationStep represents each of those steps
// Each step accepts a Paycheck instance, mutates the state if necessary, and returns the Paycheck instance
// This keeps each calculation separate from the next. Each step can be tested in isolation.
internal interface ICalculationStep
{
    Task<Paycheck> Process(Paycheck check);
}
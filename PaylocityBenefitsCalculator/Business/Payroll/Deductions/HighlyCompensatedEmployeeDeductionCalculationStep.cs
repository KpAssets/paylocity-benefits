namespace Business.Payroll.Deductions;

internal sealed class HighlyCompensatedEmployeeDeductionCalculationStep : IDeductionCalculationStep
{
    private const decimal HIGHLY_COMPENSATED_EMPLOYEE_SALARY_THRESHOLD = 80000;
    private const decimal SALARY_PERCENTAGE_COST = 0.02m;

    public Task<Paycheck> Process(Paycheck check)
    {
        try
        {
            if (EmployeesSalaryGreaterThanSalaryThreshold(check.Employee.Salary))
            {
                check.Deductions.Add(
                    BuildHighlyCompensatedEmployeeDeduction(check.Employee.Salary));
            }
        }
        catch (Exception)
        {
            // log!
            throw;
        }

        return Task.FromResult(check);
    }

    private bool EmployeesSalaryGreaterThanSalaryThreshold(decimal salary) =>
        salary > HIGHLY_COMPENSATED_EMPLOYEE_SALARY_THRESHOLD;

    private Deduction BuildHighlyCompensatedEmployeeDeduction(decimal salary) =>
        new Deduction
        {
            Amount = (salary * SALARY_PERCENTAGE_COST) / 26,
            Description = "Highly compensated employee deduction"
        };
}
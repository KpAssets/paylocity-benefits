namespace Business.Payroll;

public interface IPayrollService
{
    Task<Paycheck> CalculatePaycheckForEmployee(uint employeeId);
}
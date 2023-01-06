using Business.Employees;
using Business.Payroll.Earnings;
using Business.Payroll.Deductions;

namespace Business.Payroll;

internal sealed class PayrollService : IPayrollService
{
    private readonly IEmployeeService _employeeService;
    private readonly ICalculationStepService<IEarningCalculationStep> _earningsService;
    private readonly ICalculationStepService<IDeductionCalculationStep> _deductionsService;

    public PayrollService(
        IEmployeeService employeeService,
        ICalculationStepService<IEarningCalculationStep> earningsService,
        ICalculationStepService<IDeductionCalculationStep> deductionsService)
    {
        _employeeService = employeeService;
        _earningsService = earningsService;
        _deductionsService = deductionsService;
    }

    public async Task<Paycheck> CalculatePaycheckForEmployee(uint employeeId)
    {
        var check = new Paycheck(await _employeeService.GetAsync(employeeId));

        await _earningsService.Process(check);
        await _deductionsService.Process(check);

        check.NetPay = CalculateNetPay(check);

        return check;
    }

    private decimal CalculateNetPay(Paycheck check) =>
        check.Earnings.Sum(x => x.Amount) - check.Deductions.Sum(x => x.Amount);
}
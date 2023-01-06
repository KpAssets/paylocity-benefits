using Api.Models.Employees;

namespace Api.Models.Payroll;

public class PaycheckDto
{
    public Guid Id { get; set; }
    public GetEmployeeDto Employee { get; set; }
    public List<PayrollItemDto> Earnings { get; set; }
    public List<PayrollItemDto> Deductions { get; set; }
    public decimal NetPay { get; set; }
}
using Business.Employees;
using Business.Payroll.Earnings;
using Business.Payroll.Deductions;

namespace Business.Payroll;

public class Paycheck
{
    public Paycheck(Employee employee)
    {
        Employee = employee;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public Employee Employee { get; }
    public List<Earning> Earnings { get; set; } = new List<Earning>();
    public List<Deduction> Deductions { get; set; } = new List<Deduction>();
    public decimal NetPay { get; set; }
}
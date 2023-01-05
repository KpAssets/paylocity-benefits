using Business.Employees;
using Business.Calculations.Payroll.Earnings;
using Business.Calculations.Payroll.Deductions;

namespace Business.Calculations.Payroll;

// Represents an employee's payroll check
internal class Check
{
    public Guid Id { get; } = Guid.NewGuid();
    public Employee Employee { get; set; }
    public List<Earning> Earnings { get; set; } = new List<Earning>();
    public List<Deduction> Deductions { get; set; } = new List<Deduction>();
    public decimal NetPay { get; set; }
}
namespace Business.Payroll.Earnings;

// Every calculation step that mutates earnings would implement this interface
internal interface IEarningCalculationStep : ICalculationStep { }
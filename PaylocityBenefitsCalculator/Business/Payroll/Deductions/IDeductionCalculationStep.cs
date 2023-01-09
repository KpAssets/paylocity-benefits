namespace Business.Payroll.Deductions;

// Every calculation step that mutates deductions would implement this interface
internal interface IDeductionCalculationStep : ICalculationStep { }
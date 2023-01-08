namespace Business;

public sealed class Settings
{
    public uint PayPeriods { get; set; } = 26;
    public decimal YearlyBaseBenefitCost { get; set; } = 12000;
    public decimal YearlyBenefitCostPerDependent { get; set; } = 7200;
    public decimal HighlyCompensatedEmployeeSalaryThreshold { get; set; } = 80000;
    public decimal HighlyCompensatedEmployeeDeductionPercentage { get; set; } = 0.02m;
    public decimal YearlyBenefitCostPerSeniorDependent { get; set; } = 2400;
}
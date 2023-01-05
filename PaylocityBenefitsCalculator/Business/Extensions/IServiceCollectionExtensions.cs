using Microsoft.Extensions.DependencyInjection;
using Business.Dependents;
using Business.Payroll;
using Business.Payroll.Deductions;
using Business.Payroll.Earnings;
using Business.Employees;

namespace Business.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services) =>
        services
            .AddDependentServices()
            .AddEmployeeServices()
            .AddPayrollServices();

    private static IServiceCollection AddEmployeeServices(this IServiceCollection services) =>
        services
            .AddSingleton<IEmployeeService, EmployeeService>();

    private static IServiceCollection AddDependentServices(this IServiceCollection services) =>
        services
            .AddSingleton<IDependentService, DependentService>()
            .AddSingleton<IDependentValidatorService, DependentValidatorService>();

    private static IServiceCollection AddPayrollServices(this IServiceCollection services) =>
        services
            .AddSingleton<IPayrollService, PayrollService>()
            .AddEarningsServices()
            .AddDeductionServices();

    private static IServiceCollection AddEarningsServices(this IServiceCollection services) =>
        services
            .AddSingleton<ICalculationStepService<IEarningCalculationStep>, CalculationStepService<IEarningCalculationStep>>()
            .AddSingleton<IEarningCalculationStep, SalaryCalculationStep>();

    private static IServiceCollection AddDeductionServices(this IServiceCollection services) =>
        services
            .AddSingleton<ICalculationStepService<IDeductionCalculationStep>, CalculationStepService<IDeductionCalculationStep>>()
            .AddSingleton<IDeductionCalculationStep, BaseDeductionCalculationStep>()
            .AddSingleton<IDeductionCalculationStep, DependentDeductionCalculationStep>()
            .AddSingleton<IDeductionCalculationStep, HighlyCompensatedEmployeeDeductionCalculationStep>()
            .AddSingleton<IDeductionCalculationStep, SeniorDependentsDeductionCalculationStep>();
}
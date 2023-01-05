using Microsoft.Extensions.DependencyInjection;
using Business.Dependents;
using Business.Calculations;
using Business.Calculations.Payroll;
using Business.Calculations.Payroll.Deductions;
using Business.Calculations.Payroll.Earnings;
using Business.Employees;

namespace Business.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services) =>
        services
            .AddDependentServices()
            .AddEmployeeServices();

    private static IServiceCollection AddEmployeeServices(this IServiceCollection services) =>
        services
            .AddSingleton<IEmployeeService, EmployeeService>();

    private static IServiceCollection AddDependentServices(this IServiceCollection services) =>
        services
            .AddSingleton<IDependentService, DependentService>()
            .AddSingleton<IDependentValidatorService, DependentValidatorService>();

    private static IServiceCollection AddEarningsServices(this IServiceCollection services) =>
        services
            .AddSingleton<IEarningCalculationStep, SalaryCalculationStep>();

    private static IServiceCollection AddDeductionServices(this IServiceCollection services) =>
        services
            .AddSingleton<IDeductionCalculationStep, BaseDeductionCalculationStep>()
            .AddSingleton<IDeductionCalculationStep, DependentDeductionCalculationStep>()
            .AddSingleton<IDeductionCalculationStep, HighlyCompensatedEmployeeDeductionCalculationStep>()
            .AddSingleton<IDeductionCalculationStep, SeniorDependentsDeductionCalculationStep>();
}
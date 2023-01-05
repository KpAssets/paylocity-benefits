using Microsoft.Extensions.DependencyInjection;
using Business.Dependents;
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
}
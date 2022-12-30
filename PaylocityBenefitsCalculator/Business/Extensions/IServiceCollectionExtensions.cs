using Microsoft.Extensions.DependencyInjection;
using Business.Employees;

namespace Business.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services) =>
        services
            .AddEmployeeServices();

    private static IServiceCollection AddEmployeeServices(this IServiceCollection services) =>
        services
            .AddSingleton<IEmployeeService, EmployeeService>();
}
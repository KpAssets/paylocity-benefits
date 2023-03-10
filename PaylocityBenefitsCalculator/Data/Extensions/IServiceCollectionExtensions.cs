using Microsoft.Extensions.DependencyInjection;
using Business.Dependents;
using Business.Employees;
using Data.Dependents;
using Data.Employees;

namespace Data.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services) =>
        services
            .AddRepositories();

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddSingleton<IEmployeeRepository, EmployeeRepository>()
            .AddSingleton<IDependentRepository, DependentRepository>();
}
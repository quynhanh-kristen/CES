using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Netcompany.Net.DomainDrivenDesign.Services;

namespace RoutePlanning.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRoutePlanningDomain(this IServiceCollection services)
    {
        services.Scan(scanner =>
            scanner.FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(classes => classes.AssignableTo<IDomainService>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;
using RoutePlanning.Domain;
using Netcompany.Net.Cqs;
using Netcompany.Net.Validation;

namespace RoutePlanning.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRoutePlanningApplication(this IServiceCollection services)
    {
        services.AddRoutePlanningDomain();

        services.AddCqs();
        services.AddValidation();

        return services;
    }
}

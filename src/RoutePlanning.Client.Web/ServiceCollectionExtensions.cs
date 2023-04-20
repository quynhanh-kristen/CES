using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using RoutePlanning.Client.Web.Authentication;
using RoutePlanning.Client.Web.Authorization;

namespace RoutePlanning.Client.Web;

public static class ServiceCollectionExtensions
{
    public static void AddSimpleAuthentication(this IServiceCollection services)
    {
        services.AddScoped<ProtectedBrowserStorage, ProtectedSessionStorage>();
        services.AddScoped<SimpleAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<SimpleAuthenticationStateProvider>());
    }

    public static void AddApiTokenAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAuthorizationHandler, TokenRequirementHandler>();
        services.AddAuthorization(options =>
        {
            var apiToken = configuration.GetValue<string>("ApiToken")!;
            options.AddPolicy(nameof(TokenRequirement), policy => policy.AddRequirements(new TokenRequirement(apiToken)));
        });
    }
}

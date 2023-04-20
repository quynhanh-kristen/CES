using Microsoft.AspNetCore.Authorization;

namespace RoutePlanning.Client.Web.Authorization;

public sealed record TokenRequirement(string Token) : IAuthorizationRequirement;

public sealed class TokenRequirementHandler : AuthorizationHandler<TokenRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenRequirement requirement)
    {
        if (context.Resource is HttpContext httpContext)
        {
            var apiToken = httpContext.Request.Headers["Token"];

            if (apiToken == requirement.Token)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }

        return Task.CompletedTask;
    }
}

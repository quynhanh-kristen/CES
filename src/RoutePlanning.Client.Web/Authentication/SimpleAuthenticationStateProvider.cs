using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace RoutePlanning.Client.Web.Authentication;

public sealed class SimpleAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedBrowserStorage _browserStorage;
    private readonly AuthenticationState _anonymousState;

    public SimpleAuthenticationStateProvider(ProtectedBrowserStorage browserStorage)
    {
        _browserStorage = browserStorage;
        _anonymousState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var userSessionStorageResult = await _browserStorage.GetAsync<UserSession>(nameof(UserSession));

        var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

        if (userSession is null)
        {
            return _anonymousState;
        }

        return CreateUserAuthenticationState(userSession);
    }

    public async Task SetAuthenticationStateAsync(UserSession userSession)
    {
        await _browserStorage.SetAsync(nameof(UserSession), userSession);

        var userAuthState = CreateUserAuthenticationState(userSession);
        NotifyAuthenticationStateChanged(Task.FromResult(userAuthState));
    }

    public async Task ClearAuthenticationStateAsync()
    {
        await _browserStorage.DeleteAsync(nameof(UserSession));

        NotifyAuthenticationStateChanged(Task.FromResult(_anonymousState));
    }

    private static AuthenticationState CreateUserAuthenticationState(UserSession userSession)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userSession.Username),
        };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "UsernamePassword"));

        return new AuthenticationState(claimsPrincipal);
    }
}

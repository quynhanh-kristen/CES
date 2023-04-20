using Microsoft.AspNetCore.Components;
using MediatR;
using RoutePlanning.Application.Users.Queries.AuthenticatedUser;
using RoutePlanning.Client.Web.Authentication;

namespace RoutePlanning.Client.Web.Pages.Core;

public sealed partial class LoginDisplay
{
    private string Username { get; set; } = string.Empty;
    private string Password { get; set; } = string.Empty;
    private AuthenticatedUser? User { get; set; }
    private bool ShowAuthError { get; set; } = false;

    [Inject]
    private SimpleAuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    private async Task Login()
    {
        User = await Mediator.Send(new AuthenticatedUserQuery(Username, Password), CancellationToken.None);

        ShowAuthError = User is null;

        if (User is not null)
        {
            await AuthStateProvider.SetAuthenticationStateAsync(new UserSession(User.Username));
        }
    }

    private async Task Logout()
    {
        await AuthStateProvider.ClearAuthenticationStateAsync();

        ShowAuthError = false;
    }
}

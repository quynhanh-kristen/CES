using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Application.Users.Queries.AuthenticatedUser;

public sealed class AuthenticatedUserQueryHandler : IQueryHandler<AuthenticatedUserQuery, AuthenticatedUser?>
{
    private readonly IQueryable<User> _users;

    public AuthenticatedUserQueryHandler(IQueryable<User> users)
    {
        _users = users;
    }

    public async Task<AuthenticatedUser?> Handle(AuthenticatedUserQuery request, CancellationToken cancellationToken)
    {
        // Note that it is considered bad practise to roll your own security like this. Use establised frameworks and services for authentication instead.
        var hashedPassword = User.ComputePasswordHash(request.Password);

        var authenticatedUser = await _users
            .Where(u => u.Username.ToLower() == request.Username.ToLower())
            .Where(u => u.PasswordHash == hashedPassword)
            .Select(MapAuthenticatedUser)
            .SingleOrDefaultAsync(cancellationToken);

        return authenticatedUser;
    }

    private static Expression<Func<User, AuthenticatedUser>> MapAuthenticatedUser => u => new AuthenticatedUser(u.Id, u.Username);
}

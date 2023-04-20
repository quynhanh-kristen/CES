using Netcompany.Net.Cqs.Queries;

namespace RoutePlanning.Application.Users.Queries.AuthenticatedUser;

public sealed record AuthenticatedUserQuery(string Username, string Password) : IQuery<AuthenticatedUser?>;

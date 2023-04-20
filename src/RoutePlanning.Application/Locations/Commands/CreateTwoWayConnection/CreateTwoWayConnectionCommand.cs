using Netcompany.Net.Cqs.Commands;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Application.Locations.Commands.CreateTwoWayConnection;

public sealed record CreateTwoWayConnectionCommand(Location.EntityId LocationAId, Location.EntityId LocationBId, int Distance) : ICommand;

using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Application.Locations.Queries.Distance;

public sealed record DistanceQuery(Location.EntityId SourceId, Location.EntityId DestinationId) : IQuery<int>;

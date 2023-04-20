using Netcompany.Net.Cqs.Queries;

namespace RoutePlanning.Application.Locations.Queries.SelectableLocationList;

public sealed record SelectableLocationListQuery : IQuery<IReadOnlyList<SelectableLocation>>;

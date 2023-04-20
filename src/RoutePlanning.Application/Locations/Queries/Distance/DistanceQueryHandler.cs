using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Locations;
using RoutePlanning.Domain.Locations.Services;

namespace RoutePlanning.Application.Locations.Queries.Distance;

public sealed class DistanceQueryhandler : IQueryHandler<DistanceQuery, int>
{
    private readonly IQueryable<Location> _locations;
    private readonly IShortestDistanceService _shortestDistanceService;

    public DistanceQueryhandler(IQueryable<Location> locations, IShortestDistanceService shortestDistanceService)
    {
        _locations = locations;
        _shortestDistanceService = shortestDistanceService;
    }

    public async Task<int> Handle(DistanceQuery request, CancellationToken cancellationToken)
    {
        var source = await _locations.FirstAsync(l => l.Id == request.SourceId, cancellationToken);
        var destination = await _locations.FirstAsync(l => l.Id == request.DestinationId, cancellationToken);

        var distance = _shortestDistanceService.CalculateShortestDistance(source, destination);

        return distance;
    }
}

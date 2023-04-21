using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Source} --{Distance}--> {Destination}")]
public sealed class Connection : Entity<Connection>
{
    public Connection(Location source, Location destination, Distance distance, int price, int _typeOfTransport)
    {
        Source = source;
        Destination = destination;
        Distance = distance;
        Price = price;
        typeOfTransport = _typeOfTransport;
    }

    private Connection()
    {
        Source = null!;
        Destination = null!;
        Distance = null!;
    }

    public Location Source { get; private set; }

    public Location Destination { get; private set; }

    public Distance Distance { get; private set; }

    public int Price { get; private set;}

    public int typeOfTransport { get;}
}

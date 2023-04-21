using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Name}")]
public sealed class Location : AggregateRoot<Location>
{
    public Location(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    private readonly List<Connection> _connections = new();

    public IReadOnlyCollection<Connection> Connections => _connections.AsReadOnly();

    public Connection AddConnection(Location destination, int distance, int price, int typeOfTransport)
    {
        Connection connection = new(this, destination, distance, price, typeOfTransport);

        _connections.Add(connection);

        return connection;
    }
}

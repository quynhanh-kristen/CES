#nullable disable

//using System.Net.Http.Json;
//using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RoutePlanning.Domain.Locations.Services;

public class Graph
{
    private readonly int _v; // No. of vertices in graph
    private List<int>[] adjList; // adjacency list

    // Constructor
    public Graph(int vertices)
    {
        _v = vertices; // initialise vertex count
        initAdjList(); // initialise adjacency list
    }

    private void initAdjList()
    {
        adjList = new List<int>[_v];

        for (var i = 0; i < _v; i++)
        {
            adjList[i] = new List<int>();
        }
    }

    public void addEdge(int u, int v)
    {
        adjList[u].Add(v);
    }

    public List<List<int>> getAllPaths(int s, int d)
    {
        var isVisited = new bool[_v];
        var pathList = new List<int>
        {
            s
        };

        var paths = new List<List<int>>();

        getAllPathsUtil(s, d, isVisited, pathList, paths, 0);

        return paths;
    }

    public void getAllPathsUtil(int u, int d, bool[] isVisited, List<int> localPathList, List<List<int>> allPaths, int level)
    {
        if (level > 9)
        {
            return;
        }

        if (u.Equals(d))
        {
            var path = new List<int>();

            for (var i = 0; i < localPathList.Count; i++)
            {
                path.Add(localPathList[i]);
            }

            allPaths.Add(path);

            return;
        }

        isVisited[u] = true;

        foreach (var i in adjList[u])
        {
            if (!isVisited[i])
            {
                localPathList.Add(i);
                getAllPathsUtil(i, d, isVisited, localPathList, allPaths, level + 1);

                localPathList.Remove(i);
            }
        }

        isVisited[u] = false;
    }
}

public class GuidToInt
{
    private int lastInt;
    private readonly Dictionary<Guid, int> _map;
    public readonly List<string> _names = new List<string>();
    public GuidToInt()
    {
        lastInt = -1;
        _map = new Dictionary<Guid, int>();
    }

    public int GetInt(Guid g, string name)
    {
        if (_map.ContainsKey(g))
        {
            return _map[g];
        }
        else
        {
            lastInt += 1;
            _map.Add(g, lastInt);
            _names.Add(name);
            return lastInt;
        }
    }
}

public class Route
{
    public readonly List<Connection> connections = new List<Connection>();

    public Route(List<int> ids, List<string> names, Dictionary<string, Connection> namesToConnection)
    {
        var n = new List<string>();
        foreach (var id in ids)
        {
            n.Add(names[id]);
        }

        for (var i = 1; i < n.Count; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                var key = n[i] + n[i - 1] + j.ToString();
                if (namesToConnection.ContainsKey(key))
                {
                    connections.Add(namesToConnection[key]);
                }
            }
        }
    }
}

public sealed class ShortestDistanceService : IShortestDistanceService
{
    private readonly List<Location> _locations;
    private readonly Graph _graph;
    private readonly GuidToInt _converter;
    private readonly List<string> _names;
    private readonly Dictionary<string, Connection> _namesToConnection;

    public ShortestDistanceService(IQueryable<Location> locations)
    {
        _locations = locations.Include(l => l.Connections).ThenInclude(c => c.Destination).ToList();
        _converter = new GuidToInt();
        _graph = GetGraph(_converter, _locations);
        _names = _converter._names;
        _namesToConnection = GetConnectionsMappings(_locations);
    }

    private static Dictionary<string, Connection> GetConnectionsMappings(List<Location> locations)
    {
        var map = new Dictionary<string, Connection>();
        foreach (var location in locations)
        {
            foreach (var connection in location.Connections)
            {
                var key = connection.Source.Name + connection.Destination.Name + connection.typeOfTransport;
                map.Add(key, connection);
            }
        }

        return map;
    }

    private static List<Connection> GetExternalConnections(List<Location> locations)
    {
        var connections = new List<Connection>();

        foreach (var location in locations)
        {
            foreach (var connection in location.Connections)
            {
                if (connection.typeOfTransport != 0)
                {
                    connections.Add(connection);
                }
            }
        }

        return connections;
    }

    public void GetExternalRouteData(List<Route> routes)
    {
        var Sources = new List<string>();
        var Destinations = new List<string>();
        var PickupTimes = new List<int>();
        var ParcelTypes = new List<string>();

        var externalConnections = GetExternalConnections(_locations);

        foreach (var c in externalConnections)
        {
            Sources.Add(c.Source.Name);
            Destinations.Add(c.Destination.Name);
            PickupTimes.Add(0);
            ParcelTypes.Add("");
        }

        var routesRequestData = new RoutesRequestData(Sources, Destinations, PickupTimes, ParcelTypes);

        var json = JsonConvert.SerializeObject(routesRequestData);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var url = "https://localhost:7004/api/getRoutesData";
        using var client = new HttpClient();

        var response = client.PostAsync(url, data).Result;

        var result = response.Content.ReadAsStringAsync().Result;
        Console.WriteLine(result);
    }

    public int CalculateShortestDistance(Location source, Location target)
    {
        var a = _converter.GetInt(source.Id.Value, source.Name);
        var b = _converter.GetInt(target.Id.Value, target.Name);

        var paths = _graph.getAllPaths(a, b);

        var routes = new List<Route>();

        foreach (var path in paths)
        {
            var route = new Route(path, _names, _namesToConnection);
            routes.Add(route);
        }

        foreach (var route in routes)
        {
            foreach (var connection in route.connections)
            {
                Console.WriteLine(connection.Destination.Name + " " + connection.Source.Name + " " + connection.Price);
            }

            Console.WriteLine();
        }

        GetExternalRouteData(routes);
        Console.WriteLine("Here");

        return 10; // TODO Return list of routes instead
    }

    private static Graph GetGraph(GuidToInt converter, List<Location> locations)
    {
        var g = new Graph(locations.Count);

        foreach (var location in locations)
        {
            foreach (var connection in location.Connections)
            {
                g.addEdge(
                    converter.GetInt(connection.Source.Id, connection.Source.Name),
                    converter.GetInt(connection.Destination.Id, connection.Destination.Name));
            }
        }

        return g;
    }
}

public class RoutesRequestData
{
    public List<string> sources { get; set; }
    public List<string> destinations { get; set; }
    public List<int> pickupTimes { get; set; }
    public List<string> parcelTypes { get; set; }

    public RoutesRequestData(List<string> sources, List<string> destinations, List<int> pickupTimes, List<string> parcelTypes)
    {
        this.sources = sources;
        this.destinations = destinations;
        this.pickupTimes = pickupTimes;
        this.parcelTypes = parcelTypes;
    }
}

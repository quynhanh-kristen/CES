using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
//using Newtonsoft.Json;
using RoutePlanning.Application.Locations.Commands.CreateTwoWayConnection;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Client.Web.Api;

[Route("api/")]
[ApiController]
public sealed class RoutesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly List<Location> _locations;

    public RoutesController(IMediator mediator, IQueryable<Location> locations)
    {
        _mediator = mediator;
        _locations = locations.Include(l => l.Connections).ThenInclude(c => c.Destination).ToList();
    }

    [HttpGet("hello")] // https://localhost:7004/api/hello
    public Task<string> HelloWorld()
    {
        return Task.FromResult("Hello World!");
    }

    [HttpGet("hello/{id}")] // https://localhost:7004/api/hello/1
    public Task<string> HelloWorld1(int id)
    {
        return Task.FromResult("Hello World!" + id);
    }

    [HttpPost("[action]")]
    public async Task AddTwoWayConnection(CreateTwoWayConnectionCommand command)
    {
        await _mediator.Send(command);
    }

    [HttpPost("post")]
    public ActionResult<string> Post([FromBody] Data requestModel)
    {
        //// Process the incoming JSON request
        //string requestData = requestModel.Data;
        //// Do some processing...
        //var responseData = requestData.ToUpper(); // Just an example

        //// Return the response as JSON
        //MyResponseModel responseModel = new MyResponseModel { Data = responseData };
        //string jsonResponse = JsonConvert.SerializeObject(responseModel);
        //return jsonResponse;

        Console.WriteLine(requestModel);

        return "Test123";
    }

    [HttpPost("getRoutesData")]
    public ActionResult<string> GetRoutesData([FromBody] RoutesRequestData routesRequestData)
    {
        var namesToConnection = GetConnectionsMappings(_locations);

        var prices = new List<int>();
        var shippingDuration = new List<int>();

        for (var i = 0; i < routesRequestData.Sources.Count; i++)
        {
            var connection = namesToConnection[routesRequestData.Sources[i] + routesRequestData.Destinations[i] + "0"]; // "1" 1 is hardcoded for ship
            prices.Add(connection.Price);
            shippingDuration.Add(connection.Distance);
        }

        return JsonConvert.SerializeObject(new RoutesResponseData(
            shippingDuration,
            prices,
            routesRequestData.PickupTimes,
            routesRequestData.Destinations,
            routesRequestData.Sources,
            routesRequestData.ParcelTypes
            ));
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
}

public class RoutesResponseData
{
    public List<string> Sources { get; set; }
    public List<string> Destinations { get; set; }
    public List<int> PickupTimes { get; set; }
    public List<int> Prices { get; set; }
    public List<int> ShippingDuration { get; set; }
    public List<string> ParcelTypes { get; set; }

    public RoutesResponseData(List<int> shippingDuration, List<int> prices, List<int> pickupTimes, List<string> destinations, List<string> sources, List<string> parcelTypes)
    {
        ShippingDuration = shippingDuration;
        Prices = prices;
        PickupTimes = pickupTimes;
        Destinations = destinations;
        Sources = sources;
        ParcelTypes = parcelTypes;
    }
}

public class RoutesRequestData
{
    public List<string> Sources { get; set; }
    public List<string> Destinations { get; set; }
    public List<int> PickupTimes { get; set; }
    public List<string> ParcelTypes { get; set; }

    public RoutesRequestData(List<string> sources, List<string> destinations, List<int> pickupTimes, List<string> parcelTypes)
    {
        Sources = sources;
        Destinations = destinations;
        PickupTimes = pickupTimes;
        ParcelTypes = parcelTypes;
    }
}

public class Data
{
    public string Destination { get; set; }

    public Data(string destination)
    {
        Destination = destination;
    }
}

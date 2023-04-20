using Microsoft.AspNetCore.Mvc.Testing;

namespace RoutePlanning.Client.Web.ApiTests;

public class RoutePlanningApplicationFactory : WebApplicationFactory<Program>
{
    public RoutePlanningApplicationFactory()
    {
        HttpClient = CreateClient();
    }

    public HttpClient HttpClient { get; private set; }
}

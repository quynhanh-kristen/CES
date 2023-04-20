using Netcompany.Net.Testing.Api;
using RoutePlanning.Client.Web.Api;

namespace RoutePlanning.Client.Web.ApiTests.Authentication;

public class TokenTests : IClassFixture<RoutePlanningApplicationFactory>
{
    private readonly RoutePlanningApplicationFactory _factory;
    private readonly HttpClient _client;

    public TokenTests(RoutePlanningApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.HttpClient;
    }

    [Fact]
    public async void ShouldGetHelloWorld()
    {
        // Arrange
        var url = _factory.GetRoute<Program, RoutesController>(x => x.HelloWorld);

        // Act
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Token", "TheSecretApiToken");

        var response = await _client.SendAsync(request);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Hello World!", content);
    }
}

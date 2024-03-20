using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Shouldly;
using HelloApi;

namespace HelloApi.ApiTests;

public class BasicTests(WebApplicationFactory<Program> factory)
        : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Theory]
    [InlineData("/weatherforecast")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType.ToString().ShouldBe("application/json; charset=utf-8");
        var items = (await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>());
        items.ShouldNotBeNull();
        items.Count().ShouldBe(5);

    }
}

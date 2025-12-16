using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;

namespace DevSecOpsDemo.Tests;

public class HealthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public HealthTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_Health_ReturnsOkAndStatus()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/health");

        // Assert
        response.EnsureSuccessStatusCode(); 
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("ok", content);
    }
}

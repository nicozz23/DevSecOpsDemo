using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DevSecOpsDemo.Tests;

public class SumaTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public SumaTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Post_Suma_ValidInput_ReturnsCorrectSum()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new { A = 10, B = 20 };

        // Act
        var response = await client.PostAsJsonAsync("/api/suma", request);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<SumaResult>();
        Assert.Equal(30, result?.Result);
    }

    [Fact]
    public async Task Post_Suma_NullInput_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PostAsync("/api/suma", null); // No body implies invalid/null for FromBody if not configured otherwise

        // Assert
        // Usually, empty body for [FromBody] returns 400 or 415 depending on content type header absence
        // Let's send a specific request to ensure we hit the method or model validation
        var emptyContent = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response2 = await client.PostAsync("/api/suma", emptyContent);

        Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
    }

    private record SumaResult(int Result);
}

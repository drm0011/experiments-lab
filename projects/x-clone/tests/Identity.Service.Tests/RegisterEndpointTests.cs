using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Identity.Service.Tests;

public class IdentityApiFactory : WebApplicationFactory<Program>
{
    public static string TestConnectionString =>
        Environment.GetEnvironmentVariable("TEST_DB_CONNECTION")
        ?? "Host=localhost;Port=5433;Database=identity_test_db;Username=xclone;Password=xclone-dev";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:IdentityDb", TestConnectionString);
        builder.UseSetting("Jwt:Key", "test-signing-key-0123456789abcdef0123456789abcdef");
    }
}

public class RegisterEndpointTests : IClassFixture<IdentityApiFactory>
{
    private readonly IdentityApiFactory _factory;

    public RegisterEndpointTests(IdentityApiFactory factory)
    {
        _factory = factory;
    }

    private string Unique(string prefix) => $"{prefix}-{Guid.NewGuid():N}"[..Math.Min(prefix.Length + 9, 31)];

    [Fact]
    public async Task Register_returns_200_with_token_for_valid_request()
    {
        HttpClient client = _factory.CreateClient();
        string username = Unique("user");
        string email = $"{username}@example.com";

        HttpResponseMessage response = await client.PostAsJsonAsync("/register",
            new { username, email, password = "password123" });
        RegisterResponse body = (await response.Content.ReadFromJsonAsync<RegisterResponse>())!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(username, body.Username);
        Assert.False(string.IsNullOrEmpty(body.Token));

        JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(body.Token);
        Assert.Equal(body.Id.ToString(), token.Subject);
    }

    [Fact]
    public async Task Register_returns_409_for_duplicate_username()
    {
        HttpClient client = _factory.CreateClient();
        string username = Unique("dup");
        string email = $"{username}@example.com";

        await client.PostAsJsonAsync("/register", new { username, email, password = "password123" });
        HttpResponseMessage duplicate = await client.PostAsJsonAsync("/register",
            new { username, email = "other@example.com", password = "password123" });

        Assert.Equal(HttpStatusCode.Conflict, duplicate.StatusCode);
    }

    [Fact]
    public async Task Register_returns_409_for_duplicate_email()
    {
        HttpClient client = _factory.CreateClient();
        string email = $"{Unique("mail")}@example.com";

        await client.PostAsJsonAsync("/register", new { username = Unique("u1"), email, password = "password123" });
        HttpResponseMessage duplicate = await client.PostAsJsonAsync("/register",
            new { username = Unique("u2"), email, password = "password123" });

        Assert.Equal(HttpStatusCode.Conflict, duplicate.StatusCode);
    }

    [Theory]
    [InlineData("ab", "a@example.com", "password123")]
    [InlineData("validuser", "not-an-email", "password123")]
    [InlineData("validuser", "a@example.com", "short")]
    public async Task Register_returns_400_for_invalid_input(string username, string email, string password)
    {
        HttpClient client = _factory.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync("/register",
            new { username, email, password });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private record RegisterResponse(Guid Id, string Username, string Email, string Token);
}

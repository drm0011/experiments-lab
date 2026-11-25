using Identity.Service.Contracts;
using Identity.Service.Data;
using Identity.Service.Models;
using Identity.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Identity.Service.Tests;

public class RegistrationServiceTests
{
    private static AppDbContext NewDbContext()
    {
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    private static RegistrationService NewService(AppDbContext db)
        => new(db, new PasswordHasher<User>());

    [Fact]
    public async Task Register_creates_user_with_hashed_password()
    {
        AppDbContext db = NewDbContext();
        RegistrationService service = NewService(db);

        User user = await service.RegisterAsync(
            new RegisterRequest("alice", "alice@example.com", "password123"));

        Assert.Equal("alice", user.Username);
        Assert.Equal("alice@example.com", user.Email);
        Assert.NotEqual("password123", user.PasswordHash);
        Assert.Single(db.Users);
    }

    [Fact]
    public async Task Register_rejects_duplicate_username()
    {
        AppDbContext db = NewDbContext();
        RegistrationService service = NewService(db);
        await service.RegisterAsync(new RegisterRequest("alice", "a@example.com", "password123"));

        await Assert.ThrowsAsync<UsernameTakenException>(() =>
            service.RegisterAsync(new RegisterRequest("alice", "b@example.com", "password123")));
    }

    [Fact]
    public async Task Register_rejects_duplicate_email()
    {
        AppDbContext db = NewDbContext();
        RegistrationService service = NewService(db);
        await service.RegisterAsync(new RegisterRequest("alice", "shared@example.com", "password123"));

        await Assert.ThrowsAsync<EmailTakenException>(() =>
            service.RegisterAsync(new RegisterRequest("bob", "shared@example.com", "password123")));
    }

    [Theory]
    [InlineData("ab", "a@example.com", "password123")]           // username too short
    [InlineData("validuser", "not-an-email", "password123")]     // bad email
    [InlineData("validuser", "a@example.com", "short")]          // password too short
    public async Task Register_rejects_invalid_input(string username, string email, string password)
    {
        AppDbContext db = NewDbContext();
        RegistrationService service = NewService(db);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.RegisterAsync(new RegisterRequest(username, email, password)));
    }

    [Fact]
    public async Task PasswordHash_verifies_against_original_password()
    {
        AppDbContext db = NewDbContext();
        RegistrationService service = NewService(db);
        User user = await service.RegisterAsync(
            new RegisterRequest("alice", "alice@example.com", "password123"));

        PasswordVerificationResult result =
            new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, "password123");

        Assert.Equal(PasswordVerificationResult.Success, result);
    }
}

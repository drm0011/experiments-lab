using Identity.Service.Contracts;
using Identity.Service.Data;
using Identity.Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Service.Services;

public class UsernameTakenException(string username) : Exception($"Username '{username}' is already taken.");

public class EmailTakenException(string email) : Exception($"Email '{email}' is already registered.");

public class RegistrationService(AppDbContext dbContext, PasswordHasher<User> passwordHasher)
{
    public async Task<User> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        string normalizedUsername = request.Username.Trim();
        string normalizedEmail = request.Email.Trim().ToLowerInvariant();

        if (normalizedUsername.Length is < 3 or > 32)
        {
            throw new ArgumentException("Username must be between 3 and 32 characters.");
        }

        if (!normalizedEmail.Contains('@') || normalizedEmail.Length > 254)
        {
            throw new ArgumentException("A valid email address is required.");
        }

        if (string.IsNullOrEmpty(request.Password) || request.Password.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters.");
        }

        if (await dbContext.Users.AnyAsync(u => u.Username == normalizedUsername, cancellationToken))
        {
            throw new UsernameTakenException(normalizedUsername);
        }

        if (await dbContext.Users.AnyAsync(u => u.Email == normalizedEmail, cancellationToken))
        {
            throw new EmailTakenException(normalizedEmail);
        }

        User user = new()
        {
            Id = Guid.NewGuid(),
            Username = normalizedUsername,
            Email = normalizedEmail,
            CreatedAt = DateTime.UtcNow,
            PasswordHash = ""
        };

        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }
}

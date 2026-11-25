using System.Text;
using Identity.Service.Contracts;
using Identity.Service.Data;
using Identity.Service.Models;
using Identity.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityDb")
        ?? throw new InvalidOperationException("Connection string 'IdentityDb' is not configured.")));

builder.Services.AddScoped<PasswordHasher<User>>();
builder.Services.AddScoped<RegistrationService>();
builder.Services.AddSingleton<TokenService>();

string jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key is not configured.");
string jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "x-clone";
string jwtAudience = builder.Configuration["Jwt:Audience"] ?? "x-clone";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    for (int attempt = 1; ; attempt++)
    {
        try
        {
            db.Database.Migrate();
            break;
        }
        catch when (attempt < 10)
        {
            Thread.Sleep(3000);
        }
    }
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();

app.MapGet("/health", () => Results.Ok(new { service = "identity", status = "ok" }));

app.MapPost("/register", async (RegisterRequest request, RegistrationService registration, TokenService tokens, CancellationToken ct) =>
{
    try
    {
        User user = await registration.RegisterAsync(request, ct);
        string token = tokens.Issue(user);
        return Results.Ok(new { user.Id, user.Username, user.Email, token });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
    catch (UsernameTakenException ex)
    {
        return Results.Conflict(new { error = ex.Message });
    }
    catch (EmailTakenException ex)
    {
        return Results.Conflict(new { error = ex.Message });
    }
});

app.Run();

public partial class Program { }

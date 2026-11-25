using Identity.Service.Data;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityDb")
        ?? throw new InvalidOperationException("Connection string 'IdentityDb' is not configured.")));

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

app.MapOpenApi();

app.MapGet("/health", () => Results.Ok(new { service = "identity", status = "ok" }));

app.MapGet("/", () => Results.Ok(new { service = "identity", description = "X-clone identity service" }));

app.Run();

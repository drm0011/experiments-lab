var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

app.UseCors();

app.MapGet("/health", () => Results.Ok(new { service = "orders", status = "ok" }));

app.MapGet("/orders", () => Results.Ok(new[]
{
    new { id = 1, itemId = 1, quantity = 2 },
    new { id = 2, itemId = 3, quantity = 5 }
}));

app.Run();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

app.UseCors();

app.MapGet("/health", () => Results.Ok(new { service = "catalog", status = "ok" }));

app.MapGet("/items", () => Results.Ok(new[]
{
    new { id = 1, name = "Widget", price = 9.99m },
    new { id = 2, name = "Gadget", price = 19.99m },
    new { id = 3, name = "Gizmo", price = 4.49m }
}));

app.Run();

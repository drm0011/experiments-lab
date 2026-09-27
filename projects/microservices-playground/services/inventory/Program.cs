var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

app.UseCors();

var stock = new Dictionary<int, int>
{
    [1] = 10,
    [2] = 5,
    [3] = 0
};

app.MapGet("/health", () => Results.Ok(new { service = "inventory", status = "ok" }));

app.MapGet("/stock", () => Results.Ok(stock.Select(kv => new { itemId = kv.Key, quantity = kv.Value })));

app.MapGet("/stock/{itemId:int}", (int itemId) =>
    stock.TryGetValue(itemId, out var quantity)
        ? Results.Ok(new { itemId, quantity })
        : Results.NotFound(new { error = "unknown item" }));

app.Run();

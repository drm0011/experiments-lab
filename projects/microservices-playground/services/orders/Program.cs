using System.Net.Http.Json;

var orders = new List<Order>
{
    new(1, 1, 2),
    new(2, 3, 5)
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseCors();

var inventoryUrl = builder.Configuration["INVENTORY_URL"] ?? "http://localhost:8083";

app.MapGet("/health", () => Results.Ok(new { service = "orders", status = "ok" }));

app.MapGet("/orders", () => Results.Ok(orders));

app.MapPost("/orders", async (IHttpClientFactory http, OrderRequest request) =>
{
    StockResponse? stock;
    try
    {
        var client = http.CreateClient();
        stock = await client.GetFromJsonAsync<StockResponse>($"{inventoryUrl}/stock/{request.ItemId}");
    }
    catch
    {
        return Results.Json(new { error = "inventory unavailable" }, statusCode: 503);
    }

    if (stock is null || stock.Quantity < request.Quantity)
    {
        return Results.Json(new { error = "insufficient stock" }, statusCode: 409);
    }

    var order = new Order(orders.Count + 1, request.ItemId, request.Quantity);
    orders.Add(order);
    return Results.Ok(order);
});

app.Run();

record OrderRequest(int ItemId, int Quantity);
record StockResponse(int Quantity);
record Order(int Id, int ItemId, int Quantity);

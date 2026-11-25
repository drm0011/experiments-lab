var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors();

app.MapOpenApi();

app.MapGet("/health", () => Results.Ok(new { service = "identity", status = "ok" }));

app.MapGet("/", () => Results.Ok(new { service = "identity", description = "X-clone identity service (skeleton)" }));

app.Run();

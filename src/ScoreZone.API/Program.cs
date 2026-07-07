using ScoreZone.API.Extensions;
using Scalar.AspNetCore;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddDatabaseConfigurations(builder.Configuration);
builder.Services.AddProjectServices();
builder.Services.AddJwtAuthenticationConfiguration(builder.Configuration);
builder.Services.AddRateLimiterPolicies();


builder.Services.AddOpenApi();

builder.Services.AddControllers();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseDataBaseMigration();
await app.UseSeedDataAsync();
app.UseApplicationMiddleware();

app.Run();


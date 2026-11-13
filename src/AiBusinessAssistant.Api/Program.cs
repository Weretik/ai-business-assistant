using AiBusinessAssistant.Api.Modules.Extraction;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();

// Monolith modules are registered here, each with its own extension method.
builder.Services.AddExtractionModule(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapHealthChecks("/health");
app.MapExtractionEndpoints();

app.Run();

// Required by WebApplicationFactory in integration tests.
public partial class Program;

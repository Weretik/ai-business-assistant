using AiBusinessAssistant.Api.Modules.Extraction;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();

// Модули монолита регистрируются здесь, каждый в своём методе-расширении.
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

// Нужен для WebApplicationFactory в интеграционных тестах.
public partial class Program;

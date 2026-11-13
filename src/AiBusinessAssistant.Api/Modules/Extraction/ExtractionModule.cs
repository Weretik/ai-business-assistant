namespace AiBusinessAssistant.Api.Modules.Extraction;

/// <summary>
/// Этап 1. AI Extractor: письмо клиента -> LLM -> валидированный DTO заказа.
/// Реализация появится по ходу модуля 1 курса.
/// </summary>
public static class ExtractionModule
{
    public static IServiceCollection AddExtractionModule(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO (М1): зарегистрировать клиента LLM и сервис извлечения.
        return services;
    }

    public static IEndpointRouteBuilder MapExtractionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/extraction").WithTags("Extraction");

        // TODO (М1): POST /api/extraction/orders — принять текст письма, вернуть OrderDto.
        group.MapPost("/orders", () => TypedResults.Problem(
                title: "Not implemented yet",
                detail: "AI Extractor будет реализован в рамках модуля 1.",
                statusCode: StatusCodes.Status501NotImplemented))
            .WithName("ExtractOrder");

        return app;
    }
}

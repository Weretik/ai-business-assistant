namespace AiBusinessAssistant.Api.Modules.Extraction;

/// <summary>
/// Stage 1. AI Extractor: customer email -> LLM -> validated order DTO.
/// Not implemented yet.
/// </summary>
public static class ExtractionModule
{
    public static IServiceCollection AddExtractionModule(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO (stage 1): register the LLM client and the extraction service.
        return services;
    }

    public static IEndpointRouteBuilder MapExtractionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/extraction").WithTags("Extraction");

        // TODO (stage 1): POST /api/extraction/orders - take the email text, return an OrderDto.
        group.MapPost("/orders", () => TypedResults.Problem(
                title: "Not implemented yet",
                detail: "The AI Extractor will be implemented in stage 1.",
                statusCode: StatusCodes.Status501NotImplemented))
            .WithName("ExtractOrder");

        return app;
    }
}

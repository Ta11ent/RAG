using AI_service.Endpoints;
using AI_service.Shared.Outcome;
using Microsoft.AspNetCore.Mvc;

namespace AI_service.Feature.GetContext
{
    public class ContentEndpoint : IEndpoint
    {
        public record Query(string content);
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Content", async (
                [FromBody] Query data,
                [FromServices] IRequestHandler<GetContentQuery, ContentResponse> handler,
                CancellationToken token) =>
            {
                GetContentQuery query = new(data.content);

                var result = await handler.Handle(query, token);

                if (result.IsSuccess)
                    return Results.Ok(result);

                return result.Problem();

            })
            .WithTags(Tags.Content);
        }
    }
}

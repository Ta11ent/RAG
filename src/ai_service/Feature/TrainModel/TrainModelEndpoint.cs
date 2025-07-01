using AI_service.Endpoints;
using AI_service.Shared.Outcome;
using Microsoft.AspNetCore.Mvc;

namespace AI_service.Feature.TrainModel
{
    internal sealed class TrainModelEndpoint : IEndpoint
    {
        internal sealed record Request(Guid tagId, string content);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("TrainModel", async (
                [FromBody] Request request,
                [FromServices] IRequestHandler<TrainModelCommand> handler,
                CancellationToken token) =>
            {
                TrainModelCommand command = new(request.tagId, request.content);

                Result result = await handler.Handle(command, token);

                if (result.IsSuccess)
                    return Results.Ok();

                return result.Problem();
            })
            .WithTags(Tags.TrainModel);
        }
    }
}

using AI_service.Endpoints;
using AI_service.Shared.Outcome;

namespace AI_service.Feature.TrainModel
{
    internal sealed class TrainModelEndpoint : IEndpoint
    {
        internal sealed record Request(int tag, string content);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("TrainModel", async (
                Request request,
                IRequestHandler<TrainModelCommand> handler,
                CancellationToken token) =>
            {
                TrainModelCommand command = new(request.tag, request.content);

                Result result = await handler.Handle(command, token);

                if (result.IsSuccess)
                    return Results.Ok();

                return result.Problem();
            })
            .WithTags(Tags.TrainModel);
        }
    }
}

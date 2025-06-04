using AI_service.Shared.Outcome;

namespace AI_service.Feature.GetContext
{
    internal sealed class GetContentQueryHandler : IRequestHandler<GetContentQuery, ContentResponse>
    {
        public Task<Result<ContentResponse>> Handle(GetContentQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

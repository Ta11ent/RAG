namespace AI_service.Feature.GetContext
{
    public sealed record GetContentQuery(string context) : IRequest<ContentResponse>;
}

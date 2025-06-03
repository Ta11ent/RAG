using AI_service.Shared.Outcome;

namespace AI_service.Shared.Messaging
{
    internal interface IRequestHandler<in TRequest>
        where TRequest : IRequest
    {
        Task<Result> Handle(TRequest request, CancellationToken cancellationToken);
    }
    internal interface IRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}

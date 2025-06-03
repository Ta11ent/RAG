using AI_service.Shared.Outcome;

namespace AI_service.Shared.Messaging
{
    internal interface IRequest : IRequest<Result> { }

    internal interface IRequest<TResponse> : IBaseRequest { }

    internal interface IBaseRequest { }
}

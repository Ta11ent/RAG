using AI_service.Shared.Configuration;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Options;
using Polly;

namespace AI_service.Shared.Behaviors
{
    internal class RetryInceraptor : Interceptor
    {
        private readonly ILogger<RetryInceraptor> _logger;
        private readonly GrpcMlServiceSettings _option;

        internal RetryInceraptor(
            ILogger<RetryInceraptor> logger,
            IOptions<GrpcMlServiceSettings> options)
        {
            _logger = logger;
            _option = options.Value;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
             TRequest request,
             ClientInterceptorContext<TRequest, TResponse> context,
             AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
             where TRequest : class
             where TResponse : class
        {
            var retryPolicy = Policy
                .Handle<RpcException>(ex =>
                    ex.StatusCode == StatusCode.Unavailable ||
                    ex.StatusCode == StatusCode.DeadlineExceeded)
                .WaitAndRetryAsync(
                    retryCount: _option.RetryCount,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(_option.RetryDelaySeconds, attempt)),
                    onRetry: (exception, timeSpan, retryCount, _) =>
                    {
                        _logger.LogError(exception, $"gRPC retry: {retryCount} after delay {timeSpan}");
                    });


            async Task<TResponse> ExecuteWithRetry()
            {
                AsyncUnaryCall<TResponse>? call = null;

                return await retryPolicy.ExecuteAsync(async () =>
                {
                    call = continuation(request, context);
                    return await call.ResponseAsync;
                });
            }

            var initialCall = continuation(request, context);

            return new AsyncUnaryCall<TResponse>(
                ExecuteWithRetry(),
                initialCall.ResponseHeadersAsync,
                initialCall.GetStatus,
                initialCall.GetTrailers,
                initialCall.Dispose);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace PortfolioMS.Server.Application.Abstract
{
    public class Mediator(IServiceProvider serviceProvider) : IMediator
    {
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var requestType = request.GetType();

            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
            var handler = serviceProvider.GetService(handlerType)
                ?? throw new InvalidOperationException($"No handler registered for {requestType.Name}.");

            var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, typeof(TResponse));
            var behaviors = serviceProvider.GetServices(behaviorType).Reverse().ToArray();

            RequestHandlerDelegate<TResponse> handlerDelegate = () =>
            {
                var method = handlerType.GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.Handle))!;
                return (Task<TResponse>)method.Invoke(handler, [request, cancellationToken])!;
            };

            foreach (var behavior in behaviors)
            {
                var next = handlerDelegate;
                var behaviorHandleMethod = behaviorType.GetMethod(nameof(IPipelineBehavior<IRequest<TResponse>, TResponse>.Handle))!;
                handlerDelegate = () => (Task<TResponse>)behaviorHandleMethod.Invoke(behavior, [request, cancellationToken, next])!;
            }

            return await handlerDelegate();
        }
    }
}

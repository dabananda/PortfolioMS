using FluentValidation;
using PortfolioMS.Server.Application.Abstract;
using PortfolioMS.Server.Application.Common;
using Error = PortfolioMS.Server.Application.Common.Error;

namespace PortfolioMS.Server.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!validators.Any())
                return await next();

            var failures = new List<FluentValidation.Results.ValidationFailure>();

            foreach (var validator in validators)
            {
                var result = await validator.ValidateAsync(request, cancellationToken);
                if (!result.IsValid)
                    failures.AddRange(result.Errors);
            }

            if (failures.Count == 0)
                return await next();

            var description = string.Join(" | ", failures.Select(f => f.ErrorMessage));
            var error = new Error("Validation.Failed", description);

            return CreateValidationFailureResponse(error);
        }

        private static TResponse CreateValidationFailureResponse(Error error)
        {
            var responseType = typeof(TResponse);

            if (responseType == typeof(Result))
                return (TResponse)(object)Result.Failure(error);

            if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Result<>))
            {
                var failureMethod = responseType.GetMethod(nameof(Result.Failure), [typeof(Error)])!;
                return (TResponse)failureMethod.Invoke(null, [error])!;
            }

            throw new InvalidOperationException($"ValidationBehavior only supports Result or Result<T> responses. {responseType.Name} is not supported.");
        }
    }
}

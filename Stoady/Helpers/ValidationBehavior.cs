using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using MediatR;

namespace Stoady.Helpers
{
    public sealed class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken ct,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any() is false)
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var errorsDictionary = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) =>
                        new
                        {
                            Key = propertyName,
                            Values = errorMessages
                                .Distinct()
                                .ToArray()
                        })
                .ToDictionary(
                    x => x.Key,
                    x => x.Values);

            if (errorsDictionary.Any())
            {
                throw new ValidationException(
                    string.Join("\r\n",
                        errorsDictionary
                            .Select(x => x.Key + ": " + string.Join(", ", x.Value))));
            }

            return await next();
        }
    }
}

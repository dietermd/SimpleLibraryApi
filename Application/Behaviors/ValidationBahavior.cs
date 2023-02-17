using FluentValidation;
using MediatR;
using SimpleLibraryApi.Application.Abstractions;
using ValidationException = SimpleLibraryApi.Application.Exceptions.ValidationException;

namespace SimpleLibraryApi.Application.Behaviors
{
    public sealed class ValidationBahavior<TRequest, Tresponse> : IPipelineBehavior<TRequest, Tresponse>
        where TRequest : class, ICommand<Tresponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBahavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<Tresponse> Handle(TRequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var errorsDictionary = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);

            if (errorsDictionary.Any())
            {
                throw new ValidationException(errorsDictionary);
            }
            return await next();
        }
    }
}

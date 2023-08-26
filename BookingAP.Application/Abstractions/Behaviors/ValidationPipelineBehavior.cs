using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BookingAP.Application.Abstractions.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : IErrorOr
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var validationErrors = _validators
                .Select(validator => validator.Validate(context))
                .Where(validationResult => validationResult.Errors.Any())
                .SelectMany(validationResult => validationResult.Errors)
                .ToList()
                .ConvertAll(validationFailure =>
                            Error.Validation(validationFailure.PropertyName,
                                             validationFailure.ErrorMessage));

            if(validationErrors is null || !validationErrors.Any())
            {
                return await next();
            }


            return (dynamic)validationErrors;
        }
    }
}

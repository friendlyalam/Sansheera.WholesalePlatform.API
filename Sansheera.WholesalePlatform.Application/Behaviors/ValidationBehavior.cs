using FluentValidation;
using MediatR;
namespace Sansheera.WholesalePlatform.Application.Behaviors
{

    public sealed class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(
            IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            foreach (var validator in _validators)
            {
                await validator.ValidateAndThrowAsync(
                    request, cancellationToken);
            }

            return await next();
        }
    }
}

//MediatR pipeline behavior command handler se pehle validators run karega. Isse har handler mein validation manually call nahi karni padegi.
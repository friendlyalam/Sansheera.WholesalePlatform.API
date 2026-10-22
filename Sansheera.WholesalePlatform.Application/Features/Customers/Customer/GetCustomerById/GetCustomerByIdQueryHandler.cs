using MediatR;
using Sansheera.WholesalePlatform.Application.Features.Customers.Customer.Abstractions;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.Customer.GetCustomerById
{
    public sealed class GetCustomerByIdQueryHandler
    : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdResult?>
    {
        private readonly ICustomerRepository _repository;

        public GetCustomerByIdQueryHandler(
            ICustomerRepository repository)
        {
            _repository = repository;
        }

        public Task<GetCustomerByIdResult?> Handle(
            GetCustomerByIdQuery request,
            CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(
                request.Id,
                cancellationToken);
        }
    }
}

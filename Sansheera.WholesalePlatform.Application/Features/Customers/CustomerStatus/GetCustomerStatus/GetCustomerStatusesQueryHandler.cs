using MediatR;
using Sansheera.WholesalePlatform.Application.Features.Customers.CustomerStatus.Abstractions;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.CustomerStatus.GetCustomerStatus
{
    public sealed class GetCustomerStatusesQueryHandler
     : IRequestHandler<GetCustomerStatusQuery, IReadOnlyList<CustomerStatusDto>>
    {
        private readonly ICustomerStatusRepository _customerStatusRepository;

        public GetCustomerStatusesQueryHandler(
            ICustomerStatusRepository customerStatusRepository)
        {
            _customerStatusRepository = customerStatusRepository;
        }

        public Task<IReadOnlyList<CustomerStatusDto>> Handle(
            GetCustomerStatusQuery request,
            CancellationToken cancellationToken)
        {
            return _customerStatusRepository.GetActiveAsync(cancellationToken);
        }
    }
}

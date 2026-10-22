using MediatR;
using Sansheera.WholesalePlatform.Application.Features.Customers.BusinessType.Abstractions;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.BusinessType.GetBusinessType
{
    public sealed class GetBusinessTypeQueryHandler
    : IRequestHandler<GetBusinessTypeQuery, IReadOnlyList<BusinessTypeDto>>
    {
        private readonly IBusinessTypeRepository _businessTypeRepository;

        public GetBusinessTypeQueryHandler(
            IBusinessTypeRepository businessTypeRepository)
        {
            _businessTypeRepository = businessTypeRepository;
        }

        public Task<IReadOnlyList<BusinessTypeDto>> Handle(
            GetBusinessTypeQuery request,
            CancellationToken cancellationToken)
        {
            return _businessTypeRepository.GetActiveAsync(cancellationToken);
        }
    }
}

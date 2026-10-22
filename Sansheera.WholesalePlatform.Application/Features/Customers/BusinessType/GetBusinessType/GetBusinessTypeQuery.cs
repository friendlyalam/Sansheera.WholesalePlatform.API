using MediatR;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.BusinessType.GetBusinessType
{
    public sealed record GetBusinessTypeQuery
    : IRequest<IReadOnlyList<BusinessTypeDto>>;
}

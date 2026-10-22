using MediatR;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.CustomerStatus.GetCustomerStatus
{
    public sealed record GetCustomerStatusQuery
    : IRequest<IReadOnlyList<CustomerStatusDto>>;
}

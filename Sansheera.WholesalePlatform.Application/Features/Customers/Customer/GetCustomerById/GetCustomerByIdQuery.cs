using MediatR;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.Customer.GetCustomerById
{
    public sealed record GetCustomerByIdQuery(Guid Id)
    : IRequest<GetCustomerByIdResult?>;
}

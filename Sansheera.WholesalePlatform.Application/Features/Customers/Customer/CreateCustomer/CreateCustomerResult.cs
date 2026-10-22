namespace Sansheera.WholesalePlatform.Application.Features.Customers.Customer.CreateCustomer
{
    public sealed record CreateCustomerResult(
    Guid Id,
    long CustomerCode,
    string BusinessName,
    short BusinessTypeId,
    short StatusId,
    string PhoneNumber
);
}

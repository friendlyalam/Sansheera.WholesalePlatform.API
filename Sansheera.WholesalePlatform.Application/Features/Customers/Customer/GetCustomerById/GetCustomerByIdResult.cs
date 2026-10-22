namespace Sansheera.WholesalePlatform.Application.Features.Customers.Customer.GetCustomerById
{
    public sealed record GetCustomerByIdResult(
    Guid Id,
    string BusinessName,
    string ContactPerson,
    string PhoneNumber,
    string? Email,
    string? BusinessAddress,
    bool IsActive,
    DateTime CreatedAt);
}

namespace Sansheera.WholesalePlatform.Application.Features.Customers.CustomerStatus.GetCustomerStatus
{
    public sealed record CustomerStatusDto(
     short Id,
     string Code,
     string Name,
     bool IsActive,
     DateTime? CreatedAt);
}

namespace Sansheera.WholesalePlatform.Application.Features.Customers.BusinessType.GetBusinessType
{
    public sealed record BusinessTypeDto(
    short Id,
    string Code,
    string Name,
    bool IsActive,
    DateTime CreatedAt);
}

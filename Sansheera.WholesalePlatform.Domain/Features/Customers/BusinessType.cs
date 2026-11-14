using Sansheera.WholesalePlatform.Domain.Common;

namespace Sansheera.WholesalePlatform.Domain.Features.Customers
{
    public sealed class BusinessType : BaseEntity<BusinessType>
    {
        private BusinessType()
        {
            // EF Core
        }

        public short Id { get; private set; }

        public string Code { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        public bool IsActive { get; private set; }
    }
}

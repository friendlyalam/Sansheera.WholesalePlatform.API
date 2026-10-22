using Sansheera.WholesalePlatform.Domain.Common;

namespace Sansheera.WholesalePlatform.Domain.Customers
{
    public sealed class CustomerStatus: BaseEntity<CustomerStatus>
    {
        private CustomerStatus()
        {
        }

        public short Id { get; private set; }

        public string Code { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        public bool IsActive { get; private set; }

    }
}

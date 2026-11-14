using Sansheera.WholesalePlatform.Domain.Common;

namespace Sansheera.WholesalePlatform.Domain.Features.Customers
{
    public sealed class UserAccount: BaseEntity<UserAccount>
    {
        private UserAccount()
        {
            // EF Core
        }

        public UserAccount(Guid customerId, string phoneNumber, string userType)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            UserType = userType;
            PhoneNumber = phoneNumber;
            IsPhoneVerified = false;
            IsActive = true;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; private set; }
        public string UserType { get; private set; } = string.Empty;

        public Guid? CustomerId { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public string PhoneNumber { get; private set; } = string.Empty;

        public bool IsPhoneVerified { get; private set; }
        public DateTimeOffset? PhoneVerifiedAt { get; private set; }

        public bool IsActive { get; private set; }
        public DateTime? LastLoginAt { get; private set; }

    }
}


using Sansheera.WholesalePlatform.Domain.Common;

namespace Sansheera.WholesalePlatform.Domain.Customers
{

    public sealed class Customer : BaseEntity<Customer>
    {
        private Customer()
        {
            // EF Core
        }

        public Customer(
            string businessName,
            short businessTypeId,
            short statusId,
            string contactPersonName,
            string phoneNumber,
            string? email,
            string addressLine1,
            string? addressLine2,
            string? landmark,
            string city,
            string? district,
            string stateName,
            string postalCode,
            string countryCode,
            string? gstin,
            string? legalBusinessName,
            string? gstStateCode)
        {
            Id = Guid.NewGuid();

            BusinessName = businessName;
            BusinessTypeId = businessTypeId;
            StatusId = statusId;
            ContactPersonName = contactPersonName;
            PhoneNumber = phoneNumber;
            Email = email;

            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            Landmark = landmark;
            City = city;
            District = district;
            StateName = stateName;
            PostalCode = postalCode;
            CountryCode = countryCode;

            Gstin = gstin;
            LegalBusinessName = legalBusinessName;
            GstStateCode = gstStateCode;

            IsPhoneVerified = false;
            IsEmailVerified = false;
            IsGstinVerified = false;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public long CustomerCode { get; private set; }

        public string BusinessName { get; private set; } = string.Empty;
        public short BusinessTypeId { get; private set; }
        public short StatusId { get; set; }

        public string ContactPersonName { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string? Email { get; private set; }

        public string AddressLine1 { get; private set; } = string.Empty;
        public string? AddressLine2 { get; private set; }
        public string? Landmark { get; private set; }
        public string City { get; private set; } = string.Empty;
        public string? District { get; private set; }
        public string StateName { get; private set; } = string.Empty;
        public string PostalCode { get; private set; } = string.Empty;
        public string CountryCode { get; private set; } = "IN";

        public string? Gstin { get; private set; }
        public string? LegalBusinessName { get; private set; }
        public string? GstStateCode { get; private set; }

        public bool IsPhoneVerified { get; private set; }
        public DateTime? PhoneVerifiedAt { get; private set; }
        public bool IsEmailVerified { get; private set; }
        public DateTime? EmailVerifiedAt { get; private set; }
        public bool IsGstinVerified { get; private set; }
        public DateTime? GstinVerifiedAt { get; private set; }

        public bool IsActive { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
    }
}

//we can add in future inside the customer folder
//SANSHEERA ke `Domain/Customers/` folder mein future mein ye files add kar sakte ho — har ek ka ek simple example:

//1. `Customer.cs` — Entity: Customer ka business name, phone aur `IsActive`.
//2. `CustomerType.cs` — Enum: `Retailer`, `Wholesaler`, `Distributor`, `Hotel`, `Restaurant`.
//3. `CustomerStatus.cs` — Enum: `PendingApproval`, `Active`, `Suspended`.
//4. `CustomerConstants.cs` — Constants: `public const int MaxPhoneNumberLength = 15;`
//5. `CustomerAddress.cs` — Value Object: `Street`, `City`, `PostalCode`.
//6. `CustomerDomainService.cs` — Domain Service: Check karna ki customer credit ke liye eligible hai ya nahi.
//7. `CustomerRules.cs` — Business Rules: Rule ki suspended customer naya order place nahi kar sakta.
//8. `CustomerSpecifications.cs` — Specifications: Aise customers identify karna jo active hain aur credit limit ke andar hain.
//9. `CustomerCreatedDomainEvent.cs` — Domain Event: Customer create hone par `CustomerCreatedDomainEvent` raise karna.

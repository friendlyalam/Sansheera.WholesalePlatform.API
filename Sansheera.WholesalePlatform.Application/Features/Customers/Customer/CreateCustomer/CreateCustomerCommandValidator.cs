using FluentValidation;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.Customer.CreateCustomer
{
    public sealed class CreateCustomerCommandValidator
    : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.BusinessName)
                .NotEmpty()
                .WithMessage("Business name is required.")
                .MaximumLength(200)
                .WithMessage("Business name cannot exceed 200 characters.");

            RuleFor(x => x.BusinessTypeId)
                .GreaterThan((short)0)
                .WithMessage("Please select a valid business type.");

            RuleFor(x => x.ContactPersonName)
                .NotEmpty()
                .WithMessage("Contact person name is required.")
                .MaximumLength(150)
                .WithMessage("Contact person name cannot exceed 150 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^[6-9]\d{9}$")
                .WithMessage("Enter a valid 10-digit Indian mobile number.");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Enter a valid email address.");

            RuleFor(x => x.AddressLine1)
                .NotEmpty()
                .WithMessage("Address line 1 is required.")
                .MaximumLength(200)
                .WithMessage("Address line 1 cannot exceed 200 characters.");

            RuleFor(x => x.AddressLine2)
                .MaximumLength(200)
                .WithMessage("Address line 2 cannot exceed 200 characters.");

            RuleFor(x => x.Landmark)
                .MaximumLength(150)
                .WithMessage("Landmark cannot exceed 150 characters.");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("City is required.")
                .MaximumLength(100)
                .WithMessage("City cannot exceed 100 characters.");

            RuleFor(x => x.District)
                .MaximumLength(100)
                .WithMessage("District cannot exceed 100 characters.");

            RuleFor(x => x.StateName)
                .NotEmpty()
                .WithMessage("State is required.")
                .MaximumLength(100)
                .WithMessage("State cannot exceed 100 characters.");

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .WithMessage("Postal code is required.")
                .MaximumLength(20)
                .WithMessage("Postal code cannot exceed 20 characters.");

            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .WithMessage("Country code is required.")
                .Matches(@"^[A-Za-z]{2}$")
                .WithMessage("Country code must contain exactly two letters.");

            RuleFor(x => x.Gstin)
                .Matches(@"^[0-9A-Z]{15}$")
                .When(x => !string.IsNullOrWhiteSpace(x.Gstin))
                .WithMessage("GSTIN must contain 15 uppercase letters or digits.");

            RuleFor(x => x.LegalBusinessName)
                .MaximumLength(200)
                .WithMessage("Legal business name cannot exceed 200 characters.");

            RuleFor(x => x.GstStateCode)
                .Matches(@"^\d{2}$")
                .When(x => !string.IsNullOrWhiteSpace(x.GstStateCode))
                .WithMessage("GST state code must contain exactly two digits.");
        }
    }
}

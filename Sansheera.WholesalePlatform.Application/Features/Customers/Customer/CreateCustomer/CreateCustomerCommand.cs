using MediatR;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.Customer.CreateCustomer
{
    public sealed record CreateCustomerCommand(
    string BusinessName,
    short BusinessTypeId,
    string ContactPersonName,
    string PhoneNumber,
    string? Email,
    string AddressLine1,
    string? AddressLine2,
    string? Landmark,
    string City,
    string? District,
    string StateName,
    string PostalCode,
    string CountryCode,
    string? Gstin,
    string? LegalBusinessName,
    string? GstStateCode
) : IRequest<CreateCustomerResult>;
}

//Yahan request DTO aur command ka data similar hai, lekin purpose different hai:
//-CreateCustomerRequest — HTTP / API contract.(UI to API)
//- CreateCustomerCommand — application use-case contract.
//- CreateCustomerResponse — application result.
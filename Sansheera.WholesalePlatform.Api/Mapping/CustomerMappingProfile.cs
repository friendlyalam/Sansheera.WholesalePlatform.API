using AutoMapper;
using Sansheera.WholesalePlatform.Application.Features.Customers.Customer.CreateCustomer;
using Sansheera.WholesalePlatform.Domain.Features.Customers;

namespace Sansheera.WholesalePlatform.Api.Mapping
{
    public sealed class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {

            CreateMap<CreateCustomerCommand, Customer>();

        }
    }
}

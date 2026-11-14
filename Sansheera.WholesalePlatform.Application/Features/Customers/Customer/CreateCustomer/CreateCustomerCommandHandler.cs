using AutoMapper;
using MediatR;
using Sansheera.WholesalePlatform.Application.Abstractions.Persistence;
using Sansheera.WholesalePlatform.Application.Features.Customers.Customer.Abstractions;
using Sansheera.WholesalePlatform.Domain.Enums;
using Sansheera.WholesalePlatform.Domain.Features.Customers;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.Customer.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler
    : IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CreateCustomerCommandHandler(
            IMapper mapper,
            ICustomerRepository customerRepository,
            IUserAccountRepository userAccountRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _userAccountRepository = userAccountRepository;
        }

        public async Task<CreateCustomerResult> Handle(
            CreateCustomerCommand request,
            CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Domain.Features.Customers.Customer>(request);
            customer.SetId(Guid.NewGuid());
            customer.StatusId = (short)CustomerStatusType.PendingVerification;
            var userAccount = new UserAccount(customer.Id, customer.PhoneNumber, "customer");
            await _customerRepository.AddAsync(customer, cancellationToken);
            await _userAccountRepository.AddAsync(userAccount, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateCustomerResult(
        customer.Id,
        customer.CustomerCode,
        customer.BusinessName,
        customer.BusinessTypeId,
        customer.StatusId,
        customer.PhoneNumber
    );
        }
    }
}

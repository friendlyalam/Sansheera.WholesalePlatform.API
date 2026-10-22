using AutoMapper;
using MediatR;
using Sansheera.WholesalePlatform.Application.Abstractions.Persistence;
using Sansheera.WholesalePlatform.Application.Features.Customers.Customer.Abstractions;
using Sansheera.WholesalePlatform.Application.Features.Customers.CustomerStatus.Abstractions;

namespace Sansheera.WholesalePlatform.Application.Features.Customers.Customer.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler
    : IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerStatusRepository _customerStatusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(
            IMapper mapper,
            ICustomerRepository customerRepository,
            ICustomerStatusRepository customerStatusRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _customerStatusRepository = customerStatusRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCustomerResult> Handle(
            CreateCustomerCommand request,
            CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Domain.Customers.Customer>(request);
            var status = await _customerStatusRepository.GetByCodeAsync(Domain.Constants.CustomerStatus.Pending, cancellationToken);

            if (status == null)
            {
                throw new InvalidOperationException("Default 'PENDING' status not found in database.");
            }
            customer.StatusId = status.Id;
            await _customerRepository.AddAsync(customer, cancellationToken);
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

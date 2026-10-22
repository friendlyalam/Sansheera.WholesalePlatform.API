using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Sansheera.WholesalePlatform.Api.Contracts.Common;
using Sansheera.WholesalePlatform.Api.Resources;
using Sansheera.WholesalePlatform.Application.Features.Customers.Customer.CreateCustomer;

namespace Sansheera.WholesalePlatform.Api.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public sealed class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Messages> _messages;

        public CustomerController(
            IMediator mediator,
            IMapper mapper,
            IStringLocalizer<Messages> messages)
        {
            _mediator = mediator;
            _mapper = mapper;
            _messages = messages;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateCustomerResult>>> Create(
    [FromBody] CreateCustomerCommand createCustomerCommand,
    CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createCustomerCommand, cancellationToken);
            var body = ApiResponse<CreateCustomerResult>.Success(
                result,
                _messages[ResourceCodes.CustomerCreated].Value,
                HttpContext.TraceIdentifier);

            return StatusCode(StatusCodes.Status201Created, body);
        }



        //    //GET https://localhost:{port}/api/customers/{customer-guid}
        //    [HttpGet("{id:guid}")]
        //    [ProducesResponseType(typeof(Contracts.Customers.CreateCustomerResponse), StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    public async Task<ActionResult<Contracts.Customers.CreateCustomerResponse>> GetById(
        //Guid id,
        //CancellationToken cancellationToken)
        //    {
        //        //var result = await _mediator.Send(
        //        //    new GetCustomerByIdQuery(id),
        //        //    cancellationToken);

        //        //if (result is null)
        //        //{
        //        //    return NotFound();
        //        //}

        //        //var response = new CreateCustomerResponse(
        //        //    result.Id,
        //        //    result.BusinessName,
        //        //    result.ContactPerson,
        //        //    result.PhoneNumber,
        //        //    result.Email,
        //        //    result.BusinessAddress,
        //        //    result.IsActive,
        //        //    result.CreatedAt);

        //        return null;// Ok(response);
        //    }
    }
}


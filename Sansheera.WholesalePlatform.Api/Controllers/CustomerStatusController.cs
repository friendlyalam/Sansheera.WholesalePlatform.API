using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sansheera.WholesalePlatform.Api.Contracts.Common;
using Sansheera.WholesalePlatform.Application.Features.Customers.CustomerStatus.GetCustomerStatus;

namespace Sansheera.WholesalePlatform.Api.Controllers
{
    [ApiController]
    [Route("api/customer-status")]
    public sealed class CustomerStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<CustomerStatusDto>>>> Get(
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetCustomerStatusQuery(),
                cancellationToken);

            var response = ApiResponse<IReadOnlyList<CustomerStatusDto>>.Success(
                result,
                "Customer status retrieved successfully.",
                HttpContext.TraceIdentifier);

            return Ok(response);
        }
    }
}

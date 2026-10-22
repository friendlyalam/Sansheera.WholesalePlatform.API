using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sansheera.WholesalePlatform.Api.Contracts.Common;
using Sansheera.WholesalePlatform.Application.Features.Customers.BusinessType.GetBusinessType;

namespace Sansheera.WholesalePlatform.Api.Controllers
{
    [Route("api/business-type")]
    public sealed class BusinessTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<BusinessTypeDto>>>> Get(
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetBusinessTypeQuery(),
                cancellationToken);

            var response =
                ApiResponse<IReadOnlyList<BusinessTypeDto>>.Success(
                    result,
                    "Business type retrieved successfully.",
                    HttpContext.TraceIdentifier);

            return Ok(response);
        }
    }
}

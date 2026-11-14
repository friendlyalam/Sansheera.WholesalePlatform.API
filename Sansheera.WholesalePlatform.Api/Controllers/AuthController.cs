using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Sansheera.WholesalePlatform.Api.Contracts.Common;
using Sansheera.WholesalePlatform.Api.Resources;
using Sansheera.WholesalePlatform.Application.Features.Authentication.SendOtp;

namespace Sansheera.WholesalePlatform.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<Messages> _messages;

        public AuthController(
            IMediator mediator,
            IStringLocalizer<Messages> messages)
        {
            _mediator = mediator;
            _messages = messages;
        }

        [HttpPost("send-otp")]
        public async Task<ActionResult<ApiResponse<SendOtpResult>>> SendOtp(
            [FromBody] SendOtpCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);

            var body = ApiResponse<SendOtpResult>.Success(
                new SendOtpResult(true),
                _messages[ResourceCodes.OtpRequested].Value,
                HttpContext.TraceIdentifier);

            return Ok(body);
        }
    }
}

using MediatR;

namespace Sansheera.WholesalePlatform.Application.Features.Authentication.SendOtp
{
    public sealed record SendOtpCommand(string PhoneNumber)
    : IRequest<SendOtpResult>;
}

using FluentValidation;

namespace Sansheera.WholesalePlatform.Application.Features.Authentication.SendOtp
{
    public sealed class SendOtpCommandValidator
     : AbstractValidator<SendOtpCommand>
    {
        public SendOtpCommandValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .MaximumLength(15)
                .WithMessage("Phone number cannot exceed 15 characters.")
                .Matches(@"^\+?[1-9]\d{7,14}$")
                .WithMessage("Enter a valid phone number.");
        }
    }
}

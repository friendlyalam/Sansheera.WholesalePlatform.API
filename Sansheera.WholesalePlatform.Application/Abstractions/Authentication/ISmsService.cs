namespace Sansheera.WholesalePlatform.Application.Abstractions.Authentication
{
    public interface ISmsService
    {
        Task SendOtpAsync(
            string phoneNumber,
            string otp,
            CancellationToken cancellationToken);
    }
}

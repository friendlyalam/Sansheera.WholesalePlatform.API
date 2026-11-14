namespace Sansheera.WholesalePlatform.Application.Abstractions.Authentication
{
    public interface IMockSmsInbox
    {
        void Store(string phoneNumber, string otp);

        string? GetLatestOtp(string phoneNumber);
    }
}

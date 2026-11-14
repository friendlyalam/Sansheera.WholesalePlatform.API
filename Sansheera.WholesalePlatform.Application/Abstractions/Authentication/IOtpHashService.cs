namespace Sansheera.WholesalePlatform.Application.Abstractions.Authentication
{
    public interface IOtpHashService
    {
        string Hash(string otp);
    }
}

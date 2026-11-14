using Microsoft.Extensions.Configuration;
using Sansheera.WholesalePlatform.Application.Abstractions.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace Sansheera.WholesalePlatform.Infrastructure.Features.Authentication
{
    public sealed class OtpHashService : IOtpHashService
    {
        private readonly byte[] _key;

        public OtpHashService(IConfiguration configuration)
        {
            var secret = configuration["Otp:HashKey"];

            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new InvalidOperationException(
                    "OTP hashing key is not configured.");
            }

            _key = Encoding.UTF8.GetBytes(secret);
        }

        public string Hash(string otp)
        {
            using var hmac = new HMACSHA256(_key);

            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(otp));

            return Convert.ToHexString(hash);
        }
    }
}

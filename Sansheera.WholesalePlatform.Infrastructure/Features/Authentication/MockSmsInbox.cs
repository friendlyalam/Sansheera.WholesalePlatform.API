using Sansheera.WholesalePlatform.Application.Abstractions.Authentication;
using System.Collections.Concurrent;

namespace Sansheera.WholesalePlatform.Infrastructure.Features.Authentication
{
    public sealed class MockSmsInbox : IMockSmsInbox
    {
        private readonly ConcurrentDictionary<string, string> _messages = new();

        public void Store(string phoneNumber, string otp)
        {
            _messages[phoneNumber] = otp;
        }

        public string? GetLatestOtp(string phoneNumber)
        {
            return _messages.TryGetValue(phoneNumber, out var otp)
                ? otp
                : null;
        }
    }
}

using Sansheera.WholesalePlatform.Application.Abstractions.Authentication;

namespace Sansheera.WholesalePlatform.Infrastructure.Features.Authentication
{
    public sealed class MockSmsService : ISmsService
    {
        private readonly IMockSmsInbox _inbox;

        public MockSmsService(IMockSmsInbox inbox)
        {
            _inbox = inbox;
        }

        public Task SendOtpAsync(
            string phoneNumber,
            string otp,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _inbox.Store(phoneNumber, otp);

            return Task.CompletedTask;
        }
    }
}

using MediatR;
using Sansheera.WholesalePlatform.Application.Abstractions.Authentication;
using Sansheera.WholesalePlatform.Application.Abstractions.Persistence;
using Sansheera.WholesalePlatform.Domain.Authentication;
using System.Security.Cryptography;

namespace Sansheera.WholesalePlatform.Application.Features.Authentication.SendOtp
{
    public sealed class SendOtpCommandHandler
    : IRequestHandler<SendOtpCommand, SendOtpResult>
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IOtpHashService _otpHashService;
        private readonly ISmsService _smsService;
        private readonly IUnitOfWork _unitOfWork;

        public SendOtpCommandHandler(
            IUserAccountRepository userAccountRepository,
            IUserCredentialRepository userCredentialRepository,
            IOtpHashService otpHashService,
            ISmsService smsService,
            IUnitOfWork unitOfWork)
        {
            _userAccountRepository = userAccountRepository;
            _userCredentialRepository = userCredentialRepository;
            _otpHashService = otpHashService;
            _smsService = smsService;
            _unitOfWork = unitOfWork;
        }

        public async Task<SendOtpResult> Handle(
            SendOtpCommand request,
            CancellationToken cancellationToken)
        {
            var account =
                await _userAccountRepository.GetActiveCustomerByPhoneNumberAsync(
                    request.PhoneNumber,
                    cancellationToken);

            // Do not reveal whether a phone number has an account.
            if (account is null)
            {
                return new SendOtpResult(false);
            }

            var credential =
                await _userCredentialRepository.GetByUserAccountIdAsync(
                    account.Id,
                    cancellationToken);

            if (credential?.OtpLockoutUntil is not null
                && credential.OtpLockoutUntil > DateTimeOffset.UtcNow)
            {
                return new SendOtpResult(false);
            }

            var otp = RandomNumberGenerator
                .GetInt32(100000, 1000000)
                .ToString();

            var otpHash = _otpHashService.Hash(otp);
            var expiresAt = DateTimeOffset.UtcNow.AddMinutes(5);

            if (credential is null)
            {
                credential = new UserCredential(account.Id);

                credential.SetOtp(otpHash, expiresAt);

                await _userCredentialRepository.AddAsync(
                    credential,
                    cancellationToken);
            }
            else
            {
                credential.SetOtp(otpHash, expiresAt);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _smsService.SendOtpAsync(
                account.PhoneNumber,
                otp,
                cancellationToken);

            return new SendOtpResult(true);
        }
    }
}

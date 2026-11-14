
namespace Sansheera.WholesalePlatform.Domain.Authentication;

public sealed class UserCredential
{
    private UserCredential()
    {
        // Required by EF Core
    }

    public UserCredential(Guid userAccountId)
    {
        Id = Guid.NewGuid();
        UserAccountId = userAccountId;
        IsMpinEnabled = false;
        FailedMpinAttempts = 0;
        OtpFailedAttempts = 0;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid UserAccountId { get; private set; }

    public bool IsMpinEnabled { get; private set; }
    public string? MpinHash { get; private set; }
    public DateTimeOffset? LastMpinChangedAt { get; private set; }
    public short FailedMpinAttempts { get; private set; }
    public DateTimeOffset? MpinLockoutUntil { get; private set; }

    public string? OtpHash { get; private set; }
    public DateTimeOffset? OtpExpiresAt { get; private set; }
    public short OtpFailedAttempts { get; private set; }
    public DateTimeOffset? OtpLockoutUntil { get; private set; }
    public DateTimeOffset? OtpVerifiedAt { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid? CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }

    public void SetOtp(string otpHash, DateTimeOffset expiresAt)
    {
        OtpHash = otpHash;
        OtpExpiresAt = expiresAt;
        OtpFailedAttempts = 0;
        OtpVerifiedAt = null;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}

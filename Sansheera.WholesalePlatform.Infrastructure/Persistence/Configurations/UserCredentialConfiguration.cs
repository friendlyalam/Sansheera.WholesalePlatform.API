
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sansheera.WholesalePlatform.Domain.Authentication;
using Sansheera.WholesalePlatform.Domain.Features.Customers;

namespace Sansheera.WholesalePlatform.Infrastructure.Persistence.Configurations;

public sealed class UserCredentialConfiguration
    : IEntityTypeConfiguration<UserCredential>
{
    public void Configure(EntityTypeBuilder<UserCredential> builder)
    {
        builder.ToTable("user_credential");

        builder.HasKey(x => x.Id)
            .HasName("pk_user_credential");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.UserAccountId)
            .HasColumnName("user_account_id")
            .IsRequired();

        builder.Property(x => x.IsMpinEnabled)
            .HasColumnName("is_mpin_enabled")
            .IsRequired();

        builder.Property(x => x.MpinHash)
            .HasColumnName("mpin_hash")
            .HasMaxLength(255);

        builder.Property(x => x.LastMpinChangedAt)
            .HasColumnName("last_mpin_changed_at");

        builder.Property(x => x.FailedMpinAttempts)
            .HasColumnName("failed_mpin_attempts")
            .IsRequired();

        builder.Property(x => x.MpinLockoutUntil)
            .HasColumnName("mpin_lockout_until");

        builder.Property(x => x.OtpHash)
            .HasColumnName("otp_hash")
            .HasMaxLength(255);

        builder.Property(x => x.OtpExpiresAt)
            .HasColumnName("otp_expires_at");

        builder.Property(x => x.OtpFailedAttempts)
            .HasColumnName("otp_failed_attempts")
            .IsRequired();

        builder.Property(x => x.OtpLockoutUntil)
            .HasColumnName("otp_lockout_until");

        builder.Property(x => x.OtpVerifiedAt)
            .HasColumnName("otp_verified_at");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasColumnName("created_by");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(x => x.UpdatedBy)
            .HasColumnName("updated_by");

        builder.HasIndex(x => x.UserAccountId)
            .IsUnique()
            .HasDatabaseName("uq_user_credential_user_account");

        builder.HasOne<UserAccount>()
            .WithMany()
            .HasForeignKey(x => x.UserAccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_user_credential_user_account");
    }
}

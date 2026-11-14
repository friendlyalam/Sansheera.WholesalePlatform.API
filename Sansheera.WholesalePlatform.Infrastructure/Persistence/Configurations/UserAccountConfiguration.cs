using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sansheera.WholesalePlatform.Domain.Features.Customers;

namespace Sansheera.WholesalePlatform.Infrastructure.Persistence.Configurations
{
    public sealed class UserAccountConfiguration
    : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable("user_account");

            builder.HasKey(x => x.Id)
                .HasName("pk_user_account");

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.UserType)
                .HasColumnName("user_type")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.CustomerId)
                .HasColumnName("customer_id");

            builder.Property(x => x.EmployeeId)
                .HasColumnName("employee_id");

            builder.Property(x => x.PhoneNumber)
                .HasColumnName("phone_number")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.IsPhoneVerified)
                .HasColumnName("is_phone_verified")
                .HasDefaultValue(false);

            builder.Property(x => x.PhoneVerifiedAt)
                .HasColumnName("phone_verified_at");

            builder.Property(x => x.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(x => x.LastLoginAt)
                .HasColumnName("last_login_at");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.CreatedBy)
                .HasColumnName("created_by");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(x => x.UpdatedBy)
                .HasColumnName("updated_by");
        }
    }
}

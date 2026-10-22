using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sansheera.WholesalePlatform.Domain.Customers;

namespace Sansheera.WholesalePlatform.Infrastructure.Persistence.Configurations
{
    public sealed class CustomerConfiguration
    : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customer");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.CustomerCode)
                .HasColumnName("customer_code")
                .UseIdentityAlwaysColumn();

            builder.Property(x => x.BusinessName)
                .HasColumnName("business_name")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.BusinessTypeId)
                .HasColumnName("business_type_id")
                .IsRequired();

            builder.Property(x => x.StatusId)
                .HasColumnName("status_id")
                .IsRequired();

            builder.Property(x => x.ContactPersonName)
                .HasColumnName("contact_person_name")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasColumnName("phone_number")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(254);

            builder.Property(x => x.AddressLine1)
                .HasColumnName("address_line_1")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.AddressLine2)
                .HasColumnName("address_line_2")
                .HasMaxLength(200);

            builder.Property(x => x.Landmark)
                .HasColumnName("landmark")
                .HasMaxLength(150);

            builder.Property(x => x.City)
                .HasColumnName("city")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.District)
                .HasColumnName("district")
                .HasMaxLength(100);

            builder.Property(x => x.StateName)
                .HasColumnName("state_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.PostalCode)
                .HasColumnName("postal_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.CountryCode)
                .HasColumnName("country_code")
                .HasColumnType("char(2)")
                .IsRequired();

            builder.Property(x => x.Gstin)
                .HasColumnName("gstin")
                .HasMaxLength(15);

            builder.Property(x => x.LegalBusinessName)
                .HasColumnName("legal_business_name")
                .HasMaxLength(200);

            builder.Property(x => x.GstStateCode)
                .HasColumnName("gst_state_code")
                .HasColumnType("char(2)");

            builder.Property(x => x.IsPhoneVerified)
                .HasColumnName("is_phone_verified")
                .IsRequired();

            builder.Property(x => x.PhoneVerifiedAt)
                .HasColumnName("phone_verified_at")
                .HasColumnType("timestamp with time zone");

            builder.Property(x => x.IsEmailVerified)
                .HasColumnName("is_email_verified")
                .IsRequired();

            builder.Property(x => x.EmailVerifiedAt)
                .HasColumnName("email_verified_at")
                .HasColumnType("timestamp with time zone");

            builder.Property(x => x.IsGstinVerified)
                .HasColumnName("is_gstin_verified")
                .IsRequired();

            builder.Property(x => x.GstinVerifiedAt)
                .HasColumnName("gstin_verified_at")
                .HasColumnType("timestamp with time zone");

            builder.Property(x => x.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

            builder.Property(x => x.LastLoginAt)
                .HasColumnName("last_login_at")
                .HasColumnType("timestamp with time zone");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.CreatedBy)
                .HasColumnName("created_by");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamp with time zone");

            builder.Property(x => x.UpdatedBy)
                .HasColumnName("updated_by");

            builder.HasIndex(x => x.BusinessName)
                .HasDatabaseName("ix_customer_business_name");

            builder.HasIndex(x => x.BusinessTypeId)
                .HasDatabaseName("ix_customer_business_type_id");

            builder.HasIndex(x => x.StatusId)
                .HasDatabaseName("ix_customer_status_id");

            builder.HasIndex(x => x.City)
                .HasDatabaseName("ix_customer_city");

            builder.HasIndex(x => x.PhoneNumber)
                .HasDatabaseName("ix_customer_phone_number");

            builder.HasIndex(x => x.Email)
                .HasDatabaseName("ix_customer_email");

            builder.HasIndex(x => x.Gstin)
                .HasDatabaseName("ux_customer_gstin")
                .IsUnique()
                .HasFilter("gstin IS NOT NULL");
        }
    }
}
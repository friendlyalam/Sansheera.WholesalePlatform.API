using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sansheera.WholesalePlatform.Domain.Features.Customers;

namespace Sansheera.WholesalePlatform.Infrastructure.Persistence.Configurations
{
    public sealed class BusinessTypeConfiguration
    : IEntityTypeConfiguration<BusinessType>
    {
        public void Configure(EntityTypeBuilder<BusinessType> builder)
        {
            builder.ToTable("business_type");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("smallint")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Code)
                .HasColumnName("code")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .HasColumnName("created_by");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamp with time zone");

            builder.Property(x => x.UpdatedBy)
                .HasColumnName("updated_by");

            builder.HasIndex(x => x.Code)
                .IsUnique()
                .HasDatabaseName("uq_business_type_code");
        }
    }
}

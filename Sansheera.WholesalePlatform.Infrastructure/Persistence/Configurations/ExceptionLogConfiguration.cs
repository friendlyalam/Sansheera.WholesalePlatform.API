using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sansheera.WholesalePlatform.Domain.ExceptionLog;

namespace Sansheera.WholesalePlatform.Infrastructure.Persistence.Configurations
{
    public sealed class ExceptionLogConfiguration
    : IEntityTypeConfiguration<ExceptionLog>
    {
        public void Configure(EntityTypeBuilder<ExceptionLog> builder)
        {
            builder.ToTable("exception_log");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.Timestamp)
                .HasColumnName("timestamp")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(x => x.TraceId)
                .HasColumnName("trace_id")
                .HasMaxLength(100);

            builder.Property(x => x.LogLevel)
                .HasColumnName("log_level")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.ExceptionType)
                .HasColumnName("exception_type")
                .HasMaxLength(255);

            builder.Property(x => x.Message)
                .HasColumnName("message")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.StackTrace)
                .HasColumnName("stack_trace")
                .HasColumnType("text");

            builder.Property(x => x.InnerException)
                .HasColumnName("inner_exception")
                .HasColumnType("text");

            builder.Property(x => x.RequestUrl)
                .HasColumnName("request_url")
                .HasMaxLength(500);

            builder.Property(x => x.HttpMethod)
                .HasColumnName("http_method")
                .HasMaxLength(10);

            builder.Property(x => x.LoginId)
                .HasColumnName("login_id");

            builder.Property(x => x.ClientIp)
                .HasColumnName("client_ip")
                .HasMaxLength(50);

            builder.Property(x => x.UserAgent)
                .HasColumnName("user_agent")
                .HasColumnType("text");

            builder.Property(x => x.Environment)
                .HasColumnName("environment")
                .HasMaxLength(50);

            builder.Property(x => x.Payload)
                .HasColumnName("payload")
                .HasColumnType("jsonb");

            builder.HasIndex(x => x.Timestamp)
                .HasDatabaseName("IX_exception_log_Timestamp");

            builder.HasIndex(x => x.TraceId)
                .HasDatabaseName("IX_exception_log_TraceId");

            builder.HasIndex(x => x.ExceptionType)
                .HasDatabaseName("IX_exception_log_ExceptionType");

            builder.HasIndex(x => x.LoginId)
                .HasDatabaseName("IX_exception_log_LoginId");
        }
    }
}
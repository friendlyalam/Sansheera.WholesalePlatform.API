namespace Sansheera.WholesalePlatform.Domain.Common
{
    public class BaseEntity<T>
    {
        public DateTimeOffset CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}

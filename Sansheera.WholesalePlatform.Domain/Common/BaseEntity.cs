namespace Sansheera.WholesalePlatform.Domain.Common
{
    public class BaseEntity<T>
    {
        public DateTime CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}

using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity<int>, IBaseAuditEntity
    {
        public string Name { get; set; } = default!;
        public List<Product>? Products { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}

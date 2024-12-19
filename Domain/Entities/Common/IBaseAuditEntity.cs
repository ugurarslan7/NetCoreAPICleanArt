namespace Domain.Entities.Common
{
    public interface IBaseAuditEntity
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}

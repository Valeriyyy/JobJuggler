namespace Domain.Models;

public abstract class BaseEntity
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastModifiedAt{ get; set; }
    public DateTime? DeletedAt { get; set; }
    public int CreatedById { get; set; }
    public int LastModifiedById { get; set; }
    public int DeletedById { get; set; }
    public bool IsDeleted { get; set; }
    public virtual AppUser? CreatedBy { get; set; }
    public virtual AppUser? LastModifiedBy { get; set; }
    public virtual AppUser? DeletedBy { get; set; }
}

namespace Domain.Models;

public abstract class BaseEntity
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastModifiedAt{ get; set; }
    public DateTime? DeletedAt { get; set; }
    public AppUser? CreatedBy { get; set; }
    public AppUser? LastModifiedBy { get; set; }
    public AppUser? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}

using JobJuggler.Domain.IdentityModels;

namespace JobJuggler.Domain.Models;

// This class is meant to be extended by models to put metadata columns on
// entities. Any business model class should extend this class to provide 
// the necessary metadata fields.
public abstract class BaseEntity {
    public DateTime DateCreated { get; set; }
    public int CreatedById { get; set; }
    public DateTime DateLastModified { get; set; }
    public int LastModifiedById { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DateDeleted { get; set; }
    public int DeletedById { get; set; }


    public virtual AppUser? CreatedBy { get; set; }
    public virtual AppUser? LastModifiedBy { get; set; }
    public virtual AppUser? DeletedBy { get; set; }
}

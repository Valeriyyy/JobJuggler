namespace JobJuggler.Domain.IdentityModels;

public class AppCompany {
    public int Id { get; set; }
    public string Name { get; set; }

    //Metadata Fields
    public DateTime DateCreated { get; set; }
    public int CreatedById { get; set; }
    public DateTime? DateLastModified { get; set; }
    public int? LastModifiedById { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DateDeleted { get; set; }
    public int? DeletedById { get; set; }

    public virtual List<AppUser> Users { get; set; }
}
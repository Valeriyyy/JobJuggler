namespace Domain.IdentityModels;

public class AppCompany {
    public int Id { get; set; }
    public string Name { get; set; }

    //Metadata Fields
    public DateTime DateCreated { get; set; }
    public int CreatedById { get; set; }
    public DateTime DateLastModified { get; set; }
    public int LastModifiedById { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DateDeleted { get; set; }
    public int DeletedById { get; set; }
}
using Microsoft.AspNetCore.Identity;

#nullable disable
namespace Domain.IdentityModels;

public class AppUser : IdentityUser<int> {
    public string DisplayName { get; set; }
    public int CompanyId { get; set; }
    public DateTime DateCreated { get; set; }
    public int CreatedById { get; set; }
    public DateTime DateLastModified { get; set; }
    public int LastModifiedById { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DateDeleted { get; set; }
    public int DeletedById { get; set; }
}

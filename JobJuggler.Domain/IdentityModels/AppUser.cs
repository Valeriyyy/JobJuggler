using JobJuggler.Domain.Models;
using Microsoft.AspNetCore.Identity;

#nullable disable
namespace JobJuggler.Domain.IdentityModels;

public class AppUser : IdentityUser<int> {
    public string DisplayName { get; set; }
    public int CompanyId { get; set; }
    public DateTime DateCreated { get; set; }
    public int CreatedById { get; set; }
    public DateTime? DateLastModified { get; set; }
    public int? LastModifiedById { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DateDeleted { get; set; }
    public int? DeletedById { get; set; }

    public virtual AppCompany Company { get; set; }
    public virtual List<AppRole> Roles { get; set; }
    public virtual List<AppUserLogin> Logins { get; set; }
    public virtual List<AppUserClaim> Claims { get; set; }
    public virtual List<AppUserToken> UserTokens { get; set; }
    
    // public virtual ICollection<T> CreatedRecords { get; set; }
    // public virtual ICollection<U> UpdatedRecords { get; set; }
    // public virtual ICollection<V> DeletedRecords { get; set; }
}

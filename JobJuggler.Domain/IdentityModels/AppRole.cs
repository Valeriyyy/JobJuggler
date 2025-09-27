using Microsoft.AspNetCore.Identity;

namespace JobJuggler.Domain.IdentityModels;
public class AppRole : IdentityRole<int> {
    public virtual ICollection<AppUserRole> UserRoles { get; set; }
    public virtual ICollection<AppRoleClaim> Claims { get; set; }
}

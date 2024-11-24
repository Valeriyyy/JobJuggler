using Microsoft.AspNetCore.Identity;

namespace JobJuggler.Domain.IdentityModels;

public class AppUserRole : IdentityUserRole<int>
{
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public AppUser User { get; set; }
    public AppRole Role { get; set; }
}
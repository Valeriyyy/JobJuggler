using Microsoft.AspNetCore.Identity;

namespace JobJuggler.Domain.IdentityModels;

public class AppUserClaim : IdentityUserClaim<int>
{
    public virtual AppUser User { get; set; }
}
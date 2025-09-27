using Microsoft.AspNetCore.Identity;

namespace JobJuggler.Domain.IdentityModels;

public class AppUserToken : IdentityUserToken<int>
{
   public virtual AppUser User { get; set; }
}
using Microsoft.AspNetCore.Identity;

namespace JobJuggler.Domain.IdentityModels;

public sealed class AppUserLogin : IdentityUserLogin<int>
{
   public AppUser User { get; set; }
}
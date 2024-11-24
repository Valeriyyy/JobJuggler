using Microsoft.AspNetCore.Identity;

namespace JobJuggler.Domain.IdentityModels;

public class AppUserToken : IdentityUserToken<int>
{
    public int UserId { get; set; }
    public string LoginProvider { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    
    public virtual AppUser User { get; set; }
}
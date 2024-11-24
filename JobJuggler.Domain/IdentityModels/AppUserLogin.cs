using Microsoft.AspNetCore.Identity;

namespace JobJuggler.Domain.IdentityModels;

public sealed class AppUserLogin : IdentityUserLogin<int>
{
    public string LoginProvider { get; set; }
    public string ProviderKey { get; set; }
    public string ProviderDisplayName { get; set; }
    public int UserId { get; set; }
    
    public AppUser User { get; set; }
}
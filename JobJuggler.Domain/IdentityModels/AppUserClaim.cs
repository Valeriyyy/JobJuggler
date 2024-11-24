using Microsoft.AspNetCore.Identity;

namespace JobJuggler.Domain.IdentityModels;

public class AppUserClaim : IdentityUserClaim<int>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
    
    public AppUser User { get; set; }
}
using Microsoft.AspNetCore.Identity;

namespace JobJuggler.Domain.IdentityModels;
public class AppRole : IdentityRole<int> {
    public int Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public string ConcurrencyStamp { get; set; }
    
    public virtual List<AppUser> Users { get; set; }
}

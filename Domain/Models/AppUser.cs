using Microsoft.AspNetCore.Identity;

#nullable disable
namespace Domain.Models;

public class AppUser : IdentityUser<Guid>
{
    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public DateTime DateDeleted { get; set; }
    public bool IsDeleted { get; set; }

}

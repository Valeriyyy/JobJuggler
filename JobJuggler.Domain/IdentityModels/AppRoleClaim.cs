using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace JobJuggler.Domain.IdentityModels;

public class AppRoleClaim : IdentityRoleClaim<int>
{
    public virtual AppRole Role { get; set; }
}
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace JobJuggler.Domain.IdentityModels;

public class AppRoleClaim : IdentityRoleClaim<int>
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    /// <summary>
    /// Use ClaimTypes object
    /// </summary>
    public string? ClaimType { get; set; }
    /// <summary>
    /// Use ClaimValueTypes
    /// </summary>
    public string? ClaimValue { get; set; }
    
    public virtual AppRole Role { get; set; }
}
using System.Security.Claims;
using JobJuggler.Application;
using Microsoft.AspNetCore.Http;

namespace JobJuggler.Infrastructure.Security;

public class UserAccessor(IHttpContextAccessor httpContextAccessor) : IUserAccessor
{
    public string GetUsername() => httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
    
    public int GetUserId()
    {
        var idString =httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return idString != null ? int.Parse(idString) : 0;
    }
}
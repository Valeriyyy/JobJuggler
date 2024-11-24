using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Security;

namespace JobJuggler.Application.Infra;

public class UserAccessor(IHttpContextAccessor httpContextAccessor) : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string GetUsername() => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
    // public int GetUserId() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.);
}


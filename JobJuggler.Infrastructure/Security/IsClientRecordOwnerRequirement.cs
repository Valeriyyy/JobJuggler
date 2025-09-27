using System.Security.Claims;
using JobJuggler.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace JobJuggler.Infrastructure.Security;

public class IsClientRecordOwnerRequirement : IAuthorizationRequirement
{
    
}

public class IsClientRecordOwnerRequirementHandler : AuthorizationHandler<IsRecordOwnerRequirement>
{
    private readonly DataContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IsClientRecordOwnerRequirementHandler(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsRecordOwnerRequirement requirement)
    {
        // var hasCompany = int.TryParse(context.User.Claims.FirstOrDefault(c => c.Type == "company")?.Value, out var companyId);
        // var hasClient = int.TryParse(_httpContextAccessor.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value.ToString(), out var clientId);
        // var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        // if (!hasCompany)
        // {
        //     throw new Exception($"No company was found for user {userId}");
        // }
        //
        // if (userId == null)
        // {
        //     return Task.CompletedTask;
        // }
        //
        // var clientRecord = _dbContext.Clients
        //     .Include(c => c.CreatedBy)
        //     .Select(c => new { c.Id, c.CreatedBy.CompanyId })
        //     .AsNoTracking()
        //     .FirstOrDefault(c => c.Id == clientId && c.CompanyId == companyId);
        //
        // if (clientRecord == null)
        // {
        //     context.Fail(new AuthorizationFailureReason(this, "fail"));
        // }
        // else
        // {
        //     context.Succeed(requirement);
        // }
        
        return Task.CompletedTask;
    }
}
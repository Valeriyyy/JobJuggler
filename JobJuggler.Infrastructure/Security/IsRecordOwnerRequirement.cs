using System.Security.Claims;
using Dapper;
using JobJuggler.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace JobJuggler.Infrastructure.Security;

// the purpose of this policy is to ensure that users only have access to
// their companies records. At this moment users should be able to access
// all records in their company.
// Admins will not be able to access records through this policy,
// they will have to get the data some other way.
// This is a client/customer facing endpoint

public class IsRecordOwnerRequirement : IAuthorizationRequirement
{
    
}

public class IsRecordOwnerRequirementHandler : AuthorizationHandler<IsRecordOwnerRequirement>
{
    private readonly DataContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    
    public IsRecordOwnerRequirementHandler(DataContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _dbContext = context;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsRecordOwnerRequirement requirement)
    {
        var hasCompany = int.TryParse(context.User.Claims.FirstOrDefault(c => c.Type == "company")?.Value, out var companyId);
        var hasClient = int.TryParse(_httpContextAccessor.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value.ToString(), out var clientId);
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!hasCompany)
        {
            throw new Exception($"No company was found for user {userId}");
        }

        if (userId == null)
        {
            return Task.CompletedTask;
        }

        Console.WriteLine("this is the user id trying to access the record " + userId);
        
        // var recordId = 
        // var rr = _dbContext
        /*var query = @"SELECT company_id as CompanyId 
                      FROM main.clients c 
                      LEFT JOIN identity.users u
                      on c.created_by_id = u.id 
                      WHERE c.id = @clientId AND u.company_id = @companyId";
        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("postgres")))
        {
            connection.Open();
            var r = connection.QuerySingleOrDefault<int?>(query, new { clientId = 9, companyId = 2 });
            Console.WriteLine("this is the company id of the person trying to access the record " + r);
            connection.Close();
        }*/

        var r = _dbContext.Clients
            .Include(c => c.CreatedBy)
            .Select(c => new { c.Id, c.CreatedBy.CompanyId })
            .AsNoTracking()
            .FirstOrDefault(c => c.Id == clientId && c.CompanyId == 2);
            
        context.Succeed(requirement);

        return Task.CompletedTask;
    }

    
}
using JobJuggler.Domain.IdentityModels;
using JobJuggler.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobJuggler.API.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly DataContext _context;

    public IdentityController(DataContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<List<AppUser>> GetAll()
    {
        var rr = await _context.Users
            .Include(u => u.Company)
            // .Include(u => u.Roles)
            .Include(u => u.UserTokens)
            .ToListAsync();

        // var rr = await _context.Roles
        //     .Include(r => r.Users)
        //     .ToListAsync();
        
        return rr;
    }
}
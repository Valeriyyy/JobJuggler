using JobJuggler.Domain.IdentityModels;
using JobJuggler.DTO.Identity;
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


    // [HttpGet]
    // public async Task<List<UserDTO>> GetAll()
    // {
    //     var rr = await _context.Users
    //         .Include(u => u.Company)
    //         .Select(c => new UserDTO { 
    //             UserId = c.Id, 
    //             UserName = c.UserName, 
    //             Email = c.Email, 
    //             Company = new CompanyDTO
    //             {
    //                 CompanyId = c.CompanyId,
    //                 Name = c.Company.Name
    //             }
    //         })
    //         .ToListAsync();
    //
    //     // var rr = await _context.Roles
    //     //     .Include(r => r.Users)
    //     //     .ToListAsync();
    //     
    //     return rr;
    // }
}
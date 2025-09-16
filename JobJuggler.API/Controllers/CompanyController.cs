using JobJuggler.Domain.IdentityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobJuggler.Persistence;

namespace JobJuggler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase {
    private readonly DataContext _context;

    public CompanyController(DataContext dbContext) {
        _context = dbContext;
    }

    // [HttpGet]
    // public async Task<ActionResult<AppCompany>> GetCompanies() {
    //     var hasComp = await _context.Companies.FirstOrDefaultAsync();
    //     Console.WriteLine(hasComp);
    //     return Ok(hasComp);
    // }
}

using JobJuggler.Domain.IdentityModels;
using JobJuggler.Infrastructure.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobJuggler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly ILogger<RolesController> _logger;
    private readonly RoleManager<AppRole> _roleManager;

    public RolesController(RoleManager<AppRole> roleManager, ILogger<RolesController> logger)
    {
        _roleManager = roleManager;
        _logger = logger;
    }

    [HttpPost("/add-role")]
    [AllowAnonymous]
    public async Task<IActionResult> AddRoleAsync([FromBody] RoleDTO role)
    {
        IdentityResult createdRole;
        try
        {
            var appRole = new AppRole()
            {
                Name = role.Name,
                NormalizedName = role.Name.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            
            createdRole = await _roleManager.CreateAsync(appRole);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed creating role of body {role}", role);
            return BadRequest(ex.Message);
        }

        return Ok(createdRole);
    }
}
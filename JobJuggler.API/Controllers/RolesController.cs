using JobJuggler.Application.Services.Interfaces;
using JobJuggler.Domain.IdentityModels;
using JobJuggler.Infrastructure.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobJuggler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "admin")]
public class RolesController : ControllerBase
{
    private readonly ILogger<RolesController> _logger;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    
    private readonly IRoleService _roleService;

    public RolesController(RoleManager<AppRole> roleManager, ILogger<RolesController> logger, UserManager<AppUser> userManager, IRoleService roleService)
    {
        _roleManager = roleManager;
        _logger = logger;
        _userManager = userManager;
        _roleService = roleService;
    }

    // [HttpGet("get-all")]
    // public ActionResult<List<AppRole>> GetRoles()
    // {
    //     var roles = _roleService.GetRoles();
    //
    //     return Ok(roles);
    // }

    // [HttpPost("add-role")]
    // [AllowAnonymous]
    // public async Task<IActionResult> AddRoleAsync([FromBody] RoleDTO role)
    // {
    //     IdentityResult createdRole;
    //     try
    //     {
    //         var appRole = new AppRole()
    //         {
    //             Name = role.Name,
    //             NormalizedName = role.Name.ToUpper(),
    //             ConcurrencyStamp = Guid.NewGuid().ToString()
    //         };
    //         
    //         createdRole = await _roleManager.CreateAsync(appRole);
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Failed creating role of body {role}", role);
    //         return BadRequest(ex.Message);
    //     }
    //
    //     return Ok(createdRole);
    // }
    //
    // [HttpPost("assign-role")]
    // public async Task<IActionResult> AssignRole([FromBody] AssignRoleDTO assignRole)
    // {
    //     IdentityResult assignedRole;
    //     var appRole = await _roleManager.FindByIdAsync(assignRole.RoleId.ToString());
    //     if (appRole == null || appRole.Name == null)
    //     {
    //         return BadRequest();
    //     }
    //     var appUser = await _userManager.FindByIdAsync(assignRole.UserId.ToString());
    //     if (appUser == null)
    //     {
    //         return BadRequest();
    //     }
    //     assignedRole = await _userManager.AddToRoleAsync(appUser, appRole.Name);
    //     
    //     return Ok(assignedRole);
    // }
}
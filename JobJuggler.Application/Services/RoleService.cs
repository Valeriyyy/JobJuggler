using JobJuggler.Application.Services.Interfaces;
using JobJuggler.Domain.IdentityModels;
using JobJuggler.Infrastructure.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JobJuggler.Application.Services;

public class RoleService : IRoleService
{
    private readonly ILogger<IRoleService> _logger;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;

    public RoleService(RoleManager<AppRole> roleManager, ILogger<IRoleService> logger, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _logger = logger;
        _userManager = userManager;
    }

    public List<AppRole> GetRoles()
    {
        var roles = _roleManager.Roles.ToList();
        
        return roles;
    }

    public AppRole CreateRole(RoleDTO role)
    {
        throw new NotImplementedException();
    }

    public Task AssignRole(AssignRoleDTO assignRoleDTO)
    {
        throw new NotImplementedException();
    }
}
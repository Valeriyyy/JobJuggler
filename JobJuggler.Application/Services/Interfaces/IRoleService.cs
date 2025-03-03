using JobJuggler.Domain.IdentityModels;
using JobJuggler.Infrastructure.DTOs;

namespace JobJuggler.Application.Services.Interfaces;

public interface IRoleService
{
    public List<AppRole> GetRoles();
    public AppRole CreateRole(RoleDTO role);
    public Task AssignRole(AssignRoleDTO assignRoleDTO);
}
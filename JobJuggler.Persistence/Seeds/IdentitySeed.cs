using JobJuggler.Domain.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobJuggler.Persistence.Seeds;

public class IdentitySeed {
    private readonly DataContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public IdentitySeed(DataContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// The main method for seeding identity rows into the database and the identity schema
    /// </summary>
    /// <param name="context">EFCore database context instance</param>
    /// <param name="userManager">EFCore identity user manager</param>
    /// <returns>Task that returns nothing</returns>
    public async Task SeedIdentity() {
        await SeedRoles();
        await SeedCompanies();
        await SeedUsers();
    }

    private async Task SeedRoles()
    {
        if (_roleManager.Roles.Any())
        {
            await _roleManager.CreateAsync(new AppRole
                { Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.Empty.ToString() });
            // context.Roles.Add(new AppRole() { Name = "idk", NormalizedName = "idk", ConcurrencyStamp = Guid.Empty.ToString() });
            // context.Roles.Add(new AppRole() { Name = "idk", NormalizedName = "idk", ConcurrencyStamp = Guid.Empty.ToString() });
        }
    }

    private async Task SeedCompanies() {
        if (!_context.Companies.Any()) {
            _context.Companies.Add(
                new AppCompany {
                    Name = "JobJuggler",
                    DateCreated = DateTime.UtcNow,
                }
            );

            await _context.SaveChangesAsync();
        }


    }
    private async Task SeedUsers() {
        var mainCompany = await _context.Companies.Where(c => c.Name == "JobJuggler").FirstOrDefaultAsync();
        if (!_userManager.Users.Any()) {
            var users = new List<AppUser>
            {
                new () {
                    CompanyId = mainCompany.Id,
                    DisplayName = "Valeriy",
                    UserName = "valeriy",
                    Email = "valeriykutsar18@gmail.com",
                    DateCreated = DateTime.UtcNow,
                    IsDeleted = false
                }
            };

            foreach (var user in users) {
                await _userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
using JobJuggler.Domain.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobJuggler.Persistence.Seeds;

public class IdentitySeed {
    /// <summary>
    /// The main method for seeding identity rows into the database and the identity schema
    /// </summary>
    /// <param name="context">EFCore database context instance</param>
    /// <param name="userManager">EFCore identity user manager</param>
    /// <returns>Task that returns nothing</returns>
    public static async Task SeedIdentity(DataContext context, UserManager<AppUser> userManager) {
        await SeedCompanies(context);
        await SeedUsers(context, userManager);
    }

    public static async Task SeedCompanies(DataContext context) {
        if (!context.Companies.Any()) {
            context.Companies.Add(
                new() {
                    Name = "JobJuggler",
                    DateCreated = DateTime.UtcNow,
                }
            );

            await context.SaveChangesAsync();
        }


    }
    public static async Task SeedUsers(DataContext context, UserManager<AppUser> userManager) {
        var mainCompany = await context.Companies.Where(c => c.Name == "JobJuggler").FirstOrDefaultAsync();
        if (!userManager.Users.Any()) {
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
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
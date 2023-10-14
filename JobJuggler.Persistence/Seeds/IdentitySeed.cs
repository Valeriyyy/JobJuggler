using JobJuggler.Domain.IdentityModels;
using Microsoft.AspNetCore.Identity;

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
            var companies = new List<AppCompany> {
                new () {
                    Name = "JobJuggler"
                }
            };
        }
    }
    public static async Task SeedUsers(DataContext context, UserManager<AppUser> userManager) {
        if (!userManager.Users.Any()) {
            var users = new List<AppUser>
            {
                new () {
                    DisplayName = "Valeriy",

                }
            };
        }
    }
}
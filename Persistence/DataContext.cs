using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfigurations;

namespace Persistence;

public class DataContext : DbContext //IdentityDbContext<AppUser>
{
    public DataContext() { }
    public DataContext(DbContextOptions options) : base(options) { }

    //public DbSet<AppUser> Users { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<Job> Jobs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("crystal_clean");
        modelBuilder.HasPostgresExtension("uuid-ossp");

        //new AppUserEntityTypeConfiguration().Configure(modelBuilder.Entity<AppUser>());

        new ClientEntityTypeConfiguration().Configure(modelBuilder.Entity<Client>());
        new LocationEntityTypeConfiguration().Configure(modelBuilder.Entity<Location>());
        new JobEntityTypeConfiguration().Configure(modelBuilder.Entity<Job>());
    }
}

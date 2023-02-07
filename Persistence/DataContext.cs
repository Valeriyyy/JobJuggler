using Domain.Models;
using Domain.Models.Enums;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<LineItem> LineItems { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<InvoiceLine> InvoicesLines { get; set; } = null!;
    public DbSet<EnumModel> EnumModels { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            Console.WriteLine("no options configured, not going to run");
        }
        NpgsqlConnection.GlobalTypeMapper.MapEnum<PriceType>("price_type");

        // according to documentation, this should be used to register enums
        // but it is broken right now and is being worked on. In the meantime
        // the context will continue to use the global type mapper.
        /*var dataSourceBuilder = new NpgsqlDataSourceBuilder();
        dataSourceBuilder.MapEnum<PriceType>();
        using var dataSource = dataSourceBuilder.Build();*/
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var defaultSchema = "crystal_clean";
        modelBuilder.HasDefaultSchema(defaultSchema);
        modelBuilder.HasPostgresExtension("uuid-ossp")
            .HasPostgresEnum(defaultSchema, "price_type", new[] { "none", "per_unit", "flat_rate" });


        //new AppUserEntityTypeConfiguration().Configure(modelBuilder.Entity<AppUser>());

        new ClientEntityTypeConfiguration().Configure(modelBuilder.Entity<Client>());
        new LocationEntityTypeConfiguration().Configure(modelBuilder.Entity<Location>());
        new JobEntityTypeConfiguration().Configure(modelBuilder.Entity<Job>());
        new PaymentMethodEntityTypeConfiguration().Configure(modelBuilder.Entity<PaymentMethod>());
        new LineItemEntityTypeConfiguration().Configure(modelBuilder.Entity<LineItem>());
        new InvoiceEntityTypeConfiguration().Configure(modelBuilder.Entity<Invoice>());
        new InvoiceLineEntityTypeConfiguration().Configure(modelBuilder.Entity<InvoiceLine>());
        new EnumModelTypeConfiguration().Configure(modelBuilder.Entity<EnumModel>());
    }
}

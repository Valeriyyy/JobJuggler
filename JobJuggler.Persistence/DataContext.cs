using JobJuggler.Domain.IdentityModels;
using JobJuggler.Domain.Models;
using JobJuggler.Domain.Enums;
using JobJuggler.Domain.MetaModels;
using JobJuggler.Persistence.EntityConfigurations;
using JobJuggler.Persistence.EntityConfigurations.Identity;
using JobJuggler.Persistence.EntityConfigurations.Meta;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Persistence.EntityConfigurations;

namespace JobJuggler.Persistence;

public class DataContext : IdentityDbContext<
    AppUser, AppRole, int, AppUserClaim, 
    AppUserRole, AppUserLogin, AppRoleClaim, 
    AppUserToken> {
    public DataContext() { }
    public DataContext(DbContextOptions options) : base(options) { }
    public DbSet<AppCompany> Companies { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    // public DbSet<Client> Clients { get; set; } = null!;
    // public DbSet<Location> Locations { get; set; } = null!;
    // public DbSet<Job> Jobs { get; set; } = null!;
    // public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    // public DbSet<LineItem> LineItems { get; set; } = null!;
    // public DbSet<Invoice> Invoices { get; set; } = null!;
    // public DbSet<InvoiceLine> InvoicesLines { get; set; } = null!;
    // public DbSet<EnumModel> EnumModels { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        #pragma warning disable CS0618
        //NpgsqlConnection.GlobalTypeMapper.MapEnum<PriceType>("price_type");

        // according to documentation, this should be used to register enums
        // but it is broken right now and is being worked on. In the meantime
        // the context will continue to use the global type mapper.
        /*var dataSourceBuilder = new NpgsqlDataSourceBuilder();
        dataSourceBuilder.MapEnum<PriceType>();
        using var dataSource = dataSourceBuilder.Build();*/
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        var defaultSchema = "main";
        // modelBuilder.HasDefaultSchema(defaultSchema);
        // modelBuilder.HasPostgresExtension("uuid-ossp")
        //     .HasPostgresEnum(defaultSchema, "price_type", ["none", "per_unit", "flat_rate"]);


        #region Identity
        new AppUserEntityTypeConfiguration().Configure(modelBuilder.Entity<AppUser>());
        new CompanyEntityTypeConfiguration().Configure(modelBuilder.Entity<AppCompany>());
        new AppRoleEntityTypeConfiguration().Configure(modelBuilder.Entity<AppRole>());
        new AppUserClaimEntityTypeConfiguration().Configure(modelBuilder.Entity<AppUserClaim>());
        new AppRoleClaimEntityTypeConfiguration().Configure(modelBuilder.Entity<AppRoleClaim>());
        new AppUserLoginEntityTypeConfiguration().Configure(modelBuilder.Entity<AppUserLogin>());
        new AppUserRoleEntityTypeConfiguration().Configure(modelBuilder.Entity<AppUserRole>());
        new AppUserTokenEntityTypeConfiguration().Configure(modelBuilder.Entity<AppUserToken>());
        #endregion
        
        #region Meta
        new ProductEntityTypeConfiguration().Configure(modelBuilder.Entity<Product>());
        new ContactEntityTypeConfiguration().Configure(modelBuilder.Entity<Contact>());
        new SubscriptionEntityTypeConfiguration().Configure(modelBuilder.Entity<Subscription>());
        #endregion
        
        #region Business
        // new ClientEntityTypeConfiguration().Configure(modelBuilder.Entity<Client>());
        // new LocationEntityTypeConfiguration().Configure(modelBuilder.Entity<Location>());
        // new JobEntityTypeConfiguration().Configure(modelBuilder.Entity<Job>());
        // new PaymentMethodEntityTypeConfiguration().Configure(modelBuilder.Entity<PaymentMethod>());
        // new LineItemEntityTypeConfiguration().Configure(modelBuilder.Entity<LineItem>());
        // new InvoiceEntityTypeConfiguration().Configure(modelBuilder.Entity<Invoice>());
        // new InvoiceLineEntityTypeConfiguration().Configure(modelBuilder.Entity<InvoiceLine>());
        // new EnumModelTypeConfiguration().Configure(modelBuilder.Entity<EnumModel>());
        #endregion
    }
}

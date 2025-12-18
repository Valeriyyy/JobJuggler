using JobJuggler.Common;
using JobJuggler.Domain.IdentityModels;
using JobJuggler.Domain.MetaModels;
using JobJuggler.Domain.MetaModels.Enums;
using JobJuggler.Persistence.EntityConfigurations.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Meta;

public class SubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        var tableName = "subscriptions";
        builder.ToTable(tableName, DbSchemas.JobJuggler.ToSnakeCase());
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.CompanyId)
            .HasColumnName("company_id");
        
        builder.Property(e => e.ProductId)
            .HasColumnName("product_id");

        builder.Property(e => e.StartDate)
            .HasColumnName("start_date");
        
        builder.Property(e => e.EndDate)
            .HasColumnName("end_date");
        
        builder.Property(e => e.Status)
            .HasColumnName("status")
            .HasConversion<SubscriptionStatus>();
        
        builder.Property(e => e.PriceOverride)
            .HasColumnName("price_override");
        
        builder.AddAuditConfigFields(tableName);
        
        builder.HasOne<Product>(s => s.Product)
            .WithMany(p => p.Subscriptions)
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<AppCompany>(s => s.Company)
            .WithMany(c => c.Subscriptions)
            .HasForeignKey(s => s.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
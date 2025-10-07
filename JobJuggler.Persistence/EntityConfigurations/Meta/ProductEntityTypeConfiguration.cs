using JobJuggler.Common;
using JobJuggler.Domain.MetaModels;
using JobJuggler.Domain.MetaModels.Enums;
using JobJuggler.Persistence.EntityConfigurations.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Meta;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        var dbName = "products";
        builder.ToTable(dbName, DbSchemas.JobJuggler.ToSnakeCase());
        
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();
        
        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(250);

        builder.Property(e => e.Type)
            .HasColumnName("type")
            .HasConversion<ProductType>();
        
        builder.Property(e => e.BillingPeriod)
            .HasColumnName("billing_period")
            .HasConversion<BillingPeriod>();
        
        builder.Property(e => e.Price)
            .HasColumnName("price");
        
        builder.Property(e => e.IsActive)
            .HasColumnName("is_active");
        
        builder.Property(e => e.ProductOptions)
            .HasColumnName("product_options")
            .HasColumnType("jsonb");
        
        builder.AddAuditConfigFields(dbName);
    }
}
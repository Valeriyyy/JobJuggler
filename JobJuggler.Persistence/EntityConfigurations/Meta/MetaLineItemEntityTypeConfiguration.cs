using JobJuggler.Common;
using JobJuggler.Domain.MetaModels;
using JobJuggler.Persistence.EntityConfigurations.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Meta;

public class MetaLineItemEntityTypeConfiguration : IEntityTypeConfiguration<MetaLineItem>
{
    public void Configure(EntityTypeBuilder<MetaLineItem> builder)
    {
        var dbName = "meta_line_items";
        builder.ToTable(dbName, DbSchemas.JobJuggler.ToSnakeCase());
        
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();
        
        builder.Property(e => e.InvoiceId)
            .HasColumnName("invoice_id")
            .IsRequired();

        builder.Property(e => e.ProductId)
            .HasColumnName("product_id");
        
        builder.Property(e => e.SubscriptionId)
            .HasColumnName("subscription_id")
            .HasDefaultValue(null);

        builder.Property(e => e.OverridePrice)
            .HasColumnName("override_price")
            .HasDefaultValue(null);
        
        // Navigation mappings
        builder.HasOne(m => m.Invoice)
            .WithMany(x => x.LineItems) // Invoice does not have a collection navigation for MetaLineItem
            .HasForeignKey(m => m.InvoiceId)
            .HasConstraintName("meta_line_items_invoice_id_foreign")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Product)
            .WithMany() // Product does not have a collection navigation for MetaLineItem
            .HasForeignKey(m => m.ProductId)
            .HasConstraintName("meta_line_items_product_id_foreign")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Subscription)
            .WithMany() // Subscription does not have a collection navigation for MetaLineItem
            .HasForeignKey(m => m.SubscriptionId)
            .HasConstraintName("meta_line_items_subscription_id_foreign")
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.AddAuditConfigFields(dbName);
    }
}
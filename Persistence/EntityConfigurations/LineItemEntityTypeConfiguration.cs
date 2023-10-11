using Domain.Models;
using Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;
public class LineItemEntityTypeConfiguration : IEntityTypeConfiguration<LineItem> {
    public void Configure(EntityTypeBuilder<LineItem> builder) {
        builder.ToTable("line_items", "main");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Name)
            .HasColumnName("name");

        builder.Property(e => e.BasePrice)
            .HasColumnName("base_price")
            .HasComment("The default price for the item. Can be overridden when put on an invoice");

        builder.Property(e => e.PriceType)
            .HasColumnName("price_type")
            .HasConversion<PriceType>();
    }
}

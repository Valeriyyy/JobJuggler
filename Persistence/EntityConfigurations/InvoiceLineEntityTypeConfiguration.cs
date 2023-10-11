using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;
public class InvoiceLineEntityTypeConfiguration : IEntityTypeConfiguration<InvoiceLine> {
    public void Configure(EntityTypeBuilder<InvoiceLine> builder) {
        builder.ToTable("invoice_lines", "main");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Guid)
            .HasColumnName("guid")
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(e => e.InvoiceId)
            .HasColumnName("invoice_id");

        builder.Property(e => e.NumOfUnits)
            .HasColumnName("num_of_units")
            .HasComment("The number of the same line items in the invoice");

        builder.Property(e => e.ItemId)
            .HasColumnName("item_id")
            .HasComment("The item that is on the invoice");

        builder.Property(e => e.Price)
            .HasColumnName("price")
            .HasComment("The total price of the item from the quantity");

        builder.HasOne(invoiceLine => invoiceLine.Invoice)
            .WithMany(invoice => invoice.Lines)
            .HasForeignKey(invoiceLine => invoiceLine.InvoiceId)
            .HasConstraintName("line_invoice_id_foreign");

        builder.HasOne(invoiceLine => invoiceLine.Item)
            .WithMany(lineItem => lineItem.Invoices)
            .HasForeignKey(invoiceLine => invoiceLine.ItemId)
            .HasConstraintName("line_item_id_foreign");
    }
}

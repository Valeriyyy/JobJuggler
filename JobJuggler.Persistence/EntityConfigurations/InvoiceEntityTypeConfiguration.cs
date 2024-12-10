using JobJuggler.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations;
public class InvoiceEntityTypeConfiguration : IEntityTypeConfiguration<Invoice> {
    public void Configure(EntityTypeBuilder<Invoice> builder) {
        builder.ToTable("invoices");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Guid)
            .HasColumnName("guid")
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(e => e.JobId)
            .HasColumnName("job_id");

        builder.Property(e => e.ConsigneeId)
            .HasColumnName("consignee_id");

        builder.Property(e => e.ReferenceNumber)
            .HasColumnName("reference_number")
            .HasComment("A unique number used for easily identifying jobs with customers");

        builder.Property(e => e.TotalPrice)
            .HasColumnName("total_price")
            .HasComment("The calculated total from the invoice lines. Not meant to be directly edited");

        builder.Property(e => e.PaymentMethodId)
            .HasColumnName("payment_method_id")
            .HasDefaultValue(null)
            .HasComment("The method used for submitting payment by the consignee");

        builder.Property(e => e.IsPaid)
            .HasColumnName("is_paid")
            .HasDefaultValue(false)
            .HasComment("Indicates if the invoice has been fully paid for");

        builder.Property(e => e.DateInvoiced)
            .HasColumnName("date_invoiced")
            .HasDefaultValue(null)
            .HasComment("The date the customer was sent the invoice");

        builder.Property(e => e.DatePaid)
            .HasColumnName("date_paid")
            .HasDefaultValue(null)
            .HasComment("The latest date the payment was submitted");

        builder.Property(e => e.DateClosed)
            .HasColumnName("date_closed")
            .HasDefaultValue(null)
            .HasComment("The final date when the invoice was fully processed and all the payment has cleared");

        builder.HasOne(invoice => invoice.Consignee)
            .WithMany(consignee => consignee.Invoices)
            .HasForeignKey(invoice => invoice.ConsigneeId)
            .HasConstraintName("invoice_consignee_id_foreign");

        // builder.HasOne(invoice => invoice.PaymentMethod)
        //     .WithMany(paymentMethod => paymentMethod.Invoices)
        //     .HasForeignKey(invoice => invoice.PaymentMethodId)
        //     .HasConstraintName("invoice_payment_method_id_foreign");
    }
}

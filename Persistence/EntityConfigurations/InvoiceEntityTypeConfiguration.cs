using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;
public class InvoiceEntityTypeConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
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
            .HasColumnName("reference_number");

        builder.Property(e => e.TotalPrice)
            .HasColumnName("total_price")
            .HasComment("The calculated total from the invoice lines. Not meant to be directly edited");

        builder.Property(e => e.PaymentMethodId)
            .HasColumnName("payment_method_id")
            .HasComment("The method used for submitting payment by the consignee");

        builder.Property(e => e.IsPaid)
            .HasColumnName("is_paid")
            .HasComment("Indicates if the invoice has been fully paid for");

        builder.Property(e => e.DateInvoiced)
            .HasColumnName("date_invoiced")
            .HasComment("The date the customer was sent the invoice");

        builder.Property(e => e.DatePaid)
            .HasColumnName("date_paid")
            .HasComment("The latest date the payment was submitted");

        builder.Property(e => e.DateClosed)
            .HasColumnName("date_closed")
            .HasComment("The final date when the invoice was fully processed and all the payment has cleared");

        builder.HasOne(invoice => invoice.Job)
            .WithMany(job => job.Invoices)
            .HasForeignKey(invoice => invoice.JobId)
            .HasConstraintName("invoice_job_id_foreign");

        builder.HasOne(invoice => invoice.Consignee)
            .WithMany(consignee => consignee.Invoices)
            .HasForeignKey(invoice => invoice.ConsigneeId)
            .HasConstraintName("invoice_consignee_id_foreign");

        builder.HasOne(invoice => invoice.PaymentMethod)
            .WithMany(paymentMethod => paymentMethod.Invoices)
            .HasForeignKey(invoice => invoice.PaymentMethodId)
            .HasConstraintName("invoice_payment_method_id_foreign");
    }
}

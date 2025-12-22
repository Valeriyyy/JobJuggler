using JobJuggler.Common;
using JobJuggler.Domain.MetaModels;
using JobJuggler.Persistence.EntityConfigurations.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Billing;

public class MetaInvoiceEntityTypeConfiguration : IEntityTypeConfiguration<MetaInvoice>
{
    public void Configure(EntityTypeBuilder<MetaInvoice> builder)
    {
        var tableName = "meta_invoices";
        builder.ToTable(tableName, DbSchemas.JobJuggler.ToSnakeCase());

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();
        
        builder.Property(e => e.CompanyId)
            .HasColumnName("company_id");

        builder.Property(e => e.Status)
            .HasColumnName("status")
            .HasConversion<InvoiceStatus>()
            .HasDefaultValue(InvoiceStatus.None);

        builder.Property(e => e.SubTotal)
            .HasColumnName("sub_total")
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0.00m);

        builder.Property(e => e.Tax)
            .HasColumnName("tax")
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0.00m);
        
        builder.Property(e => e.Total)
            .HasColumnName("total")
            .HasColumnType("decimal(18,2)")
            .HasComputedColumnSql("\"sub_total\" + \"tax\"", stored: true);

        builder.Property(e => e.DatePaid)
            .HasColumnName("date_paid")
            .HasDefaultValue(null);
        
        builder.AddAuditConfigFields(tableName);
    }
}
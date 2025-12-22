using JobJuggler.Common;
using JobJuggler.Domain.MetaModels;
using JobJuggler.Persistence.EntityConfigurations.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Billing;

public class CompanyBillingInfoEntityTypeConfiguration : IEntityTypeConfiguration<CompanyBillingInfo>
{
    public void Configure(EntityTypeBuilder<CompanyBillingInfo> builder)
    {
        var dbName = "company_billing_info";
        builder.ToTable(dbName, DbSchemas.JobJuggler.ToSnakeCase());

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.CompanyId)
            .HasColumnName("company_id");

        builder.Property(e => e.ContactId)
            .HasColumnName("contact_id");

        builder.Property(e => e.PaymentToken)
            .HasColumnName("payment_token");
        
        builder.Property(e => e.Last4Digits)
            .HasColumnName("last_4_digits");
        
        builder.Property(e => e.CardBrand)
            .HasColumnName("card_brand");
        
        builder.Property(e => e.ExpMonth)
            .HasColumnName("exp_month");
        
        builder.Property(e => e.ExpYear)
            .HasColumnName("exp_year");
        
        builder.AddAuditConfigFields(dbName);

        // Relationship: CompanyBillingInfo -> AppCompany (many billing infos per company)
        builder.HasOne(d => d.Company)
            .WithMany(e => e.BillingInfos)
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relationship: CompanyBillingInfo -> Contact (contact for billing info)
        builder.HasOne(d => d.Contact)
            .WithMany() // contact may have many billing infos; we don't need a back-nav on Contact
            .HasForeignKey(d => d.ContactId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
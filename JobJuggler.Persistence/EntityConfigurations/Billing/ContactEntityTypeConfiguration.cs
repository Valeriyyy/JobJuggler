using JobJuggler.Common;
using JobJuggler.Domain.MetaModels;
using JobJuggler.Persistence.EntityConfigurations.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Billing;

public class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        var dbName = "contacts";
        builder.ToTable(dbName, DbSchemas.JobJuggler.ToSnakeCase());
        
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.CompanyId)
            .HasColumnName("company_id");
        
        builder.Property(e => e.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(50);
        
        builder.Property(e => e.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(e => e.Email)
            .HasColumnName("email")
            .IsRequired();
        
        builder.Property(e => e.Phone)
            .HasColumnName("phone")
            .HasMaxLength(20);
        
        builder.HasOne(contact => contact.Company)
            .WithMany(appCompany => appCompany.Contacts)
            .HasForeignKey(contact => contact.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
            
        
        builder.AddAuditConfigFields(dbName);
    }
}
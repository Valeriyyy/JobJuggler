using JobJuggler.Domain.IdentityModels;
using JobJuggler.Domain.MetaModels;
using JobJuggler.Persistence.EntityConfigurations.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Identity;
public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<AppCompany> {
    public void Configure(EntityTypeBuilder<AppCompany> builder)
    {
        var tableName = "companies";
        builder.ToTable(tableName, "identity");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(64);
        
        builder.Property(e => e.PrimaryContactId)
            .HasColumnName("primary_contact_id")
            .HasDefaultValue(null);
        
        builder.HasOne<Contact>(c => c.PrimaryContact)
            .WithMany()
            .HasForeignKey(c => c.PrimaryContactId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(c => c.Contacts)
            .WithOne(c => c.Company)
            .HasForeignKey(c => c.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.AddAuditConfigFields(tableName);
    }
}

using JobJuggler.Domain.IdentityModels;
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

        builder.AddAuditConfigFields(tableName);
    }
}

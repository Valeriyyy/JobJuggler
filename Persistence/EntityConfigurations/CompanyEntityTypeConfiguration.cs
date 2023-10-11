using Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;
public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<AppCompany> {
    public void Configure(EntityTypeBuilder<AppCompany> builder) {
        builder.ToTable("companies", "identity");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(64);

        builder.Property(e => e.DateCreated)
            .HasColumnName("date_created");

        builder.Property(e => e.CreatedById)
            .HasColumnName("created_by_id");

        builder.Property(e => e.DateLastModified)
            .HasColumnName("date_last_modified");

        builder.Property(e => e.LastModifiedById)
            .HasColumnName("last_modified_by_id");

        builder.Property(e => e.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false);

        builder.Property(e => e.DateDeleted)
            .HasColumnName("date_deleted");

        builder.Property(e => e.DeletedById)
            .HasColumnName("deleted_by_id");
    }
}

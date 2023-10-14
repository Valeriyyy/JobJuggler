using JobJuggler.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations;

public class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser> {
    public void Configure(EntityTypeBuilder<AppUser> builder) {
        builder.ToTable("app_users", "crystal_clean");

        builder.Property(e => e.DisplayName)
            .HasMaxLength(64)
            .HasColumnName("display_name");

        builder.Property(e => e.CompanyId)
            .HasColumnName("company_id");

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

        builder.HasOne(user => user.Company)
            .WithMany(company => company.Users)
            .HasForeignKey(user => user.CompanyId)
            .HasConstraintName("user_company_id_foreign");
    }
}
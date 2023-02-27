using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser> {
    public void Configure(EntityTypeBuilder<AppUser> builder) {
        builder.ToTable("app_users", "crystal_clean");

        //builder.HasKey(e => e.Guid)
        //      .HasName("app_user_pkey");

        //builder.HasIndex(e => e.Guid, "app_users_guid_unique")
        //            .IsUnique();

        //builder.Property(e => e.Guid)
        //        .HasColumnName("guid")
        //        .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .HasMaxLength(64)
            .HasColumnName("Name");

        //builder.Property(e => e.Email)
        //    .HasMaxLength(255)
        //    .HasColumnName("Email");

        builder.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(e => e.DateDeleted).HasColumnName("date_deleted");

        builder.Property(e => e.DateUpdated).HasColumnName("date_updated");

        builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
    }
}
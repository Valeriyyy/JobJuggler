using JobJuggler.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations;

public class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser> {
    public void Configure(EntityTypeBuilder<AppUser> builder) {
        builder.ToTable("asp_net_users", schema: "identity");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.AccessFailedCount)
            .HasColumnName("access_failed_count");

        builder.Property(e => e.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp");

        builder.Property(e => e.Email)
            .HasColumnName("email");

        builder.Property(e => e.EmailConfirmed)
            .HasColumnName("email_confirmed");

        builder.Property(e => e.LockoutEnabled)
            .HasColumnName("lockout_enabled");

        builder.Property(e => e.LockoutEnd)
            .HasColumnName("lockout_end");

        builder.Property(e => e.NormalizedEmail)
            .HasColumnName("normalized_email");

        builder.Property(e => e.NormalizedUserName)
            .HasColumnName("normalized_username");

        builder.Property(e => e.PasswordHash)
            .HasColumnName("password_hash");

        builder.Property(e => e.PhoneNumber)
            .HasColumnName("phone_number");

        builder.Property(e => e.PhoneNumberConfirmed)
            .HasColumnName("phone_number_confirmed");

        builder.Property(e => e.SecurityStamp)
            .HasColumnName("security_stamp");

        builder.Property(e => e.TwoFactorEnabled)
            .HasColumnName("two_factor_enabled");

        builder.Property(e => e.UserName)
            .HasColumnName("username");

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
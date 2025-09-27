using JobJuggler.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Identity;

public class AppUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        builder.ToTable("user_roles", "identity");
        
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Property(ur => ur.RoleId)
            .HasColumnName("role_id");
        
        builder.Property(ur => ur.UserId)
            .HasColumnName("user_id");

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.Roles)
            .HasForeignKey(ur => ur.UserId);

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
    }
}
using JobJuggler.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Identity;

public class AppRoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<AppRoleClaim>
{
    public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
    {
        builder.ToTable("role_claims", "identity");
        
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(e => e.RoleId)
            .HasColumnName("role_id");
        
        builder.Property(e => e.ClaimType)
            .HasColumnName("claim_type");
        
        builder.Property(e => e.ClaimValue)
            .HasColumnName("claim_value");
        
        builder.HasOne(e => e.Role)
            .WithMany(r => r.Claims)
            .HasForeignKey(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_role_claims_roles_role_id");
    }
}
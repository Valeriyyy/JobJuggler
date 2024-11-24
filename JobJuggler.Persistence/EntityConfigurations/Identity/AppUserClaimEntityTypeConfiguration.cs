using JobJuggler.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Identity;

public class AppUserClaimEntityTypeConfiguration : IEntityTypeConfiguration<AppUserClaim>
{
    public void Configure(EntityTypeBuilder<AppUserClaim> builder)
    {
        builder.ToTable("AppUserClaim", schema: "identity");
        
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id");
        
        builder.Property(e => e.ClaimType)
            .HasColumnName("claim_type");
        
        builder.Property(e => e.ClaimValue)
            .HasColumnName("claim_value");

        builder.HasOne(e => e.User)
            .WithMany(e => e.Claims)
            .HasForeignKey(e => e.UserId)
            .HasConstraintName("FK_user_claims_users_user_id");
    }
}
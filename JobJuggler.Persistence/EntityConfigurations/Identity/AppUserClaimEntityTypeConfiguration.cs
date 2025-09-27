using JobJuggler.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Identity;

public class AppUserClaimEntityTypeConfiguration : IEntityTypeConfiguration<AppUserClaim>
{
    public void Configure(EntityTypeBuilder<AppUserClaim> builder)
    {
        builder.ToTable("user_claims", schema: "identity");
        
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id");
        
        builder.Property(e => e.ClaimType)
            .HasColumnName("claim_type");
        
        builder.Property(e => e.ClaimValue)
            .HasColumnName("claim_value");

        builder.HasOne(uc => uc.User)
            .WithMany(u => u.Claims) // <-- Add ICollection<AppUserClaim> Claims in AppUser
            .HasForeignKey(uc => uc.UserId);
    }
}
using JobJuggler.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Identity;

public class AppUserTokenEntityTypeConfiguration : IEntityTypeConfiguration<AppUserToken>
{
    public void Configure(EntityTypeBuilder<AppUserToken> builder)
    {
        builder.ToTable("user_tokens", "identity");
        
        builder.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
        
        builder.Property(token => token.UserId)
            .HasColumnName("user_id");
        
        builder.Property(token => token.LoginProvider)
            .HasColumnName("login_provider");
        
        builder.Property(token => token.Name)
            .HasColumnName("name");
        
        builder.Property(token => token.Value)
            .HasColumnName("value");
        
        builder.HasOne(e => e.User)
            .WithMany(e => e.UserTokens)
            .HasForeignKey(e => e.UserId);
    }
}
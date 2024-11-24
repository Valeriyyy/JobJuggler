using JobJuggler.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Identity;

public class AppUserLoginEntityTypeConfiguration : IEntityTypeConfiguration<AppUserLogin>
{
    public void Configure(EntityTypeBuilder<AppUserLogin> builder)
    {
        builder.ToTable("user_logins", "identity");

        builder.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        
        builder.Property(e => e.LoginProvider)
            .HasColumnName("login_provider");
        
        builder.Property(e => e.ProviderKey)
            .HasColumnName("provider_key");
        
        builder.Property(e => e.ProviderDisplayName)
            .HasColumnName("provider_display_name");
        
        builder.Property(e => e.UserId)
            .HasColumnName("user_id");
        
        builder.HasOne(e => e.User)
            .WithMany(e => e.Logins)
            .HasForeignKey(e => e.UserId)
            .HasConstraintName("FK_user_logins_users_user_id");
    }
}
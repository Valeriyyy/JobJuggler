using JobJuggler.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Identity;

public class AppRoleEntityTypeConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable("roles", schema: "identity");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Name)
            .HasColumnName("name");
        
        builder.Property(e => e.NormalizedName)
            .HasColumnName("normalized_name");
        
        builder.Property(e => e.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp");
    }
}
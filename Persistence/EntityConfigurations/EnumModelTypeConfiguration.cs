using Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;
public class EnumModelTypeConfiguration : IEntityTypeConfiguration<EnumModel> {
    public void Configure(EntityTypeBuilder<EnumModel> builder) {
        builder.HasNoKey();
        builder.ToView("enums");
        builder.Property(e => e.Name)
           .HasColumnName("enum_name");
        builder.Property(e => e.Value)
           .HasColumnName("enum_value");
    }
}

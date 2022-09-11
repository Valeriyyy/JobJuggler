using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;
public class PaymentMethodEntityTypeConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("payment_methods");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasComment("The name of the payment method used");

        builder.Property(e => e.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(false)
            .HasComment("Indicates if the payment method is still meant to be used");
    }
}

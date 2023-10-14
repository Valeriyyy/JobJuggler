using JobJuggler.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace Persistence.EntityConfigurations;
public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location> {
    public void Configure(EntityTypeBuilder<Location> builder) {
        builder.ToTable("locations");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Guid)
            .HasColumnName("guid")
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(50);

        builder.Property(e => e.LocationType)
            .HasColumnName("location_type")
            .HasMaxLength(10);

        builder.Property(e => e.Street1)
            .HasColumnName("street1")
            .HasMaxLength(50);

        builder.Property(e => e.Street2)
            .HasColumnName("street2")
            .HasMaxLength(50);

        builder.Property(e => e.City)
            .HasColumnName("city")
            .HasMaxLength(25);

        builder.Property(e => e.State)
            .HasColumnName("state")
            .HasMaxLength(25);

        builder.Property(e => e.PostalCode)
            .HasColumnName("postal_code")
            .HasMaxLength(15);

        builder.Property(e => e.Country)
            .HasColumnName("country")
            .HasMaxLength(50);

        builder.Property(e => e.GateCode)
            .HasColumnName("gate_code")
            .HasMaxLength(10);

        builder.Property(e => e.Latitude)
            .HasColumnName("latitude")
            .HasPrecision(15, 7);

        builder.Property(e => e.Longitude)
            .HasColumnName("longitude")
            .HasPrecision(15, 7);

        builder.Property(e => e.Notes)
            .HasColumnName("notes")
            .HasDefaultValueSql("''::text");

        builder.Property(e => e.VectorAddress)
                    .HasColumnName("vector_address")
                    .HasComputedColumnSql($@"
                        to_tsvector('english'::regconfig, 
                        ((((CASE WHEN (street1 IS NOT NULL) THEN ((street1)::text || ' '::text) ELSE ''::text END ||
                            CASE WHEN (city IS NOT NULL) THEN ((city)::text || ' '::text) ELSE ''::text END) || 
                            CASE WHEN (state IS NOT NULL) THEN ((state)::text || ' '::text) ELSE ''::text END) || 
                            CASE WHEN (postal_code IS NOT NULL) THEN ((postal_code)::text || ' '::text) ELSE ''::text END) || 
                            (CASE WHEN (country IS NOT NULL) THEN country ELSE ''::character varying\nEND)::text))", true);

        builder.HasIndex(e => e.Guid, "location_guid_unique")
            .IsUnique();

        builder
            .HasIndex(e => e.VectorAddress, "locations_vector_address_idx")
            .HasMethod("GIN");
    }
}

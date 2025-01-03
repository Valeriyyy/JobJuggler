﻿using JobJuggler.Domain.Models;
using JobJuggler.Persistence.EntityConfigurations.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client> {
    public void Configure(EntityTypeBuilder<Client> builder) {
        builder.ToTable("clients", "main");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Guid)
            .HasColumnName("guid")
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(e => e.Name)
            .HasColumnName("name");

        builder.Property(e => e.Phone)
            .HasColumnName("phone");

        builder.Property(e => e.Email)
            .HasColumnName("email")
            .HasDefaultValue(null);
        
        builder.AddAuditConfigFields("clients");
    }
}

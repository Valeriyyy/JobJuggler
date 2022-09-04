﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220904012342_AddLocations")]
    partial class AddLocations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("crystal_clean")
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("guid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.HasKey("Id");

                    b.ToTable("clients", "crystal_clean");
                });

            modelBuilder.Entity("Domain.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("country");

                    b.Property<string>("GateCode")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("gate_code");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("guid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<decimal>("Latitude")
                        .HasPrecision(15, 7)
                        .HasColumnType("numeric(15,7)")
                        .HasColumnName("latitude");

                    b.Property<string>("LocationType")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("location_type");

                    b.Property<decimal>("Longitude")
                        .HasPrecision(15, 7)
                        .HasColumnType("numeric(15,7)")
                        .HasColumnName("longitude");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<string>("Notes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("notes")
                        .HasDefaultValueSql("''::text");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("postal_code");

                    b.Property<string>("State")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("state");

                    b.Property<string>("Street1")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("street1");

                    b.Property<string>("Street2")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("street2");

                    b.Property<NpgsqlTsVector>("VectorAddress")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tsvector")
                        .HasColumnName("vector_address")
                        .HasComputedColumnSql("\r\n                        to_tsvector('english'::regconfig, \r\n                        ((((CASE WHEN (street1 IS NOT NULL) THEN ((street1)::text || ' '::text) ELSE ''::text END ||\r\n                            CASE WHEN (city IS NOT NULL) THEN ((city)::text || ' '::text) ELSE ''::text END) || \r\n                            CASE WHEN (state IS NOT NULL) THEN ((state)::text || ' '::text) ELSE ''::text END) || \r\n                            CASE WHEN (postal_code IS NOT NULL) THEN ((postal_code)::text || ' '::text) ELSE ''::text END) || \r\n                            (CASE WHEN (country IS NOT NULL) THEN country ELSE ''::character varying\\nEND)::text))", true);

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Guid" }, "location_guid_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "VectorAddress" }, "locations_vector_address_idx");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex(new[] { "VectorAddress" }, "locations_vector_address_idx"), "GIN");

                    b.ToTable("locations", "crystal_clean");
                });
#pragma warning restore 612, 618
        }
    }
}

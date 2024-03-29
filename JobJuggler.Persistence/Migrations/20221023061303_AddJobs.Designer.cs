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

namespace JobJuggler.Persistence.Migrations {
    [DbContext(typeof(DataContext))]
    [Migration("20221023061303_AddJobs")]
    partial class AddJobs {
        protected override void BuildTargetModel(ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("main")
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Client", b => {
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
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("phone");

                b.HasKey("Id");

                b.ToTable("clients", "main");
            });

            modelBuilder.Entity("Domain.Models.Job", b => {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("id");

                NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                b.Property<string>("CancelReason")
                    .HasColumnType("text")
                    .HasColumnName("cancel_reason")
                    .HasComment("Brief explanation of why the job was canceled");

                b.Property<DateTime?>("CanceledDate")
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("canceled_date")
                    .HasComment("The date time the job was canceled");

                b.Property<int>("ClientId")
                    .HasColumnType("integer")
                    .HasColumnName("client_id")
                    .HasComment("The main person or business requesting the service");

                b.Property<DateTime?>("CompletedDate")
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("completed_date")
                    .HasComment("The date time the job was completed");

                b.Property<Guid>("Guid")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid")
                    .HasColumnName("guid")
                    .HasDefaultValueSql("uuid_generate_v4()");

                b.Property<bool?>("IsCanceled")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("boolean")
                    .HasDefaultValue(false)
                    .HasColumnName("is_canceled")
                    .HasComment("Indicates if the job has been canceled");

                b.Property<bool?>("IsCompleted")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("boolean")
                    .HasDefaultValue(false)
                    .HasColumnName("is_completed")
                    .HasComment("Indicates if the job is fully complete meaning all payments have cleared");

                b.Property<int>("LocationId")
                    .HasColumnType("integer")
                    .HasColumnName("location_id");

                b.Property<string>("Notes")
                    .HasColumnType("text")
                    .HasColumnName("notes")
                    .HasComment("General notes about the order");

                b.Property<decimal>("Price")
                    .HasPrecision(6, 2)
                    .HasColumnType("numeric(6,2)")
                    .HasColumnName("price")
                    .HasComment("The total sum price of all the line items related to the order");

                b.Property<DateTime>("ScheduledArrivalEndDate")
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("scheduled_arrival_end_date")
                    .HasComment("The scheduled end datetime that the vendor will arrive at the job location");

                b.Property<DateTime>("ScheduledArrivalStartDate")
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("scheduled_arrival_start_date")
                    .HasComment("The scheduled beginning datetime that the vendor will arrive at the job location");

                b.Property<DateTime>("ScheduledDate")
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("scheduled_date")
                    .HasComment("The date the job was booked");

                b.Property<DateTime?>("StartedDate")
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("started_date")
                    .HasComment("The date time the job was started");

                b.HasKey("Id");

                b.HasIndex("ClientId");

                b.HasIndex("LocationId");

                b.ToTable("jobs", "main");
            });

            modelBuilder.Entity("Domain.Models.Location", b => {
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

                b.Property<decimal?>("Latitude")
                    .HasPrecision(15, 7)
                    .HasColumnType("numeric(15,7)")
                    .HasColumnName("latitude");

                b.Property<string>("LocationType")
                    .HasMaxLength(10)
                    .HasColumnType("character varying(10)")
                    .HasColumnName("location_type");

                b.Property<decimal?>("Longitude")
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

                b.ToTable("locations", "main");
            });

            modelBuilder.Entity("Domain.Models.Job", b => {
                b.HasOne("Domain.Models.Client", "Client")
                    .WithMany("Jobs")
                    .HasForeignKey("ClientId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("job_client_id_foreign");

                b.HasOne("Domain.Models.Location", "Location")
                    .WithMany("Jobs")
                    .HasForeignKey("LocationId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("job_location_id_foreign");

                b.Navigation("Client");

                b.Navigation("Location");
            });

            modelBuilder.Entity("Domain.Models.Client", b => {
                b.Navigation("Jobs");
            });

            modelBuilder.Entity("Domain.Models.Location", b => {
                b.Navigation("Jobs");
            });
#pragma warning restore 612, 618
        }
    }
}

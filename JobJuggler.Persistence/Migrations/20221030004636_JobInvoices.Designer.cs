﻿// <auto-generated />
using System;
using JobJuggler.Domain.Enums;
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
    [Migration("20221030004636_JobInvoices")]
    partial class JobInvoices {
        protected override void BuildTargetModel(ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("main")
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "main", "price_type", new[] { "none", "per_unit", "flat_rate" });
            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
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

            modelBuilder.Entity("Domain.Models.Invoice", b => {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("id");

                NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                b.Property<int>("ConsigneeId")
                    .HasColumnType("integer")
                    .HasColumnName("consignee_id");

                b.Property<DateTime?>("DateClosed")
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("date_closed")
                    .HasComment("The final date when the invoice was fully processed and all the payment has cleared");

                b.Property<DateTime?>("DateInvoiced")
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("date_invoiced")
                    .HasComment("The date the customer was sent the invoice");

                b.Property<DateTime?>("DatePaid")
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("date_paid")
                    .HasComment("The latest date the payment was submitted");

                b.Property<Guid>("Guid")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid")
                    .HasColumnName("guid")
                    .HasDefaultValueSql("uuid_generate_v4()");

                b.Property<bool>("IsPaid")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("boolean")
                    .HasDefaultValue(false)
                    .HasColumnName("is_paid")
                    .HasComment("Indicates if the invoice has been fully paid for");

                b.Property<int>("JobId")
                    .HasColumnType("integer")
                    .HasColumnName("job_id");

                b.Property<int?>("PaymentMethodId")
                    .IsRequired()
                    .HasColumnType("integer")
                    .HasColumnName("payment_method_id")
                    .HasComment("The method used for submitting payment by the consignee");

                b.Property<string>("ReferenceNumber")
                    .HasColumnType("text")
                    .HasColumnName("reference_number")
                    .HasComment("A unique number used for easily identifying jobs with customers");

                b.Property<decimal>("TotalPrice")
                    .HasColumnType("numeric")
                    .HasColumnName("total_price")
                    .HasComment("The calculated total from the invoice lines. Not meant to be directly edited");

                b.HasKey("Id");

                b.HasIndex("ConsigneeId");

                b.HasIndex("JobId")
                    .IsUnique();

                b.HasIndex("PaymentMethodId");

                b.ToTable("invoices", "main");
            });

            modelBuilder.Entity("Domain.Models.InvoiceLine", b => {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("id");

                NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                b.Property<Guid>("Guid")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid")
                    .HasColumnName("guid")
                    .HasDefaultValueSql("uuid_generate_v4()");

                b.Property<int>("InvoiceId")
                    .HasColumnType("integer")
                    .HasColumnName("invoice_id");

                b.Property<int>("ItemId")
                    .HasColumnType("integer")
                    .HasColumnName("item_id")
                    .HasComment("The item that is on the invoice");

                b.Property<int>("NumOfUnits")
                    .HasColumnType("integer")
                    .HasColumnName("num_of_units")
                    .HasComment("The number of the same line items in the invoice");

                b.Property<decimal>("Price")
                    .HasColumnType("numeric")
                    .HasColumnName("price")
                    .HasComment("The total price of the item from the quantity");

                b.HasKey("Id");

                b.HasIndex("InvoiceId");

                b.HasIndex("ItemId");

                b.ToTable("invoice_lines", "main");
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

            modelBuilder.Entity("Domain.Models.LineItem", b => {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("id");

                NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                b.Property<decimal?>("BasePrice")
                    .HasColumnType("numeric")
                    .HasColumnName("base_price")
                    .HasComment("The default price for the item. Can be overridden when put on an invoice");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("name");

                b.Property<PriceType>("PriceType")
                    .HasColumnType("price_type")
                    .HasColumnName("price_type");

                b.HasKey("Id");

                b.ToTable("line_items", "main");
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

            modelBuilder.Entity("Domain.Models.PaymentMethod", b => {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("id");

                NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                b.Property<bool>("IsActive")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("boolean")
                    .HasDefaultValue(false)
                    .HasColumnName("is_active")
                    .HasComment("Indicates if the payment method is still meant to be used");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("name")
                    .HasComment("The name of the payment method used");

                b.HasKey("Id");

                b.ToTable("payment_methods", "main");
            });

            modelBuilder.Entity("Domain.Models.Invoice", b => {
                b.HasOne("Domain.Models.Client", "Consignee")
                    .WithMany("Invoices")
                    .HasForeignKey("ConsigneeId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("invoice_consignee_id_foreign");

                b.HasOne("Domain.Models.Job", "Job")
                    .WithOne("Invoice")
                    .HasForeignKey("Domain.Models.Invoice", "JobId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("job_invoice_id_foreign");

                b.HasOne("Domain.Models.PaymentMethod", "PaymentMethod")
                    .WithMany("Invoices")
                    .HasForeignKey("PaymentMethodId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("invoice_payment_method_id_foreign");

                b.Navigation("Consignee");

                b.Navigation("Job");

                b.Navigation("PaymentMethod");
            });

            modelBuilder.Entity("Domain.Models.InvoiceLine", b => {
                b.HasOne("Domain.Models.Invoice", "Invoice")
                    .WithMany("Lines")
                    .HasForeignKey("InvoiceId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("line_invoice_id_foreign");

                b.HasOne("Domain.Models.LineItem", "Item")
                    .WithMany("Invoices")
                    .HasForeignKey("ItemId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("line_item_id_foreign");

                b.Navigation("Invoice");

                b.Navigation("Item");
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
                b.Navigation("Invoices");

                b.Navigation("Jobs");
            });

            modelBuilder.Entity("Domain.Models.Invoice", b => {
                b.Navigation("Lines");
            });

            modelBuilder.Entity("Domain.Models.Job", b => {
                b.Navigation("Invoice")
                    .IsRequired();
            });

            modelBuilder.Entity("Domain.Models.LineItem", b => {
                b.Navigation("Invoices");
            });

            modelBuilder.Entity("Domain.Models.Location", b => {
                b.Navigation("Jobs");
            });

            modelBuilder.Entity("Domain.Models.PaymentMethod", b => {
                b.Navigation("Invoices");
            });
#pragma warning restore 612, 618
        }
    }
}

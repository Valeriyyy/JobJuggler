using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;
public class JobEntityTypeConfiguration : IEntityTypeConfiguration<Job> {
    public void Configure(EntityTypeBuilder<Job> builder) {
        builder.ToTable("jobs");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Guid)
            .HasColumnName("guid")
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(e => e.ClientId)
            .HasColumnName("client_id")
            .HasComment("The main person or business requesting the service");

        builder.Property(e => e.LocationId)
            .HasColumnName("location_id");

        builder.Property(e => e.Price)
            .HasColumnName("price")
            .HasPrecision(6, 2)
            .HasComment("The total sum price of all the line items related to the order");

        builder.Property(e => e.Notes)
            .HasColumnName("notes")
            .HasComment("General notes about the order");

        builder.Property(e => e.IsCompleted)
            .HasColumnName("is_completed")
            .HasDefaultValue(false)
            .HasComment("Indicates if the job is fully complete meaning all payments have cleared");

        builder.Property(e => e.IsCanceled)
            .HasColumnName("is_canceled")
            .HasDefaultValue(false)
            .HasComment("Indicates if the job has been canceled");

        builder.Property(e => e.CancelReason)
            .HasColumnName("cancel_reason")
            .HasDefaultValue(null)
            .HasComment("Brief explanation of why the job was canceled");

        builder.Property(e => e.ScheduledDate)
            .HasColumnName("scheduled_date")
            .HasComment("The date the job was booked");

        builder.Property(e => e.ScheduledArrivalStartDate)
            .HasColumnName("scheduled_arrival_start_date")
            .HasComment("The scheduled beginning datetime that the vendor will arrive at the job location");

        builder.Property(e => e.ScheduledArrivalEndDate)
            .HasColumnName("scheduled_arrival_end_date")
            .HasComment("The scheduled end datetime that the vendor will arrive at the job location");

        builder.Property(e => e.StartedDate)
            .HasColumnName("started_date")
            .HasDefaultValue(null)
            .HasComment("The date time the job was started");

        builder.Property(e => e.CompletedDate)
            .HasColumnName("completed_date")
            .HasDefaultValue(null)
            .HasComment("The date time the job was completed");

        builder.Property(e => e.CanceledDate)
            .HasColumnName("canceled_date")
            .HasDefaultValue(null)
            .HasComment("The date time the job was canceled");

        builder.HasOne(job => job.Client)
            .WithMany(client => client.Jobs)
            .HasForeignKey(job => job.ClientId)
            .HasConstraintName("job_client_id_foreign");

        builder.HasOne(job => job.Location)
            .WithMany(location => location.Jobs)
            .HasForeignKey(job => job.LocationId)
            .HasConstraintName("job_location_id_foreign");

        builder.HasOne(job => job.Invoice)
            .WithOne(invoice => invoice.Job)
            .HasForeignKey<Invoice>(invoice => invoice.JobId)
            .HasConstraintName("job_invoice_id_foreign");
    }
}

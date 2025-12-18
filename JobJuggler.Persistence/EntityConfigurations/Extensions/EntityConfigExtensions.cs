using JobJuggler.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Extensions;

public static class EntityConfigExtensions
{
    public static void AddAuditConfigFields<T>(this EntityTypeBuilder<T> builder, string entityDbName) where T : BaseEntity
    {
        builder.Property(x => x.DateCreated)
            .HasColumnName("date_created")
            .HasDefaultValueSql("now() at time zone \'UTC\'");

        builder.Property(x => x.CreatedById)
            .HasColumnName("created_by_id");
        
        builder.Property(x => x.DateLastModified)
            .HasColumnName("date_last_modified")
            .HasDefaultValue(null);
        
        builder.Property(x => x.LastModifiedById)
            .HasColumnName("last_modified_by_id")
            .HasDefaultValue(null);
        
        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false);
        
        builder.Property(x => x.DateDeleted)
            .HasColumnName("date_deleted")
            .HasDefaultValue(null);
        
        builder.Property(x => x.DeletedById)
            .HasColumnName("deleted_by_id")
            .HasDefaultValue(null);
        
        builder.HasIndex(x => x.CreatedById)
            .HasDatabaseName($"IX_{entityDbName}_created_by_id");
        
        builder.HasIndex(x => x.LastModifiedById)
            .HasDatabaseName($"IX_{entityDbName}_last_modified_by_id");
        
        builder.HasIndex(x => x.DeletedById)
            .HasDatabaseName($"IX_{entityDbName}_deleted_by_id");
        
        builder.HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.LastModifiedBy)
            .WithMany()
            .HasForeignKey(x => x.LastModifiedById)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.DeletedBy)
            .WithMany()
            .HasForeignKey(x => x.DeletedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
using JobJuggler.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobJuggler.Persistence.EntityConfigurations.Extensions;

public static class EntityConfigExtensions
{
    public static void AddAuditConfigFields<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
    {
        builder.Property(x => x.DateCreated)
            .HasColumnType("date_created");

        builder.Property(x => x.CreatedById)
            .HasColumnName("created_by_id");
        
        builder.Property(x => x.DateLastModified)
            .HasColumnName("date_last_modified");
        
        builder.Property(x => x.LastModifiedById)
            .HasColumnName("last_modified_by_id");
        
        builder.Property(x => x.DateDeleted)
            .HasColumnName("date_deleted");
        
        builder.Property(x => x.DeletedById)
            .HasColumnName("deleted_by_id");
        
        // gotta figure out how these lookup properties work
        // with generics
        // builder.HasOne(x => x.CreatedBy)
        //     .WithMany(u => T)
    }
}
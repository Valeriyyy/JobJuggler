using JobJuggler.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace JobJuggler.Persistence.Interceptors;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly Func<int> GetUserId;

    public AuditInterceptor(Func<int> getUserId)
    {
        GetUserId = getUserId;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            UpdateAuditableEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditableEntities(DbContext context)
    {
        var utcNow = DateTime.UtcNow;
        var id = GetUserId.Invoke();
        var entities = context.ChangeTracker.Entries<BaseEntity>().ToList();

        foreach (var entity in entities)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    SetCurrentPropertyValue(
                        entity, nameof(BaseEntity.DateCreated), utcNow);
                    SetCurrentPropertyValue(
                        entity, nameof(BaseEntity.CreatedById), id);
                    break;
                case EntityState.Modified:
                    SetCurrentPropertyValue(
                        entity, nameof(BaseEntity.DateLastModified), utcNow);
                    SetCurrentPropertyValue(
                        entity, nameof(BaseEntity.LastModifiedById), id);
                    break;
                case EntityState.Deleted:
                    entity.State = EntityState.Modified;
                    SetCurrentPropertyValue(
                        entity, nameof(BaseEntity.IsDeleted), true);
                    SetCurrentPropertyValue(
                        entity, nameof(BaseEntity.DateDeleted), utcNow);
                    SetCurrentPropertyValue(
                        entity, nameof(BaseEntity.DeletedById), id);
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
    
    static void SetCurrentPropertyValue(
        EntityEntry entry,
        string propertyName,
        object val) =>
        entry.Property(propertyName).CurrentValue = val;
    

}
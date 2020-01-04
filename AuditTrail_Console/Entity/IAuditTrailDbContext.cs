using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace AuditTrail_Console.Entity
{
    public interface IAuditTrailDbContext : IDisposable
    {
        DbSet<AuditEntry> AuditEntries { get; set; } // AuditEntries
        DbSet<AuditEntryProperty> AuditEntryProperties { get; set; } // AuditEntryProperties
        DbSet<HistoryTrackingAudit> HistoryTrackingAudits { get; set; } // HistoryTrackingAudit
        DbSet<HistoryTrackingValueAudit> HistoryTrackingValueAudits { get; set; } // HistoryTrackingValueAudit
        DbSet<Person> People { get; set; } // Persons

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        Database Database { get; }
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbEntityEntry Entry(object entity);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();
    }
}
// </auto-generated>



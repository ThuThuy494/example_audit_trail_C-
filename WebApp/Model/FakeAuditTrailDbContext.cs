using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Model.Entity;

namespace WebApp.Model
{
    public class FakeAuditTrailDbContext : IAuditTrailDbContext
    {
        public DbSet<AuditEntry> AuditEntries { get; set; } // AuditEntries
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; } // AuditEntryProperties
        public DbSet<HistoryTrackingAudit> HistoryTrackingAudits { get; set; } // HistoryTrackingAudit
        public DbSet<HistoryTrackingValueAudit> HistoryTrackingValueAudits { get; set; } // HistoryTrackingValueAudit
        public DbSet<Person> People { get; set; } // Persons

        public FakeAuditTrailDbContext()
        {
            _changeTracker = null;
            _configuration = null;
            _database = null;

            AuditEntries = new FakeDbSet<AuditEntry>("AuditEntryId");
            AuditEntryProperties = new FakeDbSet<AuditEntryProperty>("AuditEntryPropertyId");
            HistoryTrackingAudits = new FakeDbSet<HistoryTrackingAudit>("Id");
            HistoryTrackingValueAudits = new FakeDbSet<HistoryTrackingValueAudit>("Id");
            People = new FakeDbSet<Person>("Id");

        }

        public int SaveChangesCount { get; private set; }
        public int SaveChanges()
        {
            ++SaveChangesCount;
            return 1;
        }

        public Task<int> SaveChangesAsync()
        {
            ++SaveChangesCount;
            return Task<int>.Factory.StartNew(() => 1);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ++SaveChangesCount;
            return Task<int>.Factory.StartNew(() => 1, cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private DbChangeTracker _changeTracker;

        public DbChangeTracker ChangeTracker { get { return _changeTracker; } }

        private DbContextConfiguration _configuration;

        public DbContextConfiguration Configuration { get { return _configuration; } }

        private Database _database;

        public Database Database { get { return _database; } }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public DbEntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            throw new NotImplementedException();
        }

        public DbSet Set(Type entityType)
        {
            throw new NotImplementedException();
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
// </auto-generated>



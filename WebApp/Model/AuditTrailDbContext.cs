using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Model.Entity;
using Z.EntityFramework.Plus;
using AuditEntry = WebApp.Model.Entity.AuditEntry;
using AuditEntryProperty = WebApp.Model.Entity.AuditEntryProperty;

namespace WebApp.Model
{
    public delegate void TableUpdated(IEnumerable<DbEntityEntry> entities);
    public delegate void LogHistoryTracker(IEnumerable<DbEntityEntry> entities);
    public class AuditTrailDbContext : DbContext, IAuditTrailDbContext
    {
        public TableUpdated TableUpdatedEvent;
        public LogHistoryTracker LogHistoryTrackerEvent;
        public IComponentContext Container;

        public DbSet<AuditEntry> AuditEntries { get; set; } // AuditEntries
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; } // AuditEntryProperties
        public DbSet<HistoryTrackingAudit> HistoryTrackingAudits { get; set; } // HistoryTrackingAudit
        public DbSet<HistoryTrackingValueAudit> HistoryTrackingValueAudits { get; set; } // HistoryTrackingValueAudit
        public DbSet<Person> People { get; set; } // Persons
        public DbSet<PersonDetail> PeopleDetail { get; set; } // PersonDetails
        public DbSet<Category> CategoryDetail { get; set; } // PersonDetails

        static AuditTrailDbContext()
        {
            System.Data.Entity.Database.SetInitializer<AuditTrailDbContext>(null);
            System.Data.Entity.Database.SetInitializer<AuditTrailEFPlusDbContext>(null);

            AuditManager.DefaultConfiguration.Exclude(x => true);
            AuditManager.DefaultConfiguration.IncludeDataAnnotation();
            AuditManager.DefaultConfiguration.ExcludeDataAnnotation();
            AuditManager.DefaultConfiguration.DataAnnotationDisplayName();

            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
            {
                // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
                var customAuditEntries = audit.Entries.Select(x => Import(x));
                (context as AuditTrailDbContext).AuditEntries.AddRange(customAuditEntries);
            };
        }

        /// <inheritdoc />
        public AuditTrailDbContext()
            : base("AuditTrailDbContext")
        {
        }

        /// <inheritdoc />
        public AuditTrailDbContext(string connectionString)
            : base(connectionString)
        {
        }

        /// <inheritdoc />
        public AuditTrailDbContext(string connectionString, DbCompiledModel model)
            : base(connectionString, model)
        {
        }

        /// <inheritdoc />
        public AuditTrailDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <inheritdoc />
        public AuditTrailDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <inheritdoc />
        public AuditTrailDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public bool IsSqlParameterNull(SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == DBNull.Value);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AuditEntryConfiguration());
            modelBuilder.Configurations.Add(new AuditEntryPropertyConfiguration());
            modelBuilder.Configurations.Add(new HistoryTrackingAuditConfiguration());
            modelBuilder.Configurations.Add(new HistoryTrackingValueAuditConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new PersonDetailConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AuditEntryConfiguration(schema));
            modelBuilder.Configurations.Add(new AuditEntryPropertyConfiguration(schema));
            modelBuilder.Configurations.Add(new HistoryTrackingAuditConfiguration(schema));
            modelBuilder.Configurations.Add(new HistoryTrackingValueAuditConfiguration(schema));
            modelBuilder.Configurations.Add(new PersonConfiguration(schema));
            modelBuilder.Configurations.Add(new PersonDetailConfiguration(schema));
            return modelBuilder;
        }
        public override int SaveChanges()
        {
            SaveChangeDetail();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            SaveChangeDetail();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            SaveChangeDetail();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SaveChangeDetail()
        {
            var changedEntities = ChangeTracker.Entries().ToList();
            // Maybe clear cache for unchanged entites I don't filter unchange
            if (TableUpdatedEvent != null) TableUpdatedEvent(changedEntities);
            // NO need log history, delete workflow for unchange entities
            changedEntities = changedEntities.Where(t => t.State != EntityState.Unchanged).ToList();

            if (LogHistoryTrackerEvent != null) LogHistoryTrackerEvent(changedEntities);
        }

        public static Entity.AuditEntry Import(Z.EntityFramework.Plus.AuditEntry entry)
        {
            var customAuditEntry = new Entity.AuditEntry
            {
                EntitySetName = entry.EntitySetName,
                EntityTypeName = entry.EntityTypeName,
                State = (int)entry.State,
                StateName = entry.StateName,
                CreatedBy = entry.CreatedBy,
                CreatedDate = entry.CreatedDate,
                UpdateDate = entry.CreatedDate
            };

            //Auditlog not allow exclude primary.So you must add on primary key here
            //Get Primary key of entity
            var excludePrimaryKeys = entry.Entry.EntityKey.EntityKeyValues.Select(x => x.Key).ToList();

            customAuditEntry.AuditEntryProperties = entry.Properties.Select(x => Import(x))
                .Where(x => !excludePrimaryKeys.Contains(x.PropertyName))//exclude primary key
                .ToList();

            return customAuditEntry;
        }

        public static Entity.AuditEntryProperty Import(Z.EntityFramework.Plus.AuditEntryProperty property)
        {
            var customAuditEntry = new Entity.AuditEntryProperty
            {
                RelationName = property.RelationName,
                PropertyName = property.PropertyName,
                OldValue = property.OldValueFormatted,
                NewValue = property.NewValueFormatted,
                AuditEntryId = property.AuditEntryID,
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };

            return customAuditEntry;
        }
    }
}
// </auto-generated>



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
using System.Text;
using System.Threading.Tasks;
using WebApp.Model.Entity;
using Z.EntityFramework.Plus;

namespace WebApp.Model
{
    public class AuditTrailEFPlusDbContext : DbContext, IAuditTrailDbContext
    {
        public DbSet<Entity.AuditEntry> AuditEntries { get; set; } // AuditEntries
        public DbSet<Entity.AuditEntryProperty> AuditEntryProperties { get; set; } // AuditEntryProperties
        public DbSet<Person> People { get; set; } // Persons
        public DbSet<HistoryTrackingAudit> HistoryTrackingAudits { get; set; }
        public DbSet<HistoryTrackingValueAudit> HistoryTrackingValueAudits { get; set; }
        public DbSet<PersonDetail> PersonDetails { get; set; }

        static AuditTrailEFPlusDbContext()
        {
            System.Data.Entity.Database.SetInitializer<AuditTrailEFPlusDbContext>(null);
            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
            {
                // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
                var customAuditEntries = audit.Entries.Select(x => Import(x));
                (context as AuditTrailDbContext).AuditEntries.AddRange(customAuditEntries);
            };
        }

        /// <inheritdoc />
        public AuditTrailEFPlusDbContext()
            : base("AuditTraiEFlDbContext")
        {
        }

        /// <inheritdoc />
        public AuditTrailEFPlusDbContext(string connectionString)
            : base(connectionString)
        {
        }

        /// <inheritdoc />
        public AuditTrailEFPlusDbContext(string connectionString, DbCompiledModel model)
            : base(connectionString, model)
        {
        }

        /// <inheritdoc />
        public AuditTrailEFPlusDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <inheritdoc />
        public AuditTrailEFPlusDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <inheritdoc />
        public AuditTrailEFPlusDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
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
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new PersonDetailConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AuditEntryConfiguration(schema));
            modelBuilder.Configurations.Add(new AuditEntryPropertyConfiguration(schema));
            modelBuilder.Configurations.Add(new PersonConfiguration(schema));
            modelBuilder.Configurations.Add(new PersonDetailConfiguration(schema));

            return modelBuilder;
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
                CreatedDate = entry.CreatedDate
            };

            customAuditEntry.AuditEntryProperties = entry.Properties.Select(x => Import(x)).ToList();

            return customAuditEntry;
        }

        public static Entity.AuditEntryProperty Import(Z.EntityFramework.Plus.AuditEntryProperty property)
        {
            var customAuditEntry = new Entity.AuditEntryProperty
            {
                RelationName = property.RelationName,
                PropertyName = property.PropertyName,
                OldValue = property.OldValueFormatted,
                NewValue = property.NewValueFormatted
            };

            return customAuditEntry;
        }
    }
}

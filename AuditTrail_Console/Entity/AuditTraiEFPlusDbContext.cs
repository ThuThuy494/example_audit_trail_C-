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
using Z.EntityFramework.Plus;

namespace AuditTrail_Console.Entity
{
    public class AuditTraiEFPlusDbContext : DbContext, IAuditTrailDbContext
    {
        public DbSet<AuditEntry> AuditEntries { get; set; } // AuditEntries
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; } // AuditEntryProperties
        public DbSet<Person> People { get; set; } // Persons
        public DbSet<HistoryTrackingAudit> HistoryTrackingAudits { get; set; }
        public DbSet<HistoryTrackingValueAudit> HistoryTrackingValueAudits { get; set; }
        public DbSet<PersonDetail> PersonDetails { get; set; }

        static AuditTraiEFPlusDbContext()
        {
            System.Data.Entity.Database.SetInitializer<AuditTraiEFPlusDbContext>(null);
            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
            {
                // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
                var customAuditEntries = audit.Entries.Select(x => Import(x));
                (context as AuditTrailDbContext).AuditEntries.AddRange(customAuditEntries);
            };
        }

        /// <inheritdoc />
        public AuditTraiEFPlusDbContext()
            : base("AuditTraiEFlDbContext")
        {
        }

        /// <inheritdoc />
        public AuditTraiEFPlusDbContext(string connectionString)
            : base(connectionString)
        {
        }

        /// <inheritdoc />
        public AuditTraiEFPlusDbContext(string connectionString, DbCompiledModel model)
            : base(connectionString, model)
        {
        }

        /// <inheritdoc />
        public AuditTraiEFPlusDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <inheritdoc />
        public AuditTraiEFPlusDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <inheritdoc />
        public AuditTraiEFPlusDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
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

        public static AuditEntry Import(Z.EntityFramework.Plus.AuditEntry entry)
        {
            var customAuditEntry = new AuditEntry
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

        public static AuditEntryProperty Import(Z.EntityFramework.Plus.AuditEntryProperty property)
        {
            var customAuditEntry = new AuditEntryProperty
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

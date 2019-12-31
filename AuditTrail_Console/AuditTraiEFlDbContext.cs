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

namespace AuditTrail_Console
{
    public delegate void TableUpdated(IEnumerable<DbEntityEntry> entities);
    public delegate void LogHistoryTracker(IEnumerable<DbEntityEntry> entities);
    public class AuditTraiEFlDbContext : DbContext, IAuditTrailDbContext
    {
        public TableUpdated TableUpdatedEvent;
        public LogHistoryTracker LogHistoryTrackerEvent;

        public DbSet<AuditEntry> AuditEntries { get; set; } // AuditEntries
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; } // AuditEntryProperties
        public DbSet<AuditLog> AuditLogs { get; set; } // AuditLog
        public DbSet<Person> People { get; set; } // Persons

        static AuditTraiEFlDbContext()
        {
            System.Data.Entity.Database.SetInitializer<AuditTraiEFlDbContext>(null);
        }

        /// <inheritdoc />
        public AuditTraiEFlDbContext()
            : base("Name=AuditTraiEFlDbContext")
        {
        }

        /// <inheritdoc />
        public AuditTraiEFlDbContext(string connectionString)
            : base(connectionString)
        {
        }

        /// <inheritdoc />
        public AuditTraiEFlDbContext(string connectionString, DbCompiledModel model)
            : base(connectionString, model)
        {
        }

        /// <inheritdoc />
        public AuditTraiEFlDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <inheritdoc />
        public AuditTraiEFlDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <inheritdoc />
        public AuditTraiEFlDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
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
            modelBuilder.Configurations.Add(new AuditLogConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());

            // Indexes        
            modelBuilder.Entity<AuditEntry>()
                .Property(e => e.AuditEntryId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("PK_dbo.AuditEntries", 1) { IsUnique = true, IsClustered = true })
                );


            modelBuilder.Entity<AuditEntryProperty>()
                .Property(e => e.AuditEntryPropertyId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("PK_dbo.AuditEntryProperties", 1) { IsUnique = true, IsClustered = true })
                );


            modelBuilder.Entity<AuditEntryProperty>()
                .Property(e => e.AuditEntryId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_AuditEntryID", 1))
                );


            modelBuilder.Entity<AuditLog>()
                .Property(e => e.Id)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("PK__AuditLog__3214EC07ABF71F3A", 1) { IsUnique = true, IsClustered = true })
                );


            modelBuilder.Entity<Person>()
                .Property(e => e.Id)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("PK__Persons__3214EC07B1B20D6A", 1) { IsUnique = true, IsClustered = true })
                );

        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AuditEntryConfiguration(schema));
            modelBuilder.Configurations.Add(new AuditEntryPropertyConfiguration(schema));
            modelBuilder.Configurations.Add(new AuditLogConfiguration(schema));
            modelBuilder.Configurations.Add(new PersonConfiguration(schema));

            return modelBuilder;
        }
    }
}

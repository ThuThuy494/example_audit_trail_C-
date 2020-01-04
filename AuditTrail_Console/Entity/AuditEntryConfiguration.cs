using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AuditTrail_Console.Entity
{
    // AuditEntries
    public class AuditEntryConfiguration : EntityTypeConfiguration<AuditEntry>
    {
        public AuditEntryConfiguration()
            : this("dbo")
        {
        }

        public AuditEntryConfiguration(string schema)
        {
            ToTable("AuditEntries", schema);
            HasKey(x => x.AuditEntryId);

            Property(x => x.AuditEntryId).HasColumnName(@"AuditEntryID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.EntitySetName).HasColumnName(@"EntitySetName").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.EntityTypeName).HasColumnName(@"EntityTypeName").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.State).HasColumnName(@"State").HasColumnType("int").IsRequired();
            Property(x => x.StateName).HasColumnName(@"StateName").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.UpdateDate).HasColumnName(@"UpdateDate").HasColumnType("datetime").IsRequired();
        }
    }

}
// </auto-generated>



using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AuditTrail_Console.Entity
{
    // AuditEntryProperties
    public class AuditEntryPropertyConfiguration : EntityTypeConfiguration<AuditEntryProperty>
    {
        public AuditEntryPropertyConfiguration()
            : this("dbo")
        {
        }

        public AuditEntryPropertyConfiguration(string schema)
        {
            ToTable("AuditEntryProperties", schema);
            HasKey(x => x.AuditEntryPropertyId);

            Property(x => x.AuditEntryPropertyId).HasColumnName(@"AuditEntryPropertyID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.AuditEntryId).HasColumnName(@"AuditEntryID").HasColumnType("int").IsRequired();
            Property(x => x.RelationName).HasColumnName(@"RelationName").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.PropertyName).HasColumnName(@"PropertyName").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.OldValue).HasColumnName(@"OldValue").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.NewValue).HasColumnName(@"NewValue").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.UpdateDate).HasColumnName(@"UpdateDate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            HasRequired(a => a.AuditEntry).WithMany(b => b.AuditEntryProperties).HasForeignKey(c => c.AuditEntryId); // FK_dbo.AuditEntryProperties_dbo.AuditEntries_AuditEntryID
        }
    }

}
// </auto-generated>



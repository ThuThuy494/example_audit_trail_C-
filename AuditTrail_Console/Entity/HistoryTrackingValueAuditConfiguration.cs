using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AuditTrail_Console.Entity
{
    // HistoryTrackingValueAudit
    public class HistoryTrackingValueAuditConfiguration : EntityTypeConfiguration<HistoryTrackingValueAudit>
    {
        public HistoryTrackingValueAuditConfiguration()
            : this("dbo")
        {
        }

        public HistoryTrackingValueAuditConfiguration(string schema)
        {
            ToTable("HistoryTrackingValueAudit", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("uniqueidentifier").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ColumnName).HasColumnName(@"ColumnName").HasColumnType("nvarchar(max)").IsRequired();
            Property(x => x.OldValue).HasColumnName(@"OldValue").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.NewValue).HasColumnName(@"NewValue").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.Action).HasColumnName(@"Action").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            Property(x => x.InsertedById).HasColumnName(@"InsertedById").HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.InsertedAt).HasColumnName(@"InsertedAt").HasColumnType("datetime").IsRequired();
            Property(x => x.UpdatedById).HasColumnName(@"UpdatedById").HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.UpdatedAt).HasColumnName(@"UpdatedAt").HasColumnType("datetime").IsRequired();
            Property(x => x.HistoryTrackingId).HasColumnName(@"HistoryTrackingId").HasColumnType("uniqueidentifier").IsRequired();

            // Foreign keys
            HasRequired(a => a.HistoryTrackingAudit).WithMany(b => b.HistoryTrackingValueAudits).HasForeignKey(c => c.HistoryTrackingId).WillCascadeOnDelete(false); // FK__HistoryTr__Histo__403A8C7D
        }
    }

}
// </auto-generated>


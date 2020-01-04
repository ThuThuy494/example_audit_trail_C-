using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebApp.Model.Entity
{
    // HistoryTrackingAudit
    public class HistoryTrackingAuditConfiguration : EntityTypeConfiguration<HistoryTrackingAudit>
    {
        public HistoryTrackingAuditConfiguration()
            : this("dbo")
        {
        }

        public HistoryTrackingAuditConfiguration(string schema)
        {
            ToTable("HistoryTrackingAudit", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("uniqueidentifier").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.RecordId).HasColumnName(@"RecordId").HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.RecordType).HasColumnName(@"RecordType").HasColumnType("nvarchar(max)").IsRequired();
            Property(x => x.ObjectName).HasColumnName(@"ObjectName").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.SubObjectName).HasColumnName(@"SubObjectName").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            Property(x => x.InsertedById).HasColumnName(@"InsertedById").HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.InsertedAt).HasColumnName(@"InsertedAt").HasColumnType("datetime").IsRequired();
            Property(x => x.UpdatedById).HasColumnName(@"UpdatedById").HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.UpdatedAt).HasColumnName(@"UpdatedAt").HasColumnType("datetime").IsRequired();
        }
    }

}
// </auto-generated>



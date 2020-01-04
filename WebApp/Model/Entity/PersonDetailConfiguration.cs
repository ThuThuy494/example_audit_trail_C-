using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model.Entity
{
    public class PersonDetailConfiguration : EntityTypeConfiguration<PersonDetail>
    {
        public PersonDetailConfiguration()
              : this("dbo")
        {
        }

        public PersonDetailConfiguration(string schema)
        {
            ToTable("PersonDetails", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("uniqueidentifier").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.PersonId).HasColumnName(@"PersonId").HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.RoleName).HasColumnName(@"RoleName").HasColumnType("nvarchar").IsRequired().HasMaxLength(250);
            Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.UpdateDate).HasColumnName(@"UpdateDate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            HasRequired(a => a.Person).WithMany(b => b.PersonDetails).HasForeignKey(c => c.PersonId).WillCascadeOnDelete(false); // FK__HistoryTr__Histo__403A8C7D
        }
    }
}

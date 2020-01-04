using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebApp.Model.Entity
{
    // Persons
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
            : this("dbo")
        {
        }

        public PersonConfiguration(string schema)
        {
            ToTable("Persons", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("uniqueidentifier").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            Property(x => x.FullName).HasColumnName(@"FullName").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.UpdateDate).HasColumnName(@"UpdateDate").HasColumnType("datetime").IsRequired();
        }
    }

}
// </auto-generated>



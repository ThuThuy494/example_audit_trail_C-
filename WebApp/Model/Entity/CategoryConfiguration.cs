using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace WebApp.Model.Entity
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration() : this("dbo")
        {
        }
        public CategoryConfiguration(string schema)
        {
            ToTable("Categories", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("uniqueidentifier").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName(@"FirstName").HasColumnType("nvarchar").IsRequired().HasMaxLength(255);
            Property(x => x.SubName).HasColumnName(@"LastName").HasColumnType("nvarchar").IsRequired().HasMaxLength(255);
            Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.UpdateDate).HasColumnName(@"UpdateDate").HasColumnType("datetime").IsRequired();
        }
    }
}
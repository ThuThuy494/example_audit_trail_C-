namespace AuditTrail_Console.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_col_table_person : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Persons", "FullName", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Persons", "FullName");
        }
    }
}

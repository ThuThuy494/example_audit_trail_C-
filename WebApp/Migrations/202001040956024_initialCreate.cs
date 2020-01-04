namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditEntries",
                c => new
                    {
                        AuditEntryID = c.Int(nullable: false, identity: true),
                        EntitySetName = c.String(maxLength: 255),
                        EntityTypeName = c.String(maxLength: 255),
                        State = c.Int(nullable: false),
                        StateName = c.String(maxLength: 255),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuditEntryID);
            
            CreateTable(
                "dbo.AuditEntryProperties",
                c => new
                    {
                        AuditEntryPropertyID = c.Int(nullable: false, identity: true),
                        AuditEntryID = c.Int(nullable: false),
                        RelationName = c.String(maxLength: 255),
                        PropertyName = c.String(maxLength: 255),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuditEntryPropertyID)
                .ForeignKey("dbo.AuditEntries", t => t.AuditEntryID, cascadeDelete: true)
                .Index(t => t.AuditEntryID);
            
            CreateTable(
                "dbo.HistoryTrackingAudit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RecordId = c.Guid(nullable: false),
                        RecordType = c.String(nullable: false),
                        ObjectName = c.String(),
                        SubObjectName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedById = c.Guid(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedById = c.Guid(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HistoryTrackingValueAudit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ColumnName = c.String(nullable: false),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        Action = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedById = c.Guid(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedById = c.Guid(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        HistoryTrackingId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryTrackingAudit", t => t.HistoryTrackingId)
                .Index(t => t.HistoryTrackingId);
            
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 150),
                        LastName = c.String(nullable: false, maxLength: 150),
                        FullName = c.String(nullable: false, maxLength: 150),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PersonId = c.Guid(nullable: false),
                        RoleName = c.String(nullable: false, maxLength: 250),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonDetails", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.HistoryTrackingValueAudit", "HistoryTrackingId", "dbo.HistoryTrackingAudit");
            DropForeignKey("dbo.AuditEntryProperties", "AuditEntryID", "dbo.AuditEntries");
            DropIndex("dbo.PersonDetails", new[] { "PersonId" });
            DropIndex("dbo.HistoryTrackingValueAudit", new[] { "HistoryTrackingId" });
            DropIndex("dbo.AuditEntryProperties", new[] { "AuditEntryID" });
            DropTable("dbo.PersonDetails");
            DropTable("dbo.Persons");
            DropTable("dbo.HistoryTrackingValueAudit");
            DropTable("dbo.HistoryTrackingAudit");
            DropTable("dbo.AuditEntryProperties");
            DropTable("dbo.AuditEntries");
        }
    }
}

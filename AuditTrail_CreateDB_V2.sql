CREATE DATABASE EntityFrameworkPlus
GO
USE EntityFrameworkPlus
Create Table Persons(
      [Id] [uniqueidentifier] NOT NULL Primary Key,
	  [FirstName] [nvarchar](150) NOT NULL,
	  [LastName] [nvarchar](150) NOT NULL,
	  [CreatedDate] [datetime] NOT NULL,
	  [UpdateDate] [datetime] NOT NULL
)
CREATE TABLE [dbo].[AuditEntries] (
    [AuditEntryID] [int] NOT NULL IDENTITY,
    [EntitySetName] [nvarchar](255),
    [EntityTypeName] [nvarchar](255),
    [State] [int] NOT NULL,
    [StateName] [nvarchar](255),
    [CreatedBy] [nvarchar](255),
    [CreatedDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL
    CONSTRAINT [PK_dbo.AuditEntries] PRIMARY KEY ([AuditEntryID])
)

GO

CREATE TABLE [dbo].[AuditEntryProperties] (
    [AuditEntryPropertyID] [int] NOT NULL IDENTITY,
    [AuditEntryID] [int] NOT NULL,
    [RelationName] [nvarchar](255),
    [PropertyName] [nvarchar](255),
    [OldValue] [nvarchar](max),
    [NewValue] [nvarchar](max),
	[CreatedDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL
    CONSTRAINT [PK_dbo.AuditEntryProperties] PRIMARY KEY ([AuditEntryPropertyID])
)

GO

CREATE INDEX [IX_AuditEntryID] ON [dbo].[AuditEntryProperties]([AuditEntryID])

GO

ALTER TABLE [dbo].[AuditEntryProperties] 
ADD CONSTRAINT [FK_dbo.AuditEntryProperties_dbo.AuditEntries_AuditEntryID] 
FOREIGN KEY ([AuditEntryID])
REFERENCES [dbo].[AuditEntries] ([AuditEntryID])
ON DELETE CASCADE

GO
CREATE TABLE [dbo].[HistoryTrackingAudit](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[RecordId] [uniqueidentifier] NOT NULL,
	[RecordType] [nvarchar](max) NOT NULL,
	[ObjectName] [nvarchar](max) NULL,
	[SubObjectName] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[InsertedById] [uniqueidentifier] NOT NULL,
	[InsertedAt] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL
)

CREATE TABLE [dbo].[HistoryTrackingValueAudit](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[ColumnName] [nvarchar](max) NOT NULL,
	[OldValue] [nvarchar](max) NULL,
	[NewValue] [nvarchar](max) NULL,
	[Action] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[InsertedById] [uniqueidentifier] NOT NULL,
	[InsertedAt] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	[HistoryTrackingId] [uniqueidentifier] NOT NULL
	FOREIGN KEY([HistoryTrackingId]) REFERENCES [HistoryTrackingAudit](Id)
)
GO
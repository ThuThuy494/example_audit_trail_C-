create database ExampleAuditTrail
Go 
use ExampleAuditTrail
GO
Create Table Persons(
      [Id] [uniqueidentifier] NOT NULL Primary Key,
	  [FirstName] [nvarchar](150) NOT NULL,
	  [LastName] [nvarchar](150) NOT NULL,

)

CREATE TABLE [dbo].[AuditEntryHistory] (
    [Id] [int] NOT NULL IDENTITY PRIMARY KEY,
    [EntitySetName] [nvarchar](255),
    [EntityTypeName] [nvarchar](255),
    [State] [int] NOT NULL,
    [StateName] [nvarchar](255),
    [CreatedBy] [nvarchar](255),
    [CreatedDate] [datetime] NOT NULL,
    --CONSTRAINT [PK_dbo.AuditEntries] PRIMARY KEY ([AuditEntryID])
)


Create TABLE [dbo].[AuditLog] (

       [Id] [uniqueidentifier] NOT NULL Primary Key,

       [AuditType] [char](1) NOT NULL,

       [TableName] [nvarchar](150) NOT NULL,

       [PK] [nvarchar](50) NOT NULL,

       [ColumnName] [nvarchar](100) NULL,

       [OldValue] [nvarchar](max) NULL,

       [NewValue] [nvarchar](max) NULL,

       [Date] [datetime] NOT NULL,

       [UserId] [uniqueidentifier] NOT NULL
	  FOREIGN KEY([UserId]) REFERENCES Persons(id)
)

CREATE TABLE [dbo].[AuditEntryPropertiesHistory] (
    [AuditEntryPropertyID] [int] NOT NULL IDENTITY PRIMARY KEY,
    [RelationName] [nvarchar](255),
    [PropertyName] [nvarchar](255),
    [OldValue] [nvarchar](max),
    [NewValue] [nvarchar](max),
    [AuditEntryID] [int] NOT NULL
    --CONSTRAINT [PK_dbo.AuditEntryProperties] PRIMARY KEY ([AuditEntryPropertyID])
	 FOREIGN KEY([AuditEntryID]) REFERENCES [AuditEntryHistory](Id)
)
--CREATE INDEX [IX_AuditEntryID] ON [dbo].[AuditEntryProperties]([AuditEntryID])

--GO

--ALTER TABLE [dbo].[AuditEntryProperties] 
--ADD CONSTRAINT [FK_dbo.AuditEntryProperties_dbo.AuditEntries_AuditEntryID] 
--FOREIGN KEY ([AuditEntryID])
--REFERENCES [dbo].[AuditEntries] ([AuditEntryID])
--ON DELETE CASCADE

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


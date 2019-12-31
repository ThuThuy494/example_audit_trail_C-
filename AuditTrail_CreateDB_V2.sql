CREATE DATABASE EntityFrameworkPlus
GO
USE EntityFrameworkPlus
CREATE TABLE [dbo].[AuditEntries] (
    [AuditEntryID] [int] NOT NULL IDENTITY,
    [EntitySetName] [nvarchar](255),
    [EntityTypeName] [nvarchar](255),
    [State] [int] NOT NULL,
    [StateName] [nvarchar](255),
    [CreatedBy] [nvarchar](255),
    [CreatedDate] [datetime] NOT NULL,
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
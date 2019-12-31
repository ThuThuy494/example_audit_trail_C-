create database ExampleAuditTrail
Go 
use ExampleAuditTrail
GO
Create Table Persons(
      [Id] [uniqueidentifier] NOT NULL Primary Key,
	  [FirstName] [nvarchar](150) NOT NULL,
	  [LastName] [nvarchar](150) NOT NULL,

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


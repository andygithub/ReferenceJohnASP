CREATE TABLE [dbo].[AuditLog]
(
	[AuditLogId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Action] NVARCHAR(50) NOT NULL, 
	[Type] NVARCHAR(50) NOT NULL, 
	[EntityKey] int not null,
	[ChangeSet] NVARCHAR(MAX) NOT NULL, 
	[CountofFieldsModified] int not null,
	[DateCreated] DATETIME NOT NULL DEFAULT GETDATE() , 
	[LastChangeUser] NVARCHAR(50) NOT NULL,
	[LastChangeDate] DATETIME NOT NULL
)

GO

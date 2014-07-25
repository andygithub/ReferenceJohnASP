CREATE TABLE [dbo].[Audit]
(
	[AuditId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[EntityName] NVARCHAR(50) NOT NULL, 
	[EntityKey] int not null,
	[ChangeSet] NVARCHAR(MAX) NOT NULL, 
	[DateCreated] DATETIME NOT NULL DEFAULT GETDATE() , 
	[LastChangeUser] NVARCHAR(50) NOT NULL
)

GO

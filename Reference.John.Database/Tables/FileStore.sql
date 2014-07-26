CREATE TABLE [dbo].[FileStore]
(
	[FileStoreId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[EntityName] NVARCHAR(50) NOT NULL, 
	[EntityKey] int not null,
	[File] VARBINARY(MAX) NOT NULL, 
	[ClientToken] UNIQUEIDENTIFIER NOT NULL DEFAULT newsequentialid(), 
	[DateCreated] DATETIME NOT NULL DEFAULT GETDATE() , 
	[LastChangeUser] NVARCHAR(50) NOT NULL,
	[LastChangeDate] DATETIME NOT NULL
)

GO

CREATE Unique INDEX [IX_FileStore_ClientToken] ON [dbo].[FileStore] ([ClientToken])
CREATE TABLE [dbo].[AlertTemplate]
(
	[AlertTemplateId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[AlertTypeId] int not null,
	[TemplateText] NVARCHAR(50) NOT NULL,
	[IsActive] int not null Default 1,
	[DateCreated] DATETIME NOT NULL DEFAULT GETDATE() , 
	[LastChangeUser] NVARCHAR(50) NOT NULL, 
	[LastChangeDate] DATETIME NOT NULL, 
	[ClientToken] UNIQUEIDENTIFIER NOT NULL DEFAULT newsequentialid(), 
	[RowVersion] ROWVERSION NOT NULL, 
	CONSTRAINT [FK_AlertTemplate_AlertTypeOptionList] FOREIGN KEY ([AlertTypeId]) REFERENCES [AlertTypeOptionList]([AlertTypeId]) ,
)
GO

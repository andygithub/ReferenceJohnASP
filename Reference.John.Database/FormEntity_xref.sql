CREATE TABLE [dbo].[FormEntity_xref]
(
	[FormId] INT NOT NULL , 
    [EntityId] INT NOT NULL,
	[RelationshipType] int not null,
	[RelationshipStatus] int not null,
	[RelationshipDescription] nvarchar(200),
		[IsActive] INT NOT NULL DEFAULT 1, 
	[StartDate] DATETIME NOT NULL, 
	[EndDate] DATETIME NULL, 
	[DateCreated] DATETIME NOT NULL DEFAULT GETDATE() , 
	[LastChangeUser] NVARCHAR(50) NOT NULL, 
	[LastChangeDate] DATETIME NOT NULL, 
	[ClientToken] UNIQUEIDENTIFIER NOT NULL DEFAULT newsequentialid(), 
	[RowVersion] ROWVERSION NULL, 
    PRIMARY KEY ([FormId], [EntityId]), 
    CONSTRAINT [FK_FormEntity_xref_FormSImpleZero] FOREIGN KEY ([FormId]) REFERENCES [FormSimpleZero]([Id]) ,
	CONSTRAINT [FK_FormEntity_xref_Entity] FOREIGN KEY ([EntityId]) REFERENCES [Entity]([EntityId]) ,
)

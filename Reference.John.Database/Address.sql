CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[FormSimpleZeroId] int not null,
	[AddressTypeId] int not null,
	[AddressLine1] NVARCHAR(50) NOT NULL,
	[AddressLine2] NVARCHAR(50) NULL,
	[AddressLine3] NVARCHAR(50) NULL,
	[City] NVARCHAR(50) NULL,
	[State] NVARCHAR(50) NULL,
	[Zip] NVARCHAR(50) NULL,		
	[DateCreated] DATETIME NOT NULL DEFAULT GETDATE() , 
	[LastChangeUser] NVARCHAR(50) NOT NULL, 
	[LastChangeDate] DATETIME NOT NULL, 
	[ClientToken] UNIQUEIDENTIFIER NOT NULL DEFAULT newsequentialid(), 
	CONSTRAINT [FK_Address_AddressTypeOptionList] FOREIGN KEY ([AddressTypeId]) REFERENCES [AddressTypeOptionList]([Id]) ,
	CONSTRAINT [FK_Address_FormSimpleZeroId] FOREIGN KEY ([FormSimpleZeroId]) REFERENCES [FormSimpleZero]([Id]) 
)
GO

CREATE Unique INDEX [IX_Address_ClientToken] ON [dbo].[Address] ([ClientToken])
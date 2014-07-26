CREATE TABLE [dbo].[ApplicationExtendedProperties]
(
	ApplicationId nvarchar(50) NOT NULL ,
	KeyId nvarchar(50) not null,
	Value nvarchar(max) not null,
	DateCreated datetime not null,
	DateExpired datetime not null, 
	[LastChangeDate] DATETIME NOT NULL, 
	PRIMARY KEY ([ApplicationId], [KeyId])
)

GO

CREATE INDEX [IX_UserExtendedProperties_DateExpired] ON [dbo].[ApplicationExtendedProperties] ([DateExpired])

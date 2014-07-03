CREATE TABLE [dbo].[UserExtendedProperties]
(
	UserId nvarchar(50) NOT NULL ,
	KeyId nvarchar(50) not null,
	Value nvarchar(max) not null,
	DateCreated datetime not null,
	DateExpired datetime not null, 
	PRIMARY KEY ([UserId], [KeyId])
)

GO

CREATE INDEX [IX_UserExtendedProperties_DateExpired] ON [dbo].[UserExtendedProperties] ([DateExpired])

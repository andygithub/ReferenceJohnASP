CREATE PROCEDURE [dbo].[InsertAuditLog]
	@Action nvarchar(50),
	@Type nvarchar(50),
	@EntityKey int,
	@ChangeSet nvarchar(max),
	@CountofFieldsModified int,
	@LastChangeUser nvarchar(50)
AS
insert into dbo.AuditLog ([Action],[Type] , EntityKey,ChangeSet, CountofFieldsModified, LastChangeUser,DateCreated,LastChangeDate)
values (@Action,@Type, @EntityKey,@ChangeSet, @CountofFieldsModified, @LastChangeUser,GETDATE(),GETDATE())


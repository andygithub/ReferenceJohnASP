CREATE PROCEDURE [dbo].[InsertAudit]
	@EntityName nvarchar(50) = 0,
	@EntityKey int,
	@ChangeSet nvarchar(max),
	@LastChangeUser nvarchar(50)
AS
insert into dbo.Audit (EntityName, EntityKey,ChangeSet, LastChangeUser)
values (@EntityName, @EntityKey,@ChangeSet, @LastChangeUser)


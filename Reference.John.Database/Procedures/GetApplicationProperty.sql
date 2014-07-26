CREATE PROCEDURE [dbo].[GetApplicationProperty]
	@ApplicationId nvarchar(50),
	@KeyId nvarchar(50)
AS
	SELECT Value from [dbo].[ApplicationExtendedProperties] where ApplicationId=@ApplicationId and KeyId=@KeyId


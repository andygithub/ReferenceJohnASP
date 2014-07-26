/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
			   SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[EthnicityOptionList] ON

Merge into [dbo].[EthnicityOptionList] As Target
Using (values
	(1, N'Hispanic', 1, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime)),
	(2, N'Othe', 2, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
)
As Source ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate])
On Target.EthnicityId = Source.Id
--update matched rows
when matched then	
	update set name = source.name,[SortOrder]=source.[SortOrder], [IsActive]=source.[IsActive], [StartDate]=source.[StartDate], [EndDate]=source.[EndDate], [LastChangeUser]=source.[LastChangeUser], [LastChangeDate]=source.[LastChangeDate]
--insert if no match
when not matched then
	INSERT ([EthnicityId], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate])
	VALUES (source.[Id], source.[Name], source.[SortOrder], source.[IsActive], source.[StartDate], source.[EndDate], source.[LastChangeUser], source.[LastChangeDate])
	;

SET IDENTITY_INSERT [dbo].[EthnicityOptionList] Off

---
SET IDENTITY_INSERT [dbo].[GenderOptionList] ON

Merge into [dbo].[GenderOptionList] As Target
Using (values
	(1, N'Male', 1, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime)),
	(2, N'Female', 2, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
)
As Source ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate])
On Target.GenderId = Source.Id
--update matched rows
when matched then	
	update set name = source.name,[SortOrder]=source.[SortOrder], [IsActive]=source.[IsActive], [StartDate]=source.[StartDate], [EndDate]=source.[EndDate], [LastChangeUser]=source.[LastChangeUser], [LastChangeDate]=source.[LastChangeDate]
--insert if no match
when not matched then
	INSERT ([GenderId], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate])
	VALUES (source.[Id], source.[Name], source.[SortOrder], source.[IsActive], source.[StartDate], source.[EndDate], source.[LastChangeUser], source.[LastChangeDate])
	;

SET IDENTITY_INSERT [dbo].[GenderOptionList] Off

---
SET IDENTITY_INSERT [dbo].[RaceOptionList] ON

Merge into [dbo].[RaceOptionList] As Target
Using (values
	(1, N'White', 0, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime)),
	(2, N'Black', 1, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime)),
	(3, N'Purple', 2, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
)
As Source ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate])
On Target.RaceId = Source.Id
--update matched rows
when matched then	
	update set name = source.name,[SortOrder]=source.[SortOrder], [IsActive]=source.[IsActive], [StartDate]=source.[StartDate], [EndDate]=source.[EndDate], [LastChangeUser]=source.[LastChangeUser], [LastChangeDate]=source.[LastChangeDate]
--insert if no match
when not matched then
	INSERT ([RaceId], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate])
	VALUES (source.[Id], source.[Name], source.[SortOrder], source.[IsActive], source.[StartDate], source.[EndDate], source.[LastChangeUser], source.[LastChangeDate])
	;

SET IDENTITY_INSERT [dbo].[RaceOptionList] Off

---
SET IDENTITY_INSERT [dbo].[RegionOptionList] ON

Merge into [dbo].[RegionOptionList] As Target
Using (values
	(1, N'East', 1, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime)),
	(2, N'West', 2, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime)),
	(3, N'North', 4, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime)),
	(4, N'South', 3, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
)
As Source ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate])
On Target.RegionId = Source.Id
--update matched rows
when matched then	
	update set name = source.name,[SortOrder]=source.[SortOrder], [IsActive]=source.[IsActive], [StartDate]=source.[StartDate], [EndDate]=source.[EndDate], [LastChangeUser]=source.[LastChangeUser], [LastChangeDate]=source.[LastChangeDate]
--insert if no match
when not matched then
	INSERT ([RegionId], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate])
	VALUES (source.[Id], source.[Name], source.[SortOrder], source.[IsActive], source.[StartDate], source.[EndDate], source.[LastChangeUser], source.[LastChangeDate])
	;

SET IDENTITY_INSERT [dbo].[RegionOptionList] Off

---
SET IDENTITY_INSERT [dbo].[AddressTypeOptionList] ON

Merge into [dbo].[AddressTypeOptionList] As Target
Using (values
	(1, N'Mail', 1, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'none', CAST(0x0000A2A600000000 AS DateTime)),
	(2, N'Residential', 2, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'none', CAST(0x0000A2A600000000 AS DateTime))
)
As Source ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate])
On Target.AddressTypeId = Source.Id
--update matched rows
when matched then	
	update set name = source.name,[SortOrder]=source.[SortOrder], [IsActive]=source.[IsActive], [StartDate]=source.[StartDate], [EndDate]=source.[EndDate], [LastChangeUser]=source.[LastChangeUser], [LastChangeDate]=source.[LastChangeDate]
--insert if no match
when not matched then
	INSERT ([AddressTypeId], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate])
	VALUES (source.[Id], source.[Name], source.[SortOrder], source.[IsActive], source.[StartDate], source.[EndDate], source.[LastChangeUser], source.[LastChangeDate])
	;

SET IDENTITY_INSERT [dbo].[AddressTypeOptionList] Off
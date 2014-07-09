CREATE VIEW [dbo].[SearchResults]
	AS SELECT 
	f.[Id] as [FormId], [FirstName], [LastName], [GenderId], [RaceId], [RegionId], [EthnicityId], f.[DateCreated], f.[LastChangeUser], f.[LastChangeDate],
	e.Name as EthnicityName, g.Name as GenderName,f.ClientToken as [ClientToken]
	FROM [dbo].[FormSimpleZero] f
		inner join [dbo].[EthnicityOptionList] e on f.EthnicityId=e.Id
		inner join [dbo].[GenderOptionList] g on f.GenderId=g.Id

--This script is for setting up data necessary for the unit tests to work
--The option lists should have already been deployed with the database project and the merge script.

USE [Reference_John]
GO
SET IDENTITY_INSERT [dbo].[EthnicityOptionList] ON 

GO
INSERT [dbo].[EthnicityOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (1, N'Hispanic', 1, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
GO
INSERT [dbo].[EthnicityOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (2, N'Other', 2, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[EthnicityOptionList] OFF
GO
SET IDENTITY_INSERT [dbo].[GenderOptionList] ON 

GO
INSERT [dbo].[GenderOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (1, N'Male', 1, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
GO
INSERT [dbo].[GenderOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (2, N'Female', 2, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[GenderOptionList] OFF
GO
SET IDENTITY_INSERT [dbo].[RaceOptionList] ON 

GO
INSERT [dbo].[RaceOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (1, N'White', 0, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
GO
INSERT [dbo].[RaceOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (2, N'Black', 1, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
GO
INSERT [dbo].[RaceOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (3, N'Purple', 2, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[RaceOptionList] OFF
GO
SET IDENTITY_INSERT [dbo].[RegionOptionList] ON 

GO
INSERT [dbo].[RegionOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (1, N'East', 1, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
GO
INSERT [dbo].[RegionOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (2, N'West', 2, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
GO
INSERT [dbo].[RegionOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (3, N'North', 4, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
GO
INSERT [dbo].[RegionOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (4, N'South', 3, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'zero', CAST(0x0000A2A600000000 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[RegionOptionList] OFF
GO
SET IDENTITY_INSERT [dbo].[FormSimpleZero] ON 

GO
INSERT [dbo].[FormSimpleZero] ([Id], [FirstName], [LastName], [GenderId], [RaceId], [RegionId], [EthnicityId], [DateCreated], [LastChangeUser], [LastChangeDate]) VALUES (1, N'Robject', N'Jones', 1, 2, 1, 1, CAST(0x0000A2A600000000 AS DateTime), N'script', CAST(0x0000A35B00000000 AS DateTime))
GO
INSERT [dbo].[FormSimpleZero] ([Id], [FirstName], [LastName], [GenderId], [RaceId], [RegionId], [EthnicityId], [DateCreated], [LastChangeUser], [LastChangeDate]) VALUES (2, N'Rboert', N'Jones', 2, 1, 2, 1, CAST(0x0000A2A600000000 AS DateTime), N'scripted', CAST(0x0000A2A600000000 AS DateTime))
GO
INSERT [dbo].[FormSimpleZero] ([Id], [FirstName], [LastName], [GenderId], [RaceId], [RegionId], [EthnicityId], [DateCreated], [LastChangeUser], [LastChangeDate]) VALUES (3, N'Other', N'Name', 1, 1, 1, 1, CAST(0x0000A2A600000000 AS DateTime), N'scripteunit', CAST(0x0000A2A600000000 AS DateTime))
GO
INSERT [dbo].[FormSimpleZero] ([Id], [FirstName], [LastName], [GenderId], [RaceId], [RegionId], [EthnicityId], [DateCreated], [LastChangeUser], [LastChangeDate]) VALUES (4, N'Robert', N'Unit', 2, 3, 1, 2, CAST(0x0000A2A600000000 AS DateTime), N'uir', CAST(0x0000A2A600000000 AS DateTime))
GO
INSERT [dbo].[FormSimpleZero] ([Id], [FirstName], [LastName], [GenderId], [RaceId], [RegionId], [EthnicityId], [DateCreated], [LastChangeUser], [LastChangeDate]) VALUES (5, N'Eric', N'Lang', 2, 3, 1, 2, CAST(0x0000A2A600000000 AS DateTime), N'uir', CAST(0x0000A2A600000000 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[FormSimpleZero] OFF
GO
SET IDENTITY_INSERT [dbo].[AddressTypeOptionList] ON 

GO
INSERT [dbo].[AddressTypeOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (1, N'Mail', 1, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'none', CAST(0x0000A2A600000000 AS DateTime))
GO
INSERT [dbo].[AddressTypeOptionList] ([Id], [Name], [SortOrder], [IsActive], [StartDate], [EndDate], [LastChangeUser], [LastChangeDate]) VALUES (2, N'Residential', 2, 1, CAST(0x0000A2A600000000 AS DateTime), NULL, N'none', CAST(0x0000A2A600000000 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[AddressTypeOptionList] OFF
GO
SET IDENTITY_INSERT [dbo].[Address] ON 

GO
INSERT [dbo].[Address] ([Id], [FormSimpleZeroId], [AddressTypeId], [AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [Zip], [DateCreated], [LastChangeUser], [LastChangeDate]) VALUES (1, 5, 2, N'300 corp circle', NULL, NULL, N'camp hill', N'pa', N'17011', CAST(0x0000A2A600000000 AS DateTime), N'unit', CAST(0x0000A2A600000000 AS DateTime))
GO
INSERT [dbo].[Address] ([Id], [FormSimpleZeroId], [AddressTypeId], [AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [Zip], [DateCreated], [LastChangeUser], [LastChangeDate]) VALUES (2, 4, 2, N'301 corp circle', NULL, NULL, N'camp hill', N'pa', N'17011', CAST(0x0000A2A600000000 AS DateTime), N'unit', CAST(0x0000A2A600000000 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Address] OFF
GO

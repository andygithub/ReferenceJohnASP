CREATE TABLE [dbo].[EthnicityOptionList]
(
	[EthnicityId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NVARCHAR(50) NOT NULL, 
	[SortOrder] INT NOT NULL DEFAULT 1, 
	[IsActive] INT NOT NULL DEFAULT 1, 
	[StartDate] DATETIME NOT NULL, 
	[EndDate] DATETIME NULL, 
	[DateCreated] DATETIME NOT NULL DEFAULT GETDATE() , 
	[LastChangeUser] NVARCHAR(50) NOT NULL, 
	[LastChangeDate] DATETIME NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'Primary Key for table',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'EthnicityOptionList',
	@level2type = N'COLUMN',
	@level2name = N'EthnicityId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'Text value of the option list record.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'EthnicityOptionList',
	@level2type = N'COLUMN',
	@level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'Determines the sort order of the option list',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'EthnicityOptionList',
	@level2type = N'COLUMN',
	@level2name = N'SortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'Flag to determine if the record is active.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'EthnicityOptionList',
	@level2type = N'COLUMN',
	@level2name = N'IsActive'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'Date when the record becomes active.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'EthnicityOptionList',
	@level2type = N'COLUMN',
	@level2name = N'StartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'End date when for the active record.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'EthnicityOptionList',
	@level2type = N'COLUMN',
	@level2name = N'EndDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'Last user to modify the record.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'EthnicityOptionList',
	@level2type = N'COLUMN',
	@level2name = N'LastChangeUser'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'Last change date for the record.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'EthnicityOptionList',
	@level2type = N'COLUMN',
	@level2name = N'LastChangeDate'
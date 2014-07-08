CREATE TABLE [dbo].[FormSimpleZero]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[FirstName] NVARCHAR(50) NOT NULL, 
	[LastName] NVARCHAR(50) NOT NULL, 
	[GenderId] int NOT null,
	[RaceId] int null,
	[RegionId] int null,
	[EthnicityId] int null,
	[DateCreated] DATETIME NOT NULL DEFAULT GETDATE() , 
	[LastChangeUser] NVARCHAR(50) NOT NULL, 
	[LastChangeDate] DATETIME NOT NULL, 
    CONSTRAINT [FK_FormSimpleZero_GenderOptionList] FOREIGN KEY ([GenderId]) REFERENCES [GenderOptionList]([Id]) ,
	CONSTRAINT [FK_FormSimpleZero_RaceOptionList] FOREIGN KEY ([RaceId]) REFERENCES [RaceOptionList]([Id]) ,
	CONSTRAINT [FK_FormSimpleZero_RegionOptionList] FOREIGN KEY ([RegionId]) REFERENCES [RegionOptionList]([Id]) ,
	CONSTRAINT [FK_FormSimpleZero_EthnicityOptionList] FOREIGN KEY ([EthnicityId]) REFERENCES [EthnicityOptionList]([Id]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'Primary Key for table',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'FormSimpleZero',
	@level2type = N'COLUMN',
	@level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'First name of the consumer for the form.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'FormSimpleZero',
	@level2type = N'COLUMN',
	@level2name = 'FirstName'
GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'Date when the consumer form record was created.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'FormSimpleZero',
	@level2type = N'COLUMN',
	@level2name = 'DateCreated'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'Last user to modify the record.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'FormSimpleZero',
	@level2type = N'COLUMN',
	@level2name = N'LastChangeUser'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
	@value = N'Last change date for the record.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'FormSimpleZero',
	@level2type = N'COLUMN',
	@level2name = N'LastChangeDate'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Last name of the consumer for the form.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FormSimpleZero',
    @level2type = N'COLUMN',
    @level2name = N'LastName'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Race identifier for the consumer.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FormSimpleZero',
    @level2type = N'COLUMN',
    @level2name = N'RaceId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Region identifier for the consumer.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FormSimpleZero',
    @level2type = N'COLUMN',
    @level2name = N'RegionId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Ethnicity identifier for the consumer.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FormSimpleZero',
    @level2type = N'COLUMN',
    @level2name = N'EthnicityId'
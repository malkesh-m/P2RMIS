CREATE TABLE [dbo].[Airport]
(
	[AirportId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Code] VARCHAR(50) NOT NULL,
	[Description] VARCHAR(100) NOT NULL,
    [CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an airport',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Airport',
    @level2type = N'COLUMN',
    @level2name = N'AirportId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Airport code',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Airport',
    @level2type = N'COLUMN',
    @level2name = N'Code'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Airport description',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Airport',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO
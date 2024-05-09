CREATE TABLE [dbo].[Carrier]
(
	[CarrierId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL,
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
    @value = N'Identifier for an carrier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Carrier',
    @level2type = N'COLUMN',
    @level2name = N'CarrierId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Carrier name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Carrier',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
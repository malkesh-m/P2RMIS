CREATE TABLE [dbo].[Hotel]
(
	[HotelId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [HotelName] VARCHAR(100) NOT NULL, 
	[HotelAbbreviation] VARCHAR(25) NULL,
    [Address] VARCHAR(50) NULL, 
    [City] VARCHAR(20) NULL, 
    [StateId] INT NULL, 
    [CountryId] INT NULL, 
    [ZipCode] VARCHAR(12) NULL,
	[LegacyHotelId] INT NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a hotel entry',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Hotel',
    @level2type = N'COLUMN',
    @level2name = N'HotelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Hotel name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Hotel',
    @level2type = N'COLUMN',
    @level2name = N'HotelName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Shortened hotel name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Hotel',
    @level2type = N'COLUMN',
    @level2name = N'HotelAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Street address of hotel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Hotel',
    @level2type = N'COLUMN',
    @level2name = N'Address'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'City address of hotel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Hotel',
    @level2type = N'COLUMN',
    @level2name = N'City'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'State identifier for hotel address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Hotel',
    @level2type = N'COLUMN',
    @level2name = N'StateId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Country identifier for hotel address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Hotel',
    @level2type = N'COLUMN',
    @level2name = N'CountryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Zip code for hotel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Hotel',
    @level2type = N'COLUMN',
    @level2name = N'ZipCode'
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[Hotel] TO [web-p2rmis]
    AS [dbo];
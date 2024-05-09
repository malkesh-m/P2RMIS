CREATE TABLE [dbo].[TravelMode]
(
	[TravelModeId] INT NOT NULL PRIMARY KEY, 
    [TravelModeAbbreviation] VARCHAR(20) NOT NULL, 
    [SortOrder] INT NOT NULL, 
    [LegacyTravelModeAbbreviation] VARCHAR(20) NULL
)

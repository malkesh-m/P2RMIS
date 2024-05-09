CREATE TABLE [dbo].[ClientExpertiseRating]
(
	[ClientExpertiseRatingId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [RatingAbbreviation] VARCHAR(8) NOT NULL, 
    [RatingName] VARCHAR(25) NOT NULL, 
	[RatingDescription] VARCHAR(8000) NULL,
    [ConflictFlag] BIT NOT NULL, 
    [SortOrder] INT NULL, 
    CONSTRAINT [FK_ClientExpertiseRating_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Text explaining the meaning of the rating',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientExpertiseRating',
    @level2type = N'COLUMN',
    @level2name = N'RatingDescription'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the rating is considered one that merits conflict',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientExpertiseRating',
    @level2type = N'COLUMN',
    @level2name = N'ConflictFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name describing the rating',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientExpertiseRating',
    @level2type = N'COLUMN',
    @level2name = N'RatingName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Abbreviated form of the rating',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientExpertiseRating',
    @level2type = N'COLUMN',
    @level2name = N'RatingAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientExpertiseRating',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client expertise rating',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientExpertiseRating',
    @level2type = N'COLUMN',
    @level2name = N'ClientExpertiseRatingId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Available expertise ratings of a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientExpertiseRating',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT
    ON OBJECT::[dbo].[ClientExpertiseRating] TO [web-p2rmis]
    AS [dbo];
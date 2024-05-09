CREATE TABLE [dbo].[MeetingType]
(
	[MeetingTypeId] INT NOT NULL PRIMARY KEY, 
    [LegacyMeetingTypeId] VARCHAR(3) NULL, 
    [MeetingTypeAbbreviation] VARCHAR(5) NOT NULL, 
    [MeetingTypeName] VARCHAR(20) NOT NULL, 
    [SortOrder] INT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a type of meeting',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingType',
    @level2type = N'COLUMN',
    @level2name = N'MeetingTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name indicating the nature of the meeting',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingType',
    @level2type = N'COLUMN',
    @level2name = N'MeetingTypeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Abbreviation for the meeting type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingType',
    @level2type = N'COLUMN',
    @level2name = N'MeetingTypeAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for a review type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingType',
    @level2type = N'COLUMN',
    @level2name = N'LegacyMeetingTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Possible system review meeting types',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingType',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT
    ON OBJECT::[dbo].[MeetingType] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The order in which the MeetingTypes are presented',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
CREATE TABLE [dbo].[ParticipantInfoTracking]
(
	[ParticipantInfoTrackingId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PrgPartId] INT NOT NULL, 
    [DocumentName] VARCHAR(50) NOT NULL, 
    [DocumentText] VARBINARY(MAX) NULL,
    [ModifiedBy] INT NULL,	
    [ModifiedDate] DATETIME2(0) NULL, 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Participant information tracking ID for legacy COI/NDA data',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ParticipantInfoTracking',
    @level2type = N'COLUMN',
    @level2name = N'ParticipantInfoTrackingId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Participant ID from 1.0',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ParticipantInfoTracking',
    @level2type = N'COLUMN',
    @level2name = N'PrgPartId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ParticipantInfoTracking',
    @level2type = N'COLUMN',
    @level2name = N'DocumentName'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Binary representation of a document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ParticipantInfoTracking',
    @level2type = N'COLUMN',
    @level2name = N'DocumentText'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'When the document was last updated',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ParticipantInfoTracking',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'UserId for who last updated the document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ParticipantInfoTracking',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedBy'
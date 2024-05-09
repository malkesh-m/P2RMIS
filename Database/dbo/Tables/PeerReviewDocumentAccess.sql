CREATE TABLE [dbo].[PeerReviewDocumentAccess]
(
	[PeerReviewDocumentAccessId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PeerReviewDocumentId] INT NOT NULL, 
    [MeetingTypeIds] VARCHAR(1000) NULL, 
    [ClientParticipantTypeIds] VARCHAR(1000) NULL,
	[ParticipationMethodIds] VARCHAR(1000) NULL,
	[RestrictedAssignedFlag] BIT NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PeerReviewDocumentAccess_PeerReviewDocument] FOREIGN KEY ([PeerReviewDocumentId]) REFERENCES [PeerReviewDocument]([PeerReviewDocumentId]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a peer review document access combination',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocumentAccess',
    @level2type = N'COLUMN',
    @level2name = N'PeerReviewDocumentAccessId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a peer review document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocumentAccess',
    @level2type = N'COLUMN',
    @level2name = N'PeerReviewDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Comma delimited list of allowed meeting type ids NULL serves as ALL',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocumentAccess',
    @level2type = N'COLUMN',
    @level2name = 'MeetingTypeIds'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Comma delimited list of allowed client participant type ids NULL serves as ALL',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocumentAccess',
    @level2type = N'COLUMN',
    @level2name = 'ClientParticipantTypeIds'
GO

CREATE UNIQUE INDEX [UIX_PeerReviewDocumentAccess_PeerReviewDocumentId] ON [dbo].[PeerReviewDocumentAccess] ([PeerReviewDocumentId]) WHERE [DeletedFlag] = 0

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Comma delimited list of allowed participation method ids NULL serves as ALL',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocumentAccess',
    @level2type = N'COLUMN',
    @level2name = N'ParticipationMethodIds'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the document applies to restricted or un-restricted participants NULL allows both',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocumentAccess',
    @level2type = N'COLUMN',
    @level2name = N'RestrictedAssignedFlag'
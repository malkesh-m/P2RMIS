CREATE TABLE [dbo].[UserPeerReviewDocument]
(
	[UserPeerReviewDocumentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PeerReviewDocumentId] INT NOT NULL, 
    [ProgramYearId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [ReviewDate] DATETIME2(0) NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] DATETIME2 NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] DATETIME2 NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] DATETIME2 NULL, 
    CONSTRAINT [FK_UserPeerReviewDocument_PeerReviewDocument] FOREIGN KEY ([PeerReviewDocumentId]) REFERENCES [PeerReviewDocument]([PeerReviewDocumentId]), 
    CONSTRAINT [FK_UserPeerReviewDocument_ProgramYear] FOREIGN KEY ([ProgramYearId]) REFERENCES [ProgramYear]([ProgramYearId]), 
    CONSTRAINT [FK_UserPeerReviewDocument_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserID]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date the document was reviewed.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'ReviewDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'UserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program year',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'ProgramYearId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a peer review document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'PeerReviewDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s review of a peer review document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserPeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'UserPeerReviewDocumentId'
GO

CREATE UNIQUE INDEX [UIX_UserPeerReviewDocument_ProgramYearId_UserId_PeerReviewDocumentId] ON [dbo].[UserPeerReviewDocument] ([PeerReviewDocumentId],[UserId],[ProgramYearId]) WHERE [DeletedFlag] = 0

CREATE TABLE [dbo].[SummaryReviewerDescription]
(
	[SummaryReviewerDescriptionId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProgramMechanismId] INT NULL, 
    [ClientParticipantTypeId] INT NULL, 
    [ClientRoleId] INT NULL, 
    [AssignmentOrder] INT NULL, 
    [CustomOrder] INT NOT NULL, 
    [DisplayName] VARCHAR(100) NOT NULL, 
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_SummaryReviewerDescription_ProgramMechanism] FOREIGN KEY ([ProgramMechanismId]) REFERENCES [ProgramMechanism]([ProgramMechanismId]), 
    CONSTRAINT [FK_SummaryReviewerDescription_ClientParticipantType] FOREIGN KEY ([ClientParticipantTypeId]) REFERENCES [ClientParticipantType]([ClientParticipantTypeId]), 
    CONSTRAINT [FK_SummaryReviewerDescription_ClientRole] FOREIGN KEY ([ClientRoleId]) REFERENCES [ClientRole]([ClientRoleId])
)

GO

CREATE INDEX [IX_SummaryReviewerDescription_ProgramMechanismId] ON [dbo].[SummaryReviewerDescription] ([ProgramMechanismId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a custom reviewer description in summary statements',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryReviewerDescription',
    @level2type = N'COLUMN',
    @level2name = N'SummaryReviewerDescriptionId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Program Mechanism identifier. Null = wildcard',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryReviewerDescription',
    @level2type = N'COLUMN',
    @level2name = N'ProgramMechanismId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Reviewers participant type identifier. Null = wildcard',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryReviewerDescription',
    @level2type = N'COLUMN',
    @level2name = N'ClientParticipantTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Reviewers role identifier. Null = wildcard',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryReviewerDescription',
    @level2type = N'COLUMN',
    @level2name = N'ClientRoleId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Reviewers original sort order. Null = wildcard',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryReviewerDescription',
    @level2type = N'COLUMN',
    @level2name = N'AssignmentOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Sort order for the reviewers critiques in a summary statement',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryReviewerDescription',
    @level2type = N'COLUMN',
    @level2name = N'CustomOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label for the reviewers critiques in a summary statement',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryReviewerDescription',
    @level2type = N'COLUMN',
    @level2name = N'DisplayName'
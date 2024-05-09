CREATE TABLE [dbo].[ProgramMechanism]
(
	[ProgramMechanismId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProgramYearId] INT NOT NULL, 
    [ClientAwardTypeId] INT NOT NULL, 
	[ReceiptCycle] INT NULL,
    [LegacyAtmId] INT NULL DEFAULT NEXT VALUE FOR seq_ProgramMechanism_LegacyAtmId, 
    [ReceiptDeadline] datetime2(0) NULL, 
	[AbstractFormat] VARCHAR(20),
	[BlindedFlag] BIT NOT NULL DEFAULT 0,
	[FundingOpportunityId] VARCHAR(50),
	[ParentProgramMechanismId] INT NULL,
	[MechanismRelationshipTypeId] INT NULL,
	[PartneringPiAllowedFlag] BIT NOT NULL DEFAULT 0,
	[SummarySetupLastUpdatedBy] INT NULL,
	[SummarySetupLastUpdatedDate] DATETIME2(0) NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ProgramMechanism_ClientAwardType] FOREIGN KEY ([ClientAwardTypeId]) REFERENCES [ClientAwardType]([ClientAwardTypeId]), 
    CONSTRAINT [FK_ProgramMechanism_ProgramYear] FOREIGN KEY ([ProgramYearId]) REFERENCES [ProgramYear]([ProgramYearId]), 
    CONSTRAINT [UN_ProgramMechanism_ProgramYearId_ClientAwardTypeId_ReceiptCycle] UNIQUE ([ProgramYearId],[ClientAwardTypeId],[ReceiptCycle], [DeletedDate]), 
    CONSTRAINT [FK_ProgramMechanism_ParentProgramMechanism] FOREIGN KEY ([ProgramMechanismId]) REFERENCES [ProgramMechanism]([ProgramMechanismId]), 
    CONSTRAINT [FK_ProgramMechanism_SummarySetupLastUpdatedBy] FOREIGN KEY ([SummarySetupLastUpdatedBy]) REFERENCES [User]([UserId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for an award instance',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'LegacyAtmId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'If applicable, the deadline for application receipt for the mechanism',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'ReceiptDeadline'
GO

CREATE INDEX [IX_ProgramMechanism_ProgramYearId_ReceiptCycle] ON [dbo].[ProgramMechanism] ([ProgramYearId], [ReceiptCycle])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an instance of a mechanism within a program year',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'ProgramMechanismId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an instance of a program for a given year',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'ProgramYearId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an award type offered by a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'ClientAwardTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Mechanism grouping based on when a mechanism was released to applicants ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'ReceiptCycle'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Award Mechanism offering within a review program/year',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Specifies whether the abstract is a file or text data',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'AbstractFormat'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ProgramMechanism] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether mechanism data is to be blinded to the reviewers',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'BlindedFlag'
GO

CREATE INDEX [IX_ProgramMechanism_ParentProgramMechanismId_MechanismRelationshipTypeId] ON [dbo].[ProgramMechanism] ([ParentProgramMechanismId],[MechanismRelationshipTypeId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a mechanism''s funding opportunity ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'FundingOpportunityId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for this mechanism''s hierarchial parent',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'ParentProgramMechanismId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the type of a mechanism relationship',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'MechanismRelationshipTypeId'
GO

CREATE NONCLUSTERED INDEX [IX_ProgramMechanism_DeletedFlag_ProgramMechanismId] ON [dbo].[ProgramMechanism]
(
	[DeletedFlag] ASC,
	[ProgramMechanismId] ASC,
	[ClientAwardTypeId] ASC,
	[ProgramYearId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_ProgramMechanism_ProgramYearId_DeletedFlag] ON [dbo].[ProgramMechanism]
(
	[ProgramYearId] ASC,
	[DeletedFlag] ASC,
	[ClientAwardTypeId] ASC,
	[ProgramMechanismId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether partner PI is allowed',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'PartneringPiAllowedFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User who last updated an SS setup for this mechanism',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'SummarySetupLastUpdatedBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date that an SS setup for this mechanism was last updated',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanism',
    @level2type = N'COLUMN',
    @level2name = N'SummarySetupLastUpdatedDate'
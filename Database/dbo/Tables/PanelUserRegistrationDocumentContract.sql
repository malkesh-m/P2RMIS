CREATE TABLE [dbo].[PanelUserRegistrationDocumentContract]
(
	[PanelUserRegistrationDocumentContractId] INT NOT NULL PRIMARY KEY IDENTITY,
	[PanelUserRegistrationDocumentId] INT NOT NULL,
	[ContractStatusId] INT NOT NULL DEFAULT 1,
	[FeeAmount] money NULL,
	[ContractFileLocation] VARCHAR(100) NULL,
	[BypassReason] varchar(500) NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PanelUserRegistrationDocumentContract_PanelUserRegistrationDocument] FOREIGN KEY ([PanelUserRegistrationDocumentId]) REFERENCES [PanelUserRegistrationDocument]([PanelUserRegistrationDocumentId]), 
    CONSTRAINT [FK_PanelUserRegistrationDocumentContract_ContractStatus] FOREIGN KEY ([ContractStatusId]) REFERENCES [ContractStatus]([ContractStatusId]), 
)

GO

CREATE UNIQUE INDEX [IX_PanelUserRegistrationDocumentContract_PanelUserRegistrationDocumentId] ON [dbo].[PanelUserRegistrationDocumentContract] ([PanelUserRegistrationDocumentId]) INCLUDE (ContractStatusId, FeeAmount) WHERE (DeletedFlag = 0)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a contractual registration document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocumentContract',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserRegistrationDocumentContractId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel user''s registration document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocumentContract',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserRegistrationDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a contract status type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocumentContract',
    @level2type = N'COLUMN',
    @level2name = N'ContractStatusId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Fee amount specified by contractual terms',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocumentContract',
    @level2type = N'COLUMN',
    @level2name = N'FeeAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Relative path to pdf ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocumentContract',
    @level2type = N'COLUMN',
    @level2name = 'ContractFileLocation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Reason for which the contract was bypassed',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelUserRegistrationDocumentContract',
    @level2type = N'COLUMN',
    @level2name = N'BypassReason'
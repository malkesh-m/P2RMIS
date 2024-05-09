CREATE TABLE [dbo].[ReferralMapping]
(
	[ReferralMappingId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProgramYearId] INT NOT NULL,
	[ReceiptCycle] INT NOT NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ReferralMapping_ProgramYear] FOREIGN KEY ([ProgramYearId]) REFERENCES [ProgramYear]([ProgramYearId])
)
GO
CREATE UNIQUE INDEX [UNX_ReferralMapping_ProgramYearId_ReceiptCycle] ON [dbo].[ReferralMapping] ([ProgramYearId], [ReceiptCycle]) WHERE (DeletedFlag = 0)
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a referral mapping activity',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReferralMapping',
    @level2type = N'COLUMN',
    @level2name = N'ReferralMappingId'
GO
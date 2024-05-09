CREATE TABLE [dbo].[ReferralMappingData]
(
	[ReferralMappingDataId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ReferralMappingId] INT NOT NULL,
	[ApplicationId] INT NOT NULL,
	[SessionPanelId] INT NOT NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ReferralMappingData_ReferralMapping] FOREIGN KEY ([ReferralMappingId]) REFERENCES [ReferralMapping]([ReferralMappingId]), 
    CONSTRAINT [FK_ReferralMappingData_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([ApplicationId]), 
    CONSTRAINT [FK_ReferralMappingData_SessionPanel] FOREIGN KEY ([SessionPanelId]) REFERENCES [SessionPanel]([SessionPanelId])
)
GO
CREATE INDEX [IX_ReferralMappingData_SessionPanelId] ON [dbo].[ReferralMappingData] ([SessionPanelId])
GO
CREATE UNIQUE INDEX [UNX_ReferralMappingData_ReferralMappingId_SessionPanelId_ApplicationId] ON [dbo].[ReferralMappingData] ([ReferralMappingId], [SessionPanelId], [ApplicationId]) WHERE (DeletedFlag = 0)
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a referral mapping data entry',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReferralMappingData',
    @level2type = N'COLUMN',
    @level2name = N'ReferralMappingDataId'
GO
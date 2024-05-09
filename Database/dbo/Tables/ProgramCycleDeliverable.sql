CREATE TABLE [dbo].[ProgramCycleDeliverable]
(
	[ProgramCycleDeliverableId] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProgramYearId] INT NOT NULL,
	[ReceiptCycle] INT NULL,
	[ClientDataDeliverableId] INT NOT NULL, 
    [GeneratedUserId] INT NOT NULL, 
    [GeneratedDate] DATETIME2(0) NOT NULL, 
	[QcFlag] BIT NOT NULL DEFAULT 0,
    [QcUserId] INT NULL, 
    [QcDate] DATETIME2(0) NULL,
	[DeliverableFile] NVARCHAR(MAX) NULL,
	[QcDataFile] varbinary(MAX) NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ProgramCycleDeliverable_ProgramYear] FOREIGN KEY ([ProgramYearId]) REFERENCES [ProgramYear]([ProgramYearId]), 
    CONSTRAINT [FK_ProgramCycleDeliverable_ClientDataDeliverable] FOREIGN KEY ([ClientDataDeliverableId]) REFERENCES [ClientDataDeliverable]([ClientDataDeliverableId]), 
    CONSTRAINT [FK_ProgramCycleDeliverable_Generate_User] FOREIGN KEY ([GeneratedUserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_ProgramCycleDeliverable_QC_User] FOREIGN KEY ([QcUserId]) REFERENCES [User]([UserId]), 
)

GO

CREATE INDEX [IX_ProgramCycleDeliverable_ProgramYearId_ReceiptCycle_ClientDataDeliverableId] ON [dbo].[ProgramCycleDeliverable] ([ProgramYearId],[ReceiptCycle],[ClientDataDeliverableId])

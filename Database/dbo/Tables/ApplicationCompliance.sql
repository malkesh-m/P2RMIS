CREATE TABLE [dbo].[ApplicationCompliance]
(
	[ApplicationComplianceId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationId] INT NOT NULL, 
    [ComplianceStatusId] INT NOT NULL, 
    [Comment] NVARCHAR(2000) NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationCompliance_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([ApplicationId]), 
    CONSTRAINT [FK_ApplicationCompliance_ComplianceStatus] FOREIGN KEY ([ComplianceStatusId]) REFERENCES [ComplianceStatus]([ComplianceStatusId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application''s compliance status',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationCompliance',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationComplianceId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationCompliance',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a compliance status',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationCompliance',
    @level2type = N'COLUMN',
    @level2name = N'ComplianceStatusId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Comment supplied regarding the compliance status',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationCompliance',
    @level2type = N'COLUMN',
    @level2name = N'Comment'
GO

CREATE INDEX [IX_ApplicationCompliance_ApplicationId_DeletedFlag] ON [dbo].[ApplicationCompliance] ([ApplicationId],[DeletedFlag])
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationCompliance] TO [web-p2rmis]
    AS [dbo];
GO
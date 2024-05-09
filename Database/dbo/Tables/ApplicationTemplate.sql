CREATE TABLE [dbo].[ApplicationTemplate]
(
	[ApplicationTemplateId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ApplicationId] INT NOT NULL, 
	[ApplicationStageId] INT NOT NULL,
	[MechanismTemplateId] INT NOT NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationTemplate_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([ApplicationId]), 
    CONSTRAINT [FK_ApplicationTemplate_MechanismTemplate] FOREIGN KEY ([MechanismTemplateId]) REFERENCES [MechanismTemplate]([MechanismTemplateId]), 
    --TODO: Enable after summary data is corrected-- CONSTRAINT [UN_ApplicationTemplate_ApplicationId_MechanismTemplateId] UNIQUE ([ApplicationStageId],[MechanismTemplateId]), 
    CONSTRAINT [FK_ApplicationTemplate_PanelApplicationStage] FOREIGN KEY ([ApplicationStageId]) REFERENCES [ApplicationStage]([ApplicationStageId])
)

GO

CREATE INDEX [IX_ApplicationTemplate_ApplicationId_MechanismTemplateId] ON [dbo].[ApplicationTemplate] ([ApplicationId],[MechanismTemplateId])
GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationTemplate] TO [web-p2rmis]
    AS [dbo];
GO

CREATE INDEX [IX_ApplicationTemplate_ApplicationStageId_DeletedFlag] ON [dbo].[ApplicationTemplate] ([ApplicationStageId],[DeletedFlag]) INCLUDE ([MechanismTemplateId])

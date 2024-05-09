CREATE TABLE [dbo].[ApplicationTemplateElement]
(
	[ApplicationTemplateElementId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ApplicationTemplateId] INT NOT NULL, 
    [MechanismTemplateElementId] INT NOT NULL, 
	[PanelApplicationReviewerAssignmentId] INT NULL,
	[DiscussionNoteFlag] BIT NOT NULL DEFAULT 0,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationTemplateElement_ApplicationTemplate] FOREIGN KEY ([ApplicationTemplateId]) REFERENCES [ApplicationTemplate]([ApplicationTemplateId]), 
    CONSTRAINT [FK_ApplicationTemplateElement_MechanismTemplateElement] FOREIGN KEY ([MechanismTemplateElementId]) REFERENCES [MechanismTemplateElement]([MechanismTemplateElementId]), 
    CONSTRAINT [UN_ApplicationTemplateElement_ApplicationTemplateId_MechanismTemplateElementId] UNIQUE ([ApplicationTemplateId],[MechanismTemplateElementId],[PanelApplicationReviewerAssignmentId], [DeletedDate])
)

GO

CREATE INDEX [IX_ApplicationTemplateElement_ApplicationTemplateId_MechanismTemplateElementId] ON [dbo].[ApplicationTemplateElement] ([ApplicationTemplateId],[MechanismTemplateElementId])
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationTemplateElement] TO [web-p2rmis]
    AS [dbo];
GO

CREATE INDEX [IX_ApplicationTemplateElement_MechanismTemplateElementId_DeletedFlag] ON [dbo].[ApplicationTemplateElement] ([MechanismTemplateElementId], [DeletedFlag])

CREATE TABLE [dbo].[ApplicationWorkflowStepElementContent]
(
	[ApplicationWorkflowStepElementContentId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY NONCLUSTERED, 
	[ApplicationWorkflowStepElementId] INT NOT NULL,
    [Score] DECIMAL(18, 1) NULL, 
    [ContentText] NVARCHAR(MAX) NULL, 
	[ContentTextNoMarkup] NVARCHAR(MAX) NULL,
    [Abstain] BIT NOT NULL DEFAULT 0,
	    [CritiqueRevised] BIT NOT NULL DEFAULT 0, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationWorkflowStepElementContent_ApplicationWorkflowStepElement] FOREIGN KEY ([ApplicationWorkflowStepElementId]) REFERENCES [ApplicationWorkflowStepElement]([ApplicationWorkflowStepElementId]) 
)

GO

CREATE CLUSTERED INDEX [IX_ApplicationWorkflowStepElementContent_ApplicationWorkflowStepElementId_Score] ON [dbo].[ApplicationWorkflowStepElementContent] ([ApplicationWorkflowStepElementId],[Score]) 
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationWorkflowStepElementContent] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for Application Workflow Step Element Content',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepElementContent',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationWorkflowStepElementContentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for Application Workflow Step Element',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepElementContent',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationWorkflowStepElementId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Numeric rating for the element',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepElementContent',
    @level2type = N'COLUMN',
    @level2name = N'Score'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Text evaluation for the element',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepElementContent',
    @level2type = N'COLUMN',
    @level2name = N'ContentText'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Text evaluation for the element without markup',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepElementContent',
    @level2type = N'COLUMN',
    @level2name = N'ContentTextNoMarkup'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether evaluation was abstained for the element',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepElementContent',
    @level2type = N'COLUMN',
    @level2name = N'Abstain'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'True if the critique has been revised',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepElementContent',
    @level2type = N'COLUMN',
    @level2name = N'CritiqueRevised'
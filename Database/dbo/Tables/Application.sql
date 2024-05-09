CREATE TABLE [dbo].[Application]
(
	[ApplicationId] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY, 
	[ProgramMechanismId] INT NOT NULL,
	[ParentApplicationId] INT NULL,
    [LogNumber] VARCHAR(12) NOT NULL, 
    [ApplicationTitle] NVARCHAR(500) NULL, 
    [ResearchArea] VARCHAR(200) NULL, 
	[Keywords] VARCHAR(1000) NULL,
	[ProjectStartDate] datetime2(0) NULL,
	[ProjectEndDate] datetime2(0) NULL,
	[WithdrawnFlag] BIT NOT NULL DEFAULT 0,
	[WithdrawnBy] INT NULL,
	[WithdrawnDate] datetime2(0) NULL,
    [CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [UN_Application_ProgramMechanismId_LogNumber] UNIQUE ([ProgramMechanismId], [LogNumber], [DeletedDate]), 
    CONSTRAINT [FK_Application_ProgramMechanism] FOREIGN KEY ([ProgramMechanismId]) REFERENCES [ProgramMechanism]([ProgramMechanismId]), 
    CONSTRAINT [FK_Application_Application] FOREIGN KEY ([ParentApplicationId]) REFERENCES [Application]([ApplicationId])
)

GO

CREATE INDEX [IX_Application_LogNumber] ON [dbo].[Application] ([LogNumber])

GO

CREATE INDEX [IX_Application_ProgramMechanismId_LogNumber] ON [dbo].[Application] ([ProgramMechanismId], [LogNumber])

GO

CREATE NONCLUSTERED INDEX [IX_Application_ParentApplicationId] ON [dbo].[Application] ([ParentApplicationId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a grant application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an award mechanism',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = N'COLUMN',
    @level2name = N'ProgramMechanismId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Application identifier that specifies a relationship to that application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = N'COLUMN',
    @level2name = N'ParentApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identification label of an application by a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = N'COLUMN',
    @level2name = N'LogNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Title describing the application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationTitle'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Generic description of the application''s research',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = N'COLUMN',
    @level2name = N'ResearchArea'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Keywords associated with the application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = N'COLUMN',
    @level2name = N'Keywords'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date application is projected to start if funded',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = N'COLUMN',
    @level2name = N'ProjectStartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date application is projected to end if funded',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = N'COLUMN',
    @level2name = N'ProjectEndDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Application submitted for consideration in a grant award or other entity',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = NULL,
    @level2name = NULL
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[Application] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the application has been withdrawn from review',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = N'COLUMN',
    @level2name = N'WithdrawnFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User who marked the application as withdrawn',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = N'COLUMN',
    @level2name = N'WithdrawnBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The date the application was marked as withdrawn',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Application',
    @level2type = N'COLUMN',
    @level2name = N'WithdrawnDate'
GO


CREATE NONCLUSTERED INDEX [IX_Application_DeletedFlag_ApplicationId_ProgramMechanismId] ON [dbo].[Application]
(
	[DeletedFlag] ASC,
	[ApplicationId] ASC,
	[ProgramMechanismId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_Application_DeletedFlag_ProgramMechanismId_ApplicationId_LogNumber] ON [dbo].[Application]
(
	[DeletedFlag] ASC,
	[ProgramMechanismId] ASC,
	[ApplicationId] ASC,
	[LogNumber] ASC
)
INCLUDE ( 	[ApplicationTitle]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

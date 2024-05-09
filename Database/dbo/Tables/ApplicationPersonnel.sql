CREATE TABLE [dbo].[ApplicationPersonnel]
(
	[ApplicationPersonnelId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationId] INT NOT NULL, 
    [ClientApplicationPersonnelTypeId] INT NOT NULL, 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [MiddleInitial] NVARCHAR(3) NULL, 
    [OrganizationName] NVARCHAR(250) NULL, 
    [PhoneNumber] VARCHAR(50) NULL, 
    [EmailAddress] VARCHAR(80) NULL, 
	[PrimaryFlag] bit NOT NULL DEFAULT 0,
	[Source] VARCHAR(50) NULL,
	[StateAbbreviation] VARCHAR(5) NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationPersonnel_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([ApplicationId]), 
    CONSTRAINT [FK_ApplicationPersonnel_ApplicationPersonnelType] FOREIGN KEY ([ClientApplicationPersonnelTypeId]) REFERENCES [ClientApplicationPersonnelType]([ClientApplicationPersonnelTypeId])
)

GO

CREATE INDEX [IX_ApplicationPersonnel_ApplicationId_ApplicationPersonnelTypeId] ON [dbo].[ApplicationPersonnel] ([ApplicationId], [ClientApplicationPersonnelTypeId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application person',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationPersonnelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the type of personnel a person is considered',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = N'COLUMN',
    @level2name = N'ClientApplicationPersonnelTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'First name of person',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = N'COLUMN',
    @level2name = N'FirstName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Last name of person',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = N'COLUMN',
    @level2name = N'LastName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Middle initial of person',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = N'COLUMN',
    @level2name = N'MiddleInitial'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of organization or instiution associated with person',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = N'COLUMN',
    @level2name = N'OrganizationName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Phone number of application person',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = N'COLUMN',
    @level2name = N'PhoneNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Email address of application person',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = N'COLUMN',
    @level2name = N'EmailAddress'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the person is considered the primary personnel on an application (e.g. PI)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = N'COLUMN',
    @level2name = N'PrimaryFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Personnel listed to carry out work on an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Source from which the personnel was listed',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = N'COLUMN',
    @level2name = N'Source'
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationPersonnel] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'State abbreviation in which organization resides',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationPersonnel',
    @level2type = N'COLUMN',
    @level2name = N'StateAbbreviation'
GO

CREATE NONCLUSTERED INDEX [IX_ApplicationPersonnel_PrimaryFlag_DeletedFlag_ApplicationId_ApplicationPersonnelId] ON [dbo].[ApplicationPersonnel]
(
	[PrimaryFlag] ASC,
	[DeletedFlag] ASC,
	[ApplicationId] ASC,
	[ApplicationPersonnelId] ASC
)
INCLUDE ( 	[FirstName],
	[LastName],
	[OrganizationName]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

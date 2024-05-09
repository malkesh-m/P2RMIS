CREATE TABLE [dbo].[UserDegree]
(
	[UserDegreeId] INT PRIMARY KEY NOT NULL IDENTITY,
	[UserInfoId] INT NOT NULL, 
    [DegreeId] INT NOT NULL, 
	[LegacyDegreeId] INT NULL,
    [Major] VARCHAR(40) NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_UserDegree_User] FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo]([UserInfoId]), 
    CONSTRAINT [FK_UserDegree_Degree] FOREIGN KEY ([DegreeId]) REFERENCES [Degree]([DegreeId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s academic degree',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserDegree',
    @level2type = N'COLUMN',
    @level2name = N'UserDegreeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserDegree',
    @level2type = N'COLUMN',
    @level2name = 'UserInfoId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an academic degree',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserDegree',
    @level2type = N'COLUMN',
    @level2name = N'DegreeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Field of study for the degree',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserDegree',
    @level2type = N'COLUMN',
    @level2name = N'Major'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Academic degrees associated with a user profile',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserDegree',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_UserDegree_UserId_DegreeId] ON [dbo].[UserDegree] ([UserInfoId], [DegreeId])

GO

CREATE INDEX [IX_UserDegree_Major_UserId] ON [dbo].[UserDegree] ([Major], [UserInfoId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for a degree for mapping purposes',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserDegree',
    @level2type = N'COLUMN',
    @level2name = N'LegacyDegreeId'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[UserDegree] TO [web-p2rmis]
    AS [dbo];
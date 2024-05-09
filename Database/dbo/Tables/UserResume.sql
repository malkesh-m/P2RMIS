CREATE TABLE [dbo].[UserResume]
(
	[UserResumeId] INT NOT NULL IDENTITY, 
    [UserInfoId] INT NOT NULL, 
    [LegacyCvId] INT NULL, 
    [DocType] VARCHAR(10) NOT NULL DEFAULT 'pdf', 
	[ResumeFile] VARBINARY(MAX) NULL,
    [FileName] VARCHAR(40) NOT NULL, 
    [Version] INT NOT NULL DEFAULT 1, 
    [ReceivedDate] datetime2(0) NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [PK_UserResume] PRIMARY KEY ([UserResumeId]), 
    CONSTRAINT [FK_UserResume_UserInfo] FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo]([UserInfoId]), 
    CONSTRAINT [UN_UserResume_UserInfoId] UNIQUE ([UserInfoId], [DeletedDate])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Information regarding a user''s resume or curriculum vitae',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserResume',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s resume',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserResume',
    @level2type = N'COLUMN',
    @level2name = N'UserResumeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s personal information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserResume',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier from legacy PPL_CV table',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserResume',
    @level2type = N'COLUMN',
    @level2name = N'LegacyCvId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The document type of the resume',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserResume',
    @level2type = N'COLUMN',
    @level2name = N'DocType'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date resume was received',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserResume',
    @level2type = N'COLUMN',
    @level2name = N'ReceivedDate'
GO

CREATE INDEX [IX_UserResume_UserInfoId] ON [dbo].[UserResume] ([UserInfoId])
GO

CREATE INDEX [IX_UserResume_LegacyCvId] ON [dbo].[UserResume] ([LegacyCvId])
GO

CREATE FULLTEXT INDEX ON [dbo].[UserResume] ([ResumeFile] TYPE COLUMN [DocType]) KEY INDEX [PK_UserResume] ON [Catalog_Resume] WITH CHANGE_TRACKING AUTO
GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[UserResume] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Resume file name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserResume',
    @level2type = N'COLUMN',
    @level2name = N'FileName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Binary data of a resume file',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserResume',
    @level2type = N'COLUMN',
    @level2name = N'ResumeFile'
GO

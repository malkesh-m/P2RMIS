CREATE TABLE [dbo].[UserInfo] (
    [UserInfoID]     INT            IDENTITY (1, 1) NOT NULL,
    [UserID]         INT            NOT NULL,
	[MilitaryRankId] INT            NULL,
	[MilitaryStatusTypeId] INT NULL,
    [FirstName]      NVARCHAR (50)  NOT NULL,
    [MiddleName]     NVARCHAR (50)  NULL,
    [LastName]       NVARCHAR (100) NOT NULL,
	[NickName]       VARCHAR (50) NULL,
	[VendorId]		INT NULL,
	[VendorName]	 NVARCHAR(200) NULL,
	[Institution]    VARCHAR(125) NULL,
	[Department]		 VARCHAR(100) NULL,
	[Position]		 VARCHAR(100) NULL,
    [BadgeName]      NVARCHAR (100) NULL,
	[PrefixId] INT NULL,
    [SuffixText] VARCHAR(50) NULL, 
	[GenderId] INT NULL,
	[EthnicityId] INT NULL,
    [AcademicRankId] INT NULL,
	[Expertise] VARCHAR(500) NULL,
	[DegreeNotApplicable] BIT NOT NULL DEFAULT 0,
    [ProfessionalAffiliationId] INT NULL,   
    [CreatedBy]      INT            NULL,
    [CreatedDate]    DATETIME       NULL,
    [ModifiedBy]     INT            NULL,
    [ModifiedDate]   DATETIME       NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL
    CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED ([UserInfoID] ASC),
    CONSTRAINT [FK_UserInfo_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID]) ON DELETE CASCADE,
    CONSTRAINT [UN_UserInfo] UNIQUE NONCLUSTERED ([UserID], [DeletedDate] ASC), 
    CONSTRAINT [FK_UserInfo_Ethnicity] FOREIGN KEY ([EthnicityId]) REFERENCES [Ethnicity]([EthnicityId]), 
    CONSTRAINT [FK_UserInfo_Gender] FOREIGN KEY ([GenderId]) REFERENCES [Gender]([GenderId]), 
    CONSTRAINT [FK_UserInfo_Prefix] FOREIGN KEY ([PrefixId]) REFERENCES [Prefix]([PrefixId]), 
    CONSTRAINT [FK_UserInfo_MilitaryRank] FOREIGN KEY ([MilitaryRankId]) REFERENCES [MilitaryRank]([MilitaryRankId]), 
    CONSTRAINT [FK_UserInfo_MilitaryStatusType] FOREIGN KEY ([MilitaryStatusTypeId]) REFERENCES [MilitaryStatusType]([MilitaryStatusTypeId])
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s personal information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'UserID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'First name of user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'FirstName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Middle name of user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'MiddleName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Last name of user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'LastName'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name used for a badge name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'BadgeName'
GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User''s personal information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s name prefix',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'PrefixId'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s gender',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'GenderId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s ethnicity',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'EthnicityId'
GO

CREATE INDEX [IX_UserInfo_UserID] ON [dbo].[UserInfo] ([UserID])

GO

CREATE NONCLUSTERED INDEX [IX_UserInfo_LastName]
ON [dbo].[UserInfo] ([LastName])
INCLUDE ([UserInfoID],[UserID],[FirstName],[MiddleName],[NickName])

GO

CREATE INDEX [IX_UserInfo_LastNameFirstNameNickName] ON [dbo].[UserInfo] ([LastName], [FirstName], [Nickname])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s military rank',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'MilitaryRankId'
GO

GO

GO


GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[UserInfo] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for military status ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'MilitaryStatusTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'nick name of user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'NickName'
GO


EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'user''s name suffix(es)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'SuffixText'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s academic rank',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'AcademicRankId'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Degree not applicable ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'DegreeNotApplicable'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'OBSOLETE: Use UserVendor.VendorName instead',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'VendorName'

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'OBSOLETE: Use UserVendor.VendorId instead',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserInfo',
    @level2type = N'COLUMN',
    @level2name = N'VendorId'
GO

CREATE NONCLUSTERED INDEX [IX_UserInfo_DeletedFlag_UserID] ON [dbo].[UserInfo]
(
	[DeletedFlag] ASC,
	[UserID] ASC,
	[UserInfoID] ASC
)
INCLUDE ( 	[FirstName],
	[LastName]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

/*
The following procedures are ran on the linked server to enable full text search.  They are not included in the deployment
as that would require access beyond read/write on the linked server which is not granted to the linked server login for security puproses
*/

CREATE TABLE [dbo].[UserResume](
	[UserResumeId] [int] NOT NULL,
	[ResumeFile] [varbinary](max) NULL,
	[DocType] [varchar](10) NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[DeletedFlag] [bit] NOT NULL,
 CONSTRAINT [PK_UserResume] PRIMARY KEY CLUSTERED 
(
	[UserResumeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserResume] ADD  CONSTRAINT [DF_UserResume_Timestamp]  DEFAULT (CONVERT([datetime2],(sysdatetimeoffset() AT TIME ZONE 'Eastern Standard Time'))) FOR [Timestamp]
GO

ALTER TABLE [dbo].[UserResume] ADD  CONSTRAINT [DF_UserResume_DeletedFlag]  DEFAULT ((0)) FOR [DeletedFlag]
GO

CREATE FULLTEXT CATALOG [Catalog_Resume] WITH ACCENT_SENSITIVITY = ON
AS DEFAULT
GO
CREATE FULLTEXT INDEX ON UserResume(ResumeFile TYPE COLUMN DocType) KEY INDEX PK_UserResume;
GO

CREATE PROCEDURE dbo.UpdateResume
	-- Add the parameters for the stored procedure here
	@UserResumeId int, 
	@ResumeFile varbinary(max),
	@DocType varchar(10),
	@DeletedFlag bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE UserResume
	SET ResumeFile = @ResumeFile,
	DocType = @DocType,
	DeletedFlag = @DeletedFlag
	WHERE UserResumeId = @UserResumeId;
END
GO

CREATE PROCEDURE [dbo].[InsertResume]
	-- Add the parameters for the stored procedure here
	@UserResumeId int, 
	@ResumeFile varbinary(max),
	@DocType varchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO UserResume (UserResumeId, ResumeFile, DocType)
	VALUES (@UserResumeId, @ResumeFile, @DocType);
	
END

GO

GRANT EXECUTE ON [dbo].[InsertResume] TO [dbft-user]
GO

GRANT EXECUTE ON [dbo].[UpdateResume] TO [dbft-user]
GO
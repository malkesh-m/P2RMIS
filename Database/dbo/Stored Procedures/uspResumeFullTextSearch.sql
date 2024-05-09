CREATE PROCEDURE [dbo].[uspResumeFullTextSearch]
		-- String that should be searched
	@SearchString1 nvarchar(4000), 
	@SearchString2 nvarchar(4000), --NULLABLE
	@SearchString3 nvarchar(4000), --NULLABLE
	@SearchString4 nvarchar(4000), --NULLABLE
	@SearchString5 nvarchar(4000) --NULLABLE
AS
BEGIN
	DECLARE @sql nvarchar(max)
	DECLARE @T table([UserResumeId] [int] NOT NULL,
	[UserInfoId] [int] NOT NULL,
	[LegacyCvId] [int] NULL,
	[DocType] [varchar](10) NOT NULL,
	[FileName] [varchar](40) NOT NULL,
	[Version] [int] NOT NULL,
	[ReceivedDate] [smalldatetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [smalldatetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [smalldatetime] NULL)
	--Since dynamic sql is used, sanitize against SQL injection using recommend wrapper
	SET @SearchString1 = REPLACE(@SearchString1,'''','''''');
	SET @SearchString2 = REPLACE(@SearchString2,'''','''''');
	SET @SearchString3 = REPLACE(@SearchString3,'''','''''');
	SET @SearchString4 = REPLACE(@SearchString4,'''','''''');
	SET @SearchString5 = REPLACE(@SearchString5,'''','''''');


	--This is a workaround hack to be able to use nullable parameters as full text predicate
	IF ISNULL(@SearchString2, '') = '' SET @SearchString2 = '""';
	IF ISNULL(@SearchString3, '') = '' SET @SearchString3 = '""';
	IF ISNULL(@SearchString4, '') = '' SET @SearchString4 = '""';
	IF ISNULL(@SearchString5, '') = '' SET @SearchString5 = '""';
	SELECT @sql = 'SELECT ViewUserResume.[UserResumeId]
      ,ViewUserResume.[UserInfoId]
      ,ViewUserResume.[LegacyCvId]
      ,ViewUserResume.[DocType]
      ,ViewUserResume.[FileName]
      ,ViewUserResume.[Version]
      ,ViewUserResume.[ReceivedDate]
      ,ViewUserResume.[CreatedBy]
      ,ViewUserResume.[CreatedDate]
      ,ViewUserResume.[ModifiedBy]
      ,ViewUserResume.[ModifiedDate]
  FROM [$(LinkedServerName)].[$(DatabaseName)].[dbo].[UserResume] LinkedResume
  INNER JOIN [ViewUserResume] ON LinkedResume.UserResumeId = ViewUserResume.UserResumeId
  WHERE Contains(ResumeFile, ''' + @SearchString1 +''') AND ''' +
  @SearchString2 + ''' = ''""'' OR Contains(LinkedResume.ResumeFile, ''' + @SearchString2 + ''') AND ''' +
  @SearchString3 + ''' = ''""'' OR Contains(LinkedResume.ResumeFile, ''' + @SearchString3 + ''') AND ''' +
  @SearchString4 + ''' = ''""'' OR Contains(LinkedResume.ResumeFile, ''' + @SearchString4 + ''') AND ''' +
  @SearchString5 + ''' = ''""'' OR Contains(LinkedResume.ResumeFile, ''' + @SearchString5 + ''')'
INSERT INTO @T (UserResumeId, UserInfoId, LegacyCvId, DocType, [FileName], [Version], ReceivedDate, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
EXEC sp_executesql @sql;

SELECT UserResumeId, UserInfoId, LegacyCvId, DocType, [FileName], [Version], ReceivedDate, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
FROM @T;
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspResumeFullTextSearch] TO [NetSqlAzMan_Users]
    AS [dbo];
GO

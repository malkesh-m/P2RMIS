-- =============================================
-- Author:		Joe Gao
-- Create date: 4/6/2015
-- Description:	This stored procedure searches for exact/fuzzy matched user records by first/last names
-- =============================================
CREATE PROCEDURE [dbo].[uspGetUsersByName] 
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @ExactRelevancy Decimal(2,1);
	DECLARE @FuzzyRelevancy Decimal(2,1);
	SET @ExactRelevancy = 1;
	SET @FuzzyRelevancy = 0.5;

	IF (@FirstName IS NULL) 
		SET @FirstName = '';
	IF (@LastName IS NULL) 
		SET @LastName = '';		
		
    IF (@FirstName = '')
      BEGIN
		SELECT UserID, UserInfoID, FirstName, MiddleName, LastName, NickName, Relevancy = @ExactRelevancy
		FROM UserInfo
		WHERE UserInfo.LastName = @LastName  
		UNION
		SELECT UserID, UserInfoID, FirstName, MiddleName, LastName, NickName, Relevancy = @FuzzyRelevancy
		FROM UserInfo
		WHERE [dbo].[udfFuzzyMatch](UserInfo.LastName, @LastName) = 1
			AND UserInfo.LastName <> @LastName;
	  END
	ELSE IF (@LastName = '')
	  BEGIN
		SELECT UserID, UserInfoID, FirstName, MiddleName, LastName, NickName, Relevancy = @ExactRelevancy
		FROM UserInfo
		WHERE UserInfo.FirstName = @FirstName  
		UNION
		SELECT UserID, UserInfoID, FirstName, MiddleName, LastName, NickName, Relevancy = @FuzzyRelevancy
		FROM UserInfo
		WHERE [dbo].[udfFuzzyMatch](UserInfo.FirstName, @FirstName) = 1
			AND UserInfo.FirstName <> @FirstName;	  
	  END
	ELSE
      BEGIN
		SELECT UserID, UserInfoID, FirstName, MiddleName, LastName, NickName, Relevancy = @ExactRelevancy
		FROM UserInfo
		WHERE (UserInfo.FirstName = @FirstName OR ISNULL(UserInfo.NickName, '') = @FirstName)
			AND UserInfo.LastName = @LastName  
		UNION
		SELECT UserID, UserInfoID, FirstName, MiddleName, LastName, NickName, Relevancy = @FuzzyRelevancy
		FROM UserInfo
		WHERE ([dbo].[udfFuzzyMatch](UserInfo.FirstName, @FirstName) = 1 
				OR [dbo].[udfFuzzyMatch](ISNULL(UserInfo.NickName, ''), @FirstName) = 1)
			AND [dbo].[udfFuzzyMatch](UserInfo.LastName, @LastName) = 1
			AND ((UserInfo.FirstName <> @FirstName AND ISNULL(UserInfo.NickName, '') <> @FirstName)
				OR UserInfo.LastName <> @LastName);
	  END	
END
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspGetUsersByName] TO [NetSqlAzMan_Users]
    AS [dbo];
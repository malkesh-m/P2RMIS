-- =============================================
-- Author:		Joe Gao
-- Create date: 4/7/2015
-- Description:	This stored procedure searches for exact/fuzzy matched user records by user login name
-- =============================================
CREATE PROCEDURE [dbo].[uspGetUsersByUserLogin] 
	@UserLogin NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @ExactRelevancy Decimal(2,1);
	DECLARE @FuzzyRelevancy Decimal(2,1);
	SET @ExactRelevancy = 1;
	SET @FuzzyRelevancy = 0.5;

	SELECT [User].UserID, UserInfoID, FirstName, MiddleName, LastName, NickName, Relevancy = @ExactRelevancy
	FROM UserInfo
	INNER JOIN [User]
	ON UserInfo.UserID = [User].UserID
	WHERE [User].UserLogin = @UserLogin  
	UNION
	SELECT [User].UserID, UserInfoID, FirstName, MiddleName, LastName, NickName, Relevancy = @FuzzyRelevancy
	FROM UserInfo
	INNER JOIN [User]
	ON UserInfo.UserID = [User].UserID
	WHERE [dbo].[udfFuzzyMatch]([User].UserLogin, @UserLogin) = 1
		AND [User].UserLogin <> @UserLogin;
	
END
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspGetUsersByUserLogin] TO [NetSqlAzMan_Users]
    AS [dbo];

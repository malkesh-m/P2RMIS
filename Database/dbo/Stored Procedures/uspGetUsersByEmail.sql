-- =============================================
-- Author:		Joe Gao
-- Create date: 4/7/2015
-- Description:	This stored procedure searches for exact/fuzzy matched user records by email address
-- =============================================
CREATE PROCEDURE [dbo].[uspGetUsersByEmail] 
	@Email NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @ExactRelevancy Decimal(2,1);
	DECLARE @FuzzyRelevancy Decimal(2,1);
	SET @ExactRelevancy = 1;
	SET @FuzzyRelevancy = 0.5;

	SELECT UserID, UserInfo.UserInfoID, FirstName, MiddleName, LastName, NickName, Relevancy = @ExactRelevancy
	FROM UserInfo
	INNER JOIN UserEmail
	ON UserInfo.UserInfoID = UserEmail.UserInfoID
	WHERE UserEmail.Email = @Email  
	UNION
	SELECT UserID, UserInfo.UserInfoID, FirstName, MiddleName, LastName, NickName, Relevancy = @FuzzyRelevancy
	FROM UserInfo
	INNER JOIN UserEmail
	ON UserInfo.UserInfoID = UserEmail.UserInfoID
	WHERE [dbo].[udfFuzzyMatch](UserEmail.Email, @Email) = 1
		AND UserEmail.Email <> @Email;
	
END
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspGetUsersByEmail] TO [NetSqlAzMan_Users]
    AS [dbo];
-- =============================================
-- Author:		Coffey, Daniel
-- Create date: 9/11/2013
-- Description:	Accepts username and password from exising system and returns personId
-- =============================================
CREATE PROCEDURE [dbo].[GetPersonId]
	@Username nvarchar(25),
	@Password nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		SYS_Users.Person_ID
	FROM		[$(P2RMIS)].dbo.SYS_Users INNER JOIN 
				[$(P2RMIS)].dbo.PPL_People ON [$(P2RMIS)].dbo.SYS_Users.Person_ID = [$(P2RMIS)].dbo.PPL_People.Person_ID
					INNER JOIN [$(P2RMIS)].dbo.sys_pwd on 1 = 1
	WHERE 		UserID = @Username
		AND (Hashed_Password = 
				HashBytes('SHA1', convert(nvarchar(100),@Password))
			OR  PWD = 
				HashBytes('SHA1', convert(nvarchar(100),@Password))) AND Status = 'Active'
END

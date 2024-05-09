CREATE PROCEDURE [dbo].[uspAddNoteTestDataByLogNumber]
	@LogNumber varchar(12),
	@ReturnMessage varchar(200) OUT
AS
	DECLARE
	@UserId int = 10,
	@DateTimeNow datetime2(0) = dbo.GetP2rmisDateTime()
	
	INSERT INTO [dbo].[UserApplicationComment]
           ([UserID]
           ,[PanelApplicationId]
           ,[Comments]
           ,[CommentTypeID]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT @UserId, ViewPanelApplication.PanelApplicationId, 
	'This is test comment #' + CAST(FakeTable.Number AS varchar(5)) + ' for ' + CommentType.CommentTypeName + 's added ' +
	CAST(@DateTimeNow as VARCHAR(20)), CommentType.CommentTypeId, @UserId, @DateTimeNow, @UserId, @DateTimeNow
	FROM ViewApplication INNER JOIN
		ViewPanelApplication ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId CROSS JOIN
		CommentType CROSS JOIN
		(SELECT 1  AS Number UNION ALL Select 2 UNION ALL SELECT 3 UNION ALL Select 4 UNION ALL Select 5 AS Number) FakeTable
	WHERE ViewApplication.LogNumber = @LogNumber AND CommentType.CommentTypeId IN (1,2,3,5)

SET @ReturnMessage = 'Finished. ' + CAST(@@ROWCOUNT AS varchar(10)) + ' rows added.'
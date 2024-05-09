
CREATE TRIGGER [ProBudgetSyncTrigger]
ON [$(P2RMIS)].[dbo].PRO_Budget
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	UPDATE [$(DatabaseName)].[dbo].[ApplicationBudget]
	SET DirectCosts = inserted.Requested_Direct, IndirectCosts = inserted.Requested_Indirect, TotalFunding = inserted.Req_Total_Funding,
	ModifiedBy = vun.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(P2RMIS)].[dbo].PRO_Budget PRO_Budget ON inserted.LOG_NO = PRO_Budget.LOG_NO INNER JOIN
	[$(DatabaseName)].[dbo].[Application] app ON inserted.LOG_NO = app.LogNumber INNER JOIN
	[$(DatabaseName)].[dbo].[ApplicationBudget] ApplicationBudget ON app.ApplicationId = ApplicationBudget.ApplicationId LEFT OUTER JOIN
	[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun ON inserted.LAST_UPDATED_BY = vun.UserName
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	INSERT INTO [$(DatabaseName)].[dbo].[ApplicationBudget]
           ([ApplicationId]
           ,[DirectCosts]
           ,[IndirectCosts]
           ,[TotalFunding]
		   ,[CreatedBy]
		   ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT app.ApplicationId, bud.Requested_Direct, bud.Requested_Indirect, bud.Req_Total_Funding,
		vun.UserID, bud.LAST_UPDATE_DATE, vun.UserID, bud.LAST_UPDATE_DATE
	FROM inserted bud INNER JOIN
		[$(P2RMIS)].[dbo].PRO_Budget PRO_Budget ON bud.LOG_NO = PRO_Budget.LOG_NO INNER JOIN
		[$(DatabaseName)].[dbo].[Application] app ON bud.LOG_NO = app.LogNumber LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun ON bud.LAST_UPDATED_BY = vun.UserName
	WHERE [app].DeletedFlag = 0
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[ApplicationBudget]
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
	[$(DatabaseName)].[dbo].[Application] app ON deleted.LOG_NO = app.LogNumber INNER JOIN
	[$(DatabaseName)].[dbo].[ApplicationBudget] ApplicationBudget ON app.ApplicationId = ApplicationBudget.ApplicationId 
	WHERE ApplicationBudget.DeletedFlag = 0
END


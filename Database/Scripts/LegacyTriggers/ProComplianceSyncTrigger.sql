CREATE TRIGGER [ProComplianceSyncTrigger]
ON [$(P2RMIS)].[dbo].PRO_Compliance
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[ApplicationCompliance]
	SET ComplianceStatusId = [ComplianceStatus].ComplianceStatusId, Comment = inserted.Comment,
		ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON inserted.Log_NO = [Application].LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationCompliance] ApplicationCompliance ON Application.ApplicationId = ApplicationCompliance.ApplicationId INNER JOIN
		[$(DatabaseName)].[dbo].[ComplianceStatus] ComplianceStatus ON inserted.Compliance_Status = ComplianceStatus.ComplianceStatusLabel LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.Last_Updated_By = VUN.UserName
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[ApplicationCompliance]
           ([ApplicationId]
           ,[ComplianceStatusId]
           ,[Comment]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT [Application].ApplicationId, ComplianceStatus.ComplianceStatusId, PRO_Compliance.Comment, VUN.UserId, dbo.GetP2rmisDateTime(), VUN.UserId, dbo.GetP2rmisDateTime()
	FROM inserted PRO_Compliance INNER JOIN
	[$(DatabaseName)].[dbo].[Application] Application ON PRO_Compliance.Log_No = [Application].LogNumber INNER JOIN
	[$(DatabaseName)].[dbo].[ComplianceStatus] ComplianceStatus ON PRO_Compliance.Compliance_Status = ComplianceStatus.ComplianceStatusLabel LEFT OUTER JOIN
	[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON PRO_Compliance.Last_Updated_By = VUN.UserName
	END
	--DELETE
	ELSE
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[ApplicationCompliance]
	SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON deleted.LOG_NO = Application.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationCompliance] ApplicationCompliance ON Application.ApplicationId = ApplicationCompliance.ApplicationId
	END

END
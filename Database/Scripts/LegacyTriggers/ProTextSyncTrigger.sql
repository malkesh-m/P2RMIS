
CREATE TRIGGER [ProTextSyncTrigger]
ON [$(P2RMIS)].[dbo].PRO_Text
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	UPDATE [$(DatabaseName)].[dbo].[ApplicationText]
	SET ClientApplicationTextTypeId = catt.ClientApplicationTextTypeId, BodyText = CAST(PRO_Text.BodyText AS nvarchar(max)), 
	ModifiedDate = inserted.LAST_UPDATED_DATE
	FROM inserted INNER JOIN
		[$(P2RMIS)].dbo.PRO_Text PRO_Text ON inserted.TextId = PRO_Text.TextId INNER JOIN
		[$(P2RMIS)].dbo.PRO_Text_Type protexttype ON inserted.TextTypeID = protexttype.TextTypeID INNER JOIN
		[$(DatabaseName)].[dbo].[Application] app ON inserted.Log_No = app.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientApplicationTextType] catt ON protexttype.TextType = catt.TextType AND cat.ClientId = catt.ClientId INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationText] apptxt on app.ApplicationId = apptxt.ApplicationId AND catt.ClientApplicationTextTypeId = apptxt.ClientApplicationTextTypeId
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	INSERT INTO [$(DatabaseName)].[dbo].[ApplicationText]
           ([ApplicationId]
           ,[ClientApplicationTextTypeId]
           ,[BodyText]
           ,[AbstractFlag]
           ,[CreatedDate]
           ,[ModifiedDate])
	SELECT app.ApplicationId, catt.ClientApplicationTextTypeId, CAST(pro_text.BodyText AS nvarchar(max)), CASE WHEN pm.AbstractFormat = 'Data' THEN 1 ELSE 0 END, 
		protext.LAST_UPDATED_DATE, protext.LAST_UPDATED_DATE
	FROM inserted protext INNER JOIN
	[$(P2RMIS)].dbo.PRO_Text PRO_Text ON protext.TextId = PRO_Text.TextId INNER JOIN
	[$(P2RMIS)].dbo.PRO_Text_Type protexttype ON protext.TextTypeID = protexttype.TextTypeID INNER JOIN
		[$(DatabaseName)].[dbo].[Application] app ON protext.LOG_NO = app.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientApplicationTextType] catt ON protexttype.TextType = catt.TextType AND cat.ClientId = catt.ClientId
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[ApplicationText]
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
		[$(P2RMIS)].dbo.PRO_Text_Type protexttype ON deleted.TextTypeID = protexttype.TextTypeID INNER JOIN
		[$(DatabaseName)].[dbo].[Application] app ON deleted.Log_No = app.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientApplicationTextType] catt ON protexttype.TextType = catt.TextType AND cat.ClientId = catt.ClientId INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationText] apptxt on app.ApplicationId = apptxt.ApplicationId AND catt.ClientApplicationTextTypeId = apptxt.ClientApplicationTextTypeId
	WHERE apptxt.DeletedFlag = 0
END

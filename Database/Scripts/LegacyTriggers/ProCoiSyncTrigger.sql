
CREATE TRIGGER [ProCoiSyncTrigger]
ON [$(P2RMIS)].[dbo].PRO_COI
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE (we cannot update ClientApplicationPersonnelTypeId at this time do to un-unique mapping to old system)
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	UPDATE [$(DatabaseName)].[dbo].[ApplicationPersonnel]
	SET [FirstName] = inserted.FirstName
           ,[LastName] = inserted.LastName
           ,[OrganizationName] = inserted.orgname
           ,[Source] = inserted.coisource
	FROM inserted INNER JOIN 
	deleted ON inserted.coi_id = deleted.coi_id INNER JOIN
	[$(DatabaseName)].[dbo].[Application] app ON inserted.LOG_NO = app.LogNumber INNER JOIN
	[$(P2RMIS)].dbo.PRO_COI_Type procoitype ON inserted.coitypeid = procoitype.coitypeid INNER JOIN 
	[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId 
	AND procoitype.coitypeid = capt.ExternalPersonnelTypeId INNER JOIN
	[$(DatabaseName)].[dbo].[ApplicationPersonnel] ApplicationPersonnel ON app.ApplicationId = ApplicationPersonnel.ApplicationId AND
	capt.ClientApplicationPersonnelTypeId = ApplicationPersonnel.ClientApplicationPersonnelTypeId
	AND deleted.FirstName = ApplicationPersonnel.FirstName AND
	deleted.LastName = ApplicationPersonnel.LastName
	WHERE ApplicationPersonnel.DeletedFlag = 0
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
	--Check if COI type exists for client already, if not create it (only works for single inserts)
	IF NOT EXISTS (SELECT * FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(P2RMIS)].dbo.PRO_COI_Type procoitype ON inserted.coitypeid = procoitype.coitypeid INNER JOIN 
		[$(DatabaseName)].[dbo].ProgramMechanism ProgramMechanism ON Application.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].ClientAwardType ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] capt ON ClientAwardType.ClientId = capt.ClientId AND 
		procoitype.coitypeid = capt.ExternalPersonnelTypeId)
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[ClientApplicationPersonnelType]
	([ClientId]
           ,[ApplicationPersonnelType]
           ,[CoiFlag]
		   ,[ExternalPersonnelTypeId])
	SELECT ClientAwardType.ClientId, procoitype.coitype, 1, procoitype.coitypeid
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(P2RMIS)].dbo.PRO_COI_Type procoitype ON inserted.coitypeid = procoitype.coitypeid INNER JOIN 
		[$(DatabaseName)].[dbo].ProgramMechanism ProgramMechanism ON Application.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].ClientAwardType ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId
	END
	INSERT INTO [$(DatabaseName)].[dbo].[ApplicationPersonnel]
           ([ApplicationId]
           ,[ClientApplicationPersonnelTypeId]
           ,[FirstName]
           ,[LastName]
           ,[MiddleInitial]
           ,[OrganizationName]
           ,[PhoneNumber]
           ,[EmailAddress]
           ,[PrimaryFlag]
           ,[Source]
		   ,[CreatedBy]
		   ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, procoi.FirstName, procoi.LASTNAME, NULL, procoi.orgname, 
		procoi.phone, procoi.email, 0, procoi.coisource, app.ModifiedBy, procoi.datetimestamp, app.ModifiedBy, procoi.datetimestamp
	FROM 	inserted procoi  INNER JOIN 
		[$(P2RMIS)].dbo.PRO_COI_Type procoitype ON procoi.coitypeid = procoitype.coitypeid INNER JOIN 
		[$(DatabaseName)].[dbo].[Application] app ON procoi.LOG_NO = app.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId 
			AND procoitype.coitypeid = capt.ExternalPersonnelTypeId
	END
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[ApplicationPersonnel]
	SET DeletedFlag =1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN 
	[$(DatabaseName)].[dbo].[Application] app ON deleted.LOG_NO = app.LogNumber INNER JOIN
	[$(P2RMIS)].dbo.PRO_COI_Type procoitype ON deleted.coitypeid = procoitype.coitypeid INNER JOIN 
	[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId 
	AND procoitype.coitype = capt.ApplicationPersonnelType INNER JOIN
	[$(DatabaseName)].[dbo].[ApplicationPersonnel] ApplicationPersonnel ON app.ApplicationId = ApplicationPersonnel.ApplicationId AND
	capt.ClientApplicationPersonnelTypeId = ApplicationPersonnel.ClientApplicationPersonnelTypeId
	WHERE ApplicationPersonnel.DeletedFlag = 0
END

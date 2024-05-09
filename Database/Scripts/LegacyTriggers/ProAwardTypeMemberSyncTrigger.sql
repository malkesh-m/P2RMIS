CREATE TRIGGER [ProAwardTypeMemberSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRO_Award_Type_Member]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[ProgramMechanism]
	SET ReceiptDeadline = inserted.Rec_Deadline, BlindedFlag = CASE WHEN inserted.reviewTypeID IS NULL THEN 0 ELSE 1 END, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE, FundingOpportunityId = inserted.Opportunity_ID
	FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[ProgramMechanism] ProgramMechanism ON inserted.ATM_ID = ProgramMechanism.LegacyAtmId LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
	END
	--INSERT (works for single insert only)
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
	--First check for ClientAwardType and insert if doesn't exist
	IF NOT EXISTS (Select * 
					From [$(DatabaseName)].[dbo].[ClientAwardType] ClientAwardType INNER JOIN
					[$(DatabaseName)].[dbo].[Client] Client ON ClientAwardType.ClientId = Client.ClientId INNER JOIN
					[$(P2RMIS)].[dbo].[PRG_Program_LU] PRG_Program_LU ON Client.ClientAbrv = PRG_Program_LU.CLIENT INNER JOIN
					[$(P2RMIS)].[dbo].[PRG_Program] PRG_Program ON PRG_Program_LU.PROGRAM = PRG_Program.PROGRAM INNER JOIN
					[$(P2RMIS)].[dbo].[PRG_Program_PA] PRG_Program_PA ON PRG_Program.PRG_ID = PRG_Program_PA.PRG_ID INNER JOIN
					inserted ON PRG_Program_PA.PA_ID = inserted.PA_ID
					WHERE ClientAwardType.LegacyAwardTypeId = inserted.Award_Type)
	BEGIN
		INSERT INTO [$(DatabaseName)].[dbo].[ClientAwardType]
			([ClientId]
           ,[LegacyAwardTypeId]
           ,[AwardAbbreviation]
           ,[AwardDescription]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT ClientProgram.ClientId, inserted.Award_Type, PRO_Award_Type.Short_Desc, PRO_Award_Type.Description, 
		VUN.UserId, PRO_Award_Type.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(P2RMIS)].[dbo].[PRO_Award_Type] PRO_Award_Type ON inserted.Award_Type = PRO_Award_Type.AWARD_TYPE INNER JOIN
		[$(P2RMIS)].dbo.PRG_Program_PA PRG_Program_PA ON inserted.PA_ID = PRG_Program_PA.PA_ID INNER JOIN
		[$(DatabaseName)].dbo.ProgramYear ProgramYear ON PRG_Program_PA.PRG_ID = ProgramYear.LegacyProgramId INNER JOIN
		[$(DatabaseName)].dbo.ClientProgram ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
	END
	
	INSERT INTO [$(DatabaseName)].[dbo].[ProgramMechanism]
           ([ProgramYearId]
           ,[ClientAwardTypeId]
           ,[ReceiptCycle]
           ,[LegacyAtmId]
           ,[ReceiptDeadline]
		   ,[BlindedFlag]
		   ,[FundingOpportunityId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT ProgramYear.ProgramYearId, ClientAwardType.ClientAwardTypeId, PRG_Program_PA.Receipt_Cycle, inserted.ATM_ID,
	inserted.Rec_Deadline, CASE WHEN inserted.reviewTypeID IS NULL THEN 0 ELSE 1 END, inserted.Opportunity_ID, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN 
	[$(P2RMIS)].[dbo].[PRG_Program_PA] PRG_Program_PA ON inserted.PA_ID = PRG_Program_PA.PA_ID INNER JOIN 
	[$(DatabaseName)].[dbo].[ProgramYear] ProgramYear ON PRG_Program_PA.PRG_ID = ProgramYear.LegacyProgramId INNER JOIN
	[$(DatabaseName)].[dbo].[ClientProgram] ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
	[$(DatabaseName)].[dbo].[ClientAwardType] ClientAwardType ON inserted.Award_Type = ClientAwardType.LegacyAwardTypeId AND
		ClientProgram.ClientId = ClientAwardType.ClientId LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName

	END
	--DELETE
	ELSE
	BEGIN
		UPDATE [$(DatabaseName)].[dbo].[ProgramMechanism]
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].[ProgramMechanism] ProgramMechanism ON deleted.ATM_ID = ProgramMechanism.LegacyAtmId 
		WHERE ProgramMechanism.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[MechanismTemplate]
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].[ProgramMechanism] ProgramMechanism ON deleted.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
		[$(DatabaseName)].[dbo].[MechanismTemplate] MechanismTemplate ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId
		WHERE MechanismTemplate.DeletedFlag = 0
	END
END

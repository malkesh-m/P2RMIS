
CREATE TRIGGER [ProProposalSyncTrigger]
ON [$(P2RMIS)].[dbo].PRO_Proposal
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE (this is possibly incomplete, as PI/Admin/etc added late won't be added)(Pushpa says this doesn't usually happen or matter)
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
		UPDATE [$(DatabaseName)].[dbo].[Application]
		SET ProgramMechanismId = pm.ProgramMechanismId, ApplicationTitle = inserted.Proposal_Title,
		ResearchArea = ra.Description, Keywords = inserted.Keywords, ProjectStartDate = CASE WHEN [Project_Start_Date] < '01/01/1900' OR [Project_Start_Date] > '06/06/2079' THEN NULL Else Project_Start_Date END,
		ProjectEndDate = CASE WHEN [Project_End_Date] < '01/01/1900' OR [Project_End_Date] > '06/06/2079' THEN NULL Else Project_End_Date END,
		WithdrawnFlag = CASE WHEN inserted.WITHDRAWN_DATE IS NOT NULL THEN 1 ELSE 0 END, WithdrawnBy = vun2.UserId, WithdrawnDate = inserted.WITHDRAWN_DATE,
		ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
		FROM inserted 
		INNER JOIN [$(DatabaseName)].[dbo].[Application] app ON inserted.Log_No = app.LogNumber
		INNER JOIN [$(P2RMIS)].dbo.PRG_Program_PA ppa ON inserted.PA_ID = ppa.PA_ID
			INNER JOIN [$(P2RMIS)].dbo.PRG_Program program ON ppa.PRG_ID = program.PRG_ID
			INNER JOIN [$(DatabaseName)].[dbo].ProgramYear py ON program.PRG_ID = py.LegacyProgramId
			INNER JOIN [$(DatabaseName)].[dbo].ClientProgram cp ON py.ClientProgramId = cp.ClientProgramId
			INNER JOIN [$(DatabaseName)].[dbo].ClientAwardType ca ON cp.ClientId = ca.ClientId AND inserted.AWARD_TYPE = ca.LegacyAwardTypeId
			INNER JOIN [$(P2RMIS)].dbo.PRO_Award_Type_Member atm ON inserted.PA_ID = atm.PA_ID AND inserted.Award_Type = atm.Award_Type
			INNER JOIN [$(DatabaseName)].[dbo].ProgramMechanism pm ON atm.ATM_ID = pm.LegacyAtmId 
			LEFT OUTER JOIN [$(P2RMIS)].dbo.PRO_Research_Area ra ON inserted.RESEARCH_AREA = ra.RESEARCH_AREA
			LEFT OUTER JOIN [$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun ON inserted.LAST_UPDATED_BY = vun.UserName
			LEFT OUTER JOIN [$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun2 ON inserted.WITHDRAWN_BY = vun2.UserName
		IF ( UPDATE (PI_Org_Name) OR UPDATE (PI_First_Name) OR UPDATE (PI_Last_Name))
		UPDATE [$(DatabaseName)].[dbo].[ApplicationPersonnel]
		SET FirstName = inserted.PI_First_Name, LastName = inserted.PI_Last_Name, OrganizationName = inserted.PI_Org_Name,
		ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE, StateAbbreviation = inserted.PI_State
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationPersonnel] ApplicationPersonnel ON Application.ApplicationId = ApplicationPersonnel.ApplicationId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] Capt ON ApplicationPersonnel.ClientApplicationPersonnelTypeId = Capt.ClientApplicationPersonnelTypeId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		WHERE Capt.ApplicationPersonnelTypeAbbreviation = 'PI'
		IF ( UPDATE (PI2_First_Name) OR UPDATE (PI2_Last_Name))
		UPDATE [$(DatabaseName)].[dbo].[ApplicationPersonnel]
		SET FirstName = inserted.PI2_First_Name, LastName = inserted.PI2_Last_Name,
		ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationPersonnel] ApplicationPersonnel ON Application.ApplicationId = ApplicationPersonnel.ApplicationId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] Capt ON ApplicationPersonnel.ClientApplicationPersonnelTypeId = Capt.ClientApplicationPersonnelTypeId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		WHERE Capt.ApplicationPersonnelTypeAbbreviation = 'PI2'
		IF ( UPDATE (Admin1_Org_Name) OR UPDATE (Admin1_First_Name) OR UPDATE (Admin1_Last_Name))
		UPDATE [$(DatabaseName)].[dbo].[ApplicationPersonnel]
		SET FirstName = inserted.Admin1_First_Name, LastName = inserted.Admin1_Last_Name, OrganizationName = inserted.Admin1_Org_Name,
		ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE, StateAbbreviation = inserted.Admin1_State
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationPersonnel] ApplicationPersonnel ON Application.ApplicationId = ApplicationPersonnel.ApplicationId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] Capt ON ApplicationPersonnel.ClientApplicationPersonnelTypeId = Capt.ClientApplicationPersonnelTypeId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		WHERE Capt.ApplicationPersonnelTypeAbbreviation = 'Admin-1'
		IF ( UPDATE (Admin2_First_Name) OR UPDATE (Admin2_Last_Name))
		UPDATE [$(DatabaseName)].[dbo].[ApplicationPersonnel]
		SET FirstName = inserted.Admin2_First_Name, LastName = inserted.Admin2_Last_Name,
		ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationPersonnel] ApplicationPersonnel ON Application.ApplicationId = ApplicationPersonnel.ApplicationId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] Capt ON ApplicationPersonnel.ClientApplicationPersonnelTypeId = Capt.ClientApplicationPersonnelTypeId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		WHERE Capt.ApplicationPersonnelTypeAbbreviation = 'Admin-2'
		IF ( UPDATE (Admin3_First_Name) OR UPDATE (Admin3_Last_Name))
		UPDATE [$(DatabaseName)].[dbo].[ApplicationPersonnel]
		SET FirstName = inserted.Admin3_First_Name, LastName = inserted.Admin3_Last_Name,
		ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationPersonnel] ApplicationPersonnel ON Application.ApplicationId = ApplicationPersonnel.ApplicationId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] Capt ON ApplicationPersonnel.ClientApplicationPersonnelTypeId = Capt.ClientApplicationPersonnelTypeId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		WHERE Capt.ApplicationPersonnelTypeAbbreviation = 'Admin-3'
		IF ( UPDATE (Mentor_First_Name) OR UPDATE (Mentor_Last_Name))
		UPDATE [$(DatabaseName)].[dbo].[ApplicationPersonnel]
		SET FirstName = inserted.Mentor_First_Name, LastName = inserted.Mentor_Last_Name,
		ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationPersonnel] ApplicationPersonnel ON Application.ApplicationId = ApplicationPersonnel.ApplicationId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] Capt ON ApplicationPersonnel.ClientApplicationPersonnelTypeId = Capt.ClientApplicationPersonnelTypeId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		WHERE Capt.ApplicationPersonnelTypeAbbreviation = 'Mentor'
		IF ( UPDATE (Grant_ID))
		UPDATE [$(DatabaseName)].[dbo].[ApplicationInfo]
		SET InfoText = inserted.Grant_ID
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationInfo] ApplicationInfo ON Application.ApplicationId = ApplicationInfo.ApplicationId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		WHERE ApplicationInfo.ClientApplicationInfoTypeId = 1
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[Application]
           ([ProgramMechanismId]
           ,[ParentApplicationId]
           ,[LogNumber]
           ,[ApplicationTitle]
           ,[ResearchArea]
           ,[Keywords]
           ,[ProjectStartDate]
           ,[ProjectEndDate]
		   ,[WithdrawnFlag]
		   ,[WithdrawnBy]
		   ,[WithdrawnDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT	ProgramMechanismId = pm.ProgramMechanismId, ParentApplicationId = app2.ApplicationId, LogNumber = pp.Log_No, ApplicationTitle = pp.Proposal_Title, ResearchArea = ra.Description, 
		Keywords = pp.Keywords, ProjectStartDate = CASE WHEN [Project_Start_Date] < '01/01/1900' OR [Project_Start_Date] > '06/06/2079' THEN NULL Else Project_Start_Date END,
		ProjectEndDate = CASE WHEN [Project_End_Date] < '01/01/1900' OR [Project_End_Date] > '06/06/2079' THEN NULL Else Project_End_Date END, 
		WithdrawnFlag = CASE WHEN pp.WITHDRAWN_DATE IS NOT NULL THEN 1 ELSE 0 END, WithdrawnBy = vun2.UserId, WithdrawnDate = pp.WITHDRAWN_DATE,
		CreatedBy = VUN.UserId, CreatedDate = dbo.GetP2rmisDateTime(),
		ModifiedBy = vun.UserID, ModifiedDate = pp.LAST_UPDATE_DATE
		FROM inserted pp
			INNER JOIN [$(P2RMIS)].dbo.PRG_Program_PA ppa ON pp.PA_ID = ppa.PA_ID
			INNER JOIN [$(P2RMIS)].dbo.PRG_Program program ON ppa.PRG_ID = program.PRG_ID
			INNER JOIN [$(DatabaseName)].[dbo].ProgramYear py ON program.PRG_ID = py.LegacyProgramId
			INNER JOIN [$(DatabaseName)].[dbo].ClientProgram cp ON py.ClientProgramId = cp.ClientProgramId
			INNER JOIN [$(DatabaseName)].[dbo].ClientAwardType ca ON cp.ClientId = ca.ClientId AND pp.AWARD_TYPE = ca.LegacyAwardTypeId
			INNER JOIN [$(P2RMIS)].dbo.PRO_Award_Type_Member atm ON pp.PA_ID = atm.PA_ID AND pp.Award_Type = atm.Award_Type
			INNER JOIN [$(DatabaseName)].[dbo].ProgramMechanism pm ON atm.ATM_ID = pm.LegacyAtmId 
			LEFT OUTER JOIN [$(P2RMIS)].dbo.PRO_Research_Area ra ON pp.RESEARCH_AREA = ra.RESEARCH_AREA
			LEFT OUTER JOIN [$(DatabaseName)].[dbo].Application app2 ON SUBSTRING(pp.Log_No, 1, LEN(pp.Log_No) - 2) = app2.LogNumber AND
			SUBSTRING(pp.Log_No, LEN(pp.Log_No) - 1, 2) LIKE 'P%' AND
			SUBSTRING(pp.Log_No, LEN(pp.Log_No) - 1, 2) <> 'PP'
			LEFT OUTER JOIN [$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun ON pp.LAST_UPDATED_BY = vun.UserName
			LEFT OUTER JOIN [$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun2 ON pp.WITHDRAWN_BY = vun2.UserName
		--Add application personnel as necessary
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
           ,[ModifiedDate]
		   ,[StateAbbreviation])
		--PI Information
		SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.PI_First_Name, pro.PI_LAST_NAME, pro.PI_MIDDLE_INITIAL, pro.PI_ORG_NAME, 
		pro.PI_PHONE_NUMBER, pro.PI_EMAIL, 1, NULL, app.ModifiedBy, app.ModifiedDate, app.ModifiedBy, app.ModifiedDate, pro.PI_State
		FROM inserted pro INNER JOIN
			[$(DatabaseName)].[dbo].[Application] app ON pro.LOG_NO = app.LogNumber INNER JOIN
			[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
			[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
			[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'PI'
		WHERE pro.PI_LAST_NAME IS NOT NULL OR pro.PI_ORG_NAME IS NOT NULL
		UNION ALL
		--PI2 Information
		SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.PI2_First_Name, pro.PI2_LAST_NAME, pro.PI2_MIDDLE_INITIAL, NULL, 
		NULL, pro.PI2_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate, app.ModifiedBy, app.ModifiedDate, NULL
		FROM inserted pro INNER JOIN
			[$(DatabaseName)].[dbo].[Application] app ON pro.LOG_NO = app.LogNumber INNER JOIN
			[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
			[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
			[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'PI2'
		WHERE pro.PI2_LAST_NAME IS NOT NULL
		UNION ALL
		--Admin1 Information
		SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.admin1_First_Name, pro.admin1_LAST_NAME, pro.admin1_MIDDLE_INITIAL, pro.admin1_ORG_NAME, 
		pro.admin1_phone_number, pro.admin1_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate, app.ModifiedBy, app.ModifiedDate, pro.Admin1_State
		FROM inserted pro INNER JOIN
			[$(DatabaseName)].[dbo].[Application] app ON pro.LOG_NO = app.LogNumber INNER JOIN
			[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
			[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
			[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'admin-1'
		WHERE pro.admin1_LAST_NAME IS NOT NULL OR pro.admin1_ORG_NAME IS NOT NULL
		UNION ALL
		--admin2 Information
		SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.admin2_First_Name, pro.admin2_LAST_NAME, pro.admin2_MIDDLE_INITIAL, NULL, 
		pro.admin2_phone_number, pro.admin2_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate, app.ModifiedBy, app.ModifiedDate, NULL
		FROM inserted pro INNER JOIN
			[$(DatabaseName)].[dbo].[Application] app ON pro.LOG_NO = app.LogNumber INNER JOIN
			[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
			[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
			[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'admin-2'
		WHERE pro.admin2_LAST_NAME IS NOT NULL
		UNION ALL
		--admin3 Information
		SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.admin3_First_Name, pro.admin3_LAST_NAME, pro.admin3_MIDDLE_INITIAL, NULL, 
		pro.admin3_phone_number, pro.admin3_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate, app.ModifiedBy, app.ModifiedDate, NULL
		FROM inserted pro INNER JOIN
			[$(DatabaseName)].[dbo].[Application] app ON pro.LOG_NO = app.LogNumber INNER JOIN
			[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
			[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
			[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'admin-3'
		WHERE pro.admin3_LAST_NAME IS NOT NULL 
		UNION ALL
		--Mentor Information
		SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.mentor_First_Name, pro.mentor_LAST_NAME, pro.mentor_MIDDLE_INITIAL, NULL, 
		NULL, NULL, 0, NULL, app.ModifiedBy, app.ModifiedDate, app.ModifiedBy, app.ModifiedDate, NULL
		FROM inserted pro INNER JOIN
			[$(DatabaseName)].[dbo].[Application] app ON pro.LOG_NO = app.LogNumber INNER JOIN
			[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
			[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
			[$(DatabaseName)].[dbo].[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'mentor'
		WHERE pro.mentor_LAST_NAME IS NOT NULL
		--Add info for Grant_ID (and possibly others later on)
		INSERT INTO [$(DatabaseName)].[dbo].[ApplicationInfo]
           ([ApplicationId]
           ,[ClientApplicationInfoTypeId]
           ,[InfoText]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT app.ApplicationId, 1, prop.Grant_ID, vun.UserId, prop.LAST_UPDATE_DATE, vun.UserId, prop.LAST_UPDATE_DATE
		FROM inserted prop INNER JOIN
		[$(DatabaseName)].[dbo].[Application] app ON prop.LOG_NO = app.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].[ViewLegacyUserNameToUserId] vun ON prop.LAST_UPDATED_BY = vun.UserName
		WHERE Grant_ID IS NOT NULL AND cat.ClientId = 19
	END
	--DELETE
	ELSE
	BEGIN
		UPDATE [$(DatabaseName)].[dbo].[ApplicationInfo]
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON deleted.LOG_NO = Application.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationInfo] ApplicationInfo ON Application.ApplicationId = ApplicationInfo.ApplicationId
		WHERE ApplicationInfo.DeletedFlag = 0
		
		UPDATE [$(DatabaseName)].[dbo].[ApplicationPersonnel]
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON deleted.LOG_NO = Application.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationPersonnel] ApplicationPersonnel ON Application.ApplicationId = ApplicationPersonnel.ApplicationId
		WHERE ApplicationPersonnel.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[ApplicationCompliance]
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON deleted.LOG_NO = Application.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ApplicationCompliance] ApplicationCompliance ON Application.ApplicationId = ApplicationCompliance.ApplicationId

		UPDATE [$(DatabaseName)].[dbo].[Application]
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].[Application] Application ON deleted.LOG_NO = Application.LogNumber
		WHERE [Application].DeletedFlag = 0
	END
END

--First update existing application information
UPDATE a
SET ProgramMechanismId = pm.ProgramMechanismId, ApplicationTitle = pp.Proposal_Title, ResearchArea = ra.Description, 
Keywords = pp.Keywords, ProjectStartDate = pp.Project_Start_Date, ProjectEndDate = pp.Project_End_Date, CreatedDate = ar.Receipt_Date,
ModifiedBy = vun.UserID, ModifiedDate = pp.LAST_UPDATE_DATE
FROM [Application] a 
	INNER JOIN [$(P2RMIS)].dbo.PRO_Proposal pp ON a.LogNumber = pp.LOG_NO 
	INNER JOIN [$(P2RMIS)].dbo.PRG_Program_PA ppa ON pp.PA_ID = ppa.PA_ID
	INNER JOIN [$(P2RMIS)].dbo.PRG_Program program ON ppa.PRG_ID = program.PRG_ID
	INNER JOIN ProgramYear py ON program.PRG_ID = py.LegacyProgramId
	INNER JOIN ClientProgram cp ON py.ClientProgramId = cp.ClientProgramId
	INNER JOIN ClientAwardType ca ON cp.ClientId = ca.ClientId AND pp.AWARD_TYPE = ca.LegacyAwardTypeId
	INNER JOIN [$(P2RMIS)].dbo.PRO_Award_Type_Member atm ON pp.PA_ID = atm.PA_ID AND pp.Award_Type = atm.Award_Type
	INNER JOIN ProgramMechanism pm ON atm.ATM_ID = pm.LegacyAtmId 
	LEFT OUTER JOIN [$(P2RMIS)].dbo.PRO_Proposal_Receipt ar ON pp.LOG_NO = ar.Log_No 
	LEFT OUTER JOIN [$(P2RMIS)].dbo.PRO_Research_Area ra ON pp.RESEARCH_AREA = ra.RESEARCH_AREA
	LEFT OUTER JOIN ViewLegacyUserNameToUserId vun ON pp.LAST_UPDATED_BY = vun.UserName
--this keeps update from running every time
WHERE a.ApplicationTitle IS NULL


--Insert existing records
INSERT INTO [Application] ([LogNumber]
		   ,[ProgramMechanismId]
           ,[ApplicationTitle]
           ,[ResearchArea]
           ,[Keywords]
           ,[ProjectStartDate]
           ,[ProjectEndDate]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT	LogNumber = pp.Log_No, ProgramMechanismId = pm.ProgramMechanismId, ApplicationTitle = pp.Proposal_Title, ResearchArea = ra.Description, 
Keywords = pp.Keywords, ProjectStartDate = CASE WHEN [Project_Start_Date] < '01/01/1900' OR [Project_Start_Date] > '06/06/2079' THEN NULL Else Project_Start_Date END,
ProjectEndDate = CASE WHEN [Project_End_Date] < '01/01/1900' OR [Project_End_Date] > '06/06/2079' THEN NULL Else Project_End_Date END, CreatedDate = ar.Receipt_Date,
ModifiedBy = vun.UserID, ModifiedDate = pp.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRO_Proposal pp
	INNER JOIN [$(P2RMIS)].dbo.PRG_Program_PA ppa ON pp.PA_ID = ppa.PA_ID
	INNER JOIN [$(P2RMIS)].dbo.PRG_Program program ON ppa.PRG_ID = program.PRG_ID
	INNER JOIN ProgramYear py ON program.PRG_ID = py.LegacyProgramId
	INNER JOIN ClientProgram cp ON py.ClientProgramId = cp.ClientProgramId
	INNER JOIN ClientAwardType ca ON cp.ClientId = ca.ClientId AND pp.AWARD_TYPE = ca.LegacyAwardTypeId
	INNER JOIN [$(P2RMIS)].dbo.PRO_Award_Type_Member atm ON pp.PA_ID = atm.PA_ID AND pp.Award_Type = atm.Award_Type
	INNER JOIN ProgramMechanism pm ON atm.ATM_ID = pm.LegacyAtmId 
	LEFT OUTER JOIN [$(P2RMIS)].dbo.PRO_Proposal_Receipt ar ON pp.LOG_NO = ar.Log_No 
	LEFT OUTER JOIN [$(P2RMIS)].dbo.PRO_Research_Area ra ON pp.RESEARCH_AREA = ra.RESEARCH_AREA
	LEFT OUTER JOIN ViewLegacyUserNameToUserId vun ON pp.LAST_UPDATED_BY = vun.UserName
WHERE pp.LOG_NO NOT IN (Select LogNumber FROM [ViewApplication])

--Update Parent/child data for partnering (can probably do others here as well)
Update a
SET ParentApplicationId = a2.ApplicationId
FROM [Application] a 
	INNER JOIN [Application] a2 ON SUBSTRING(a.LogNumber, 1, LEN(a.LogNumber) - 2) = a2.LogNumber
WHERE SUBSTRING(a.LogNumber, LEN(a.LogNumber) - 1, 2) LIKE 'P%' AND
	SUBSTRING(a.LogNumber, LEN(a.LogNumber) - 1, 2) <> 'PP' AND
	a.ParentApplicationId IS NULL

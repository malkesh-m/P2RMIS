/*
As necessary, first delete existing bad data - see Miscellaneous/TemporaryDeleteCascade.sql
DELETE
FROM         TrainingDocumentAccess
WHERE TrainingDocumentId IN (SELECT TrainingDocument.TrainingDocumentId
FROM TrainingDocument
 INNER JOIN
                      TrainingDocumentAccess ON TrainingDocument.TrainingDocumentId = TrainingDocumentAccess.TrainingDocumentId INNER JOIN
                      ProgramYear ON TrainingDocument.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
                      ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId
WHERE     (ClientProgram.ClientProgramId = 75) OR
                      (ClientProgram.ClientProgramId = 85));

DELETE FROM ClientProgram WHERE ClientProgramId = 75;
DELETE FROM ClientProgram WHERE ClientProgramId = 85;
*/
 EXEC sp_msforeachtable 'ALTER TABLE ? DISABLE TRIGGER all'
 GO

DECLARE @ClientId INT = 23;
GO
--ClientProgram
 INSERT INTO [dbo].[ClientProgram] 
      ([LegacyProgramId]
      ,[ClientId]
      ,[ProgramAbbreviation]
      ,[ProgramDescription]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate])
 
  SELECT ppl.[PROGRAM] LegacyProgramID
		,c.[clientid] ClientID
		,ppl.[PROGRAM] ProgramAbbrev
      ,ppl.[DESCRIPTION]
      --,ppl.[CLIENT]
	  ,v.[userid] ModifiedBy
      ,ppl.[LAST_UPDATE_DATE] ModifiedDate
      ,v.[userid] ModifiedBy
      ,ppl.[LAST_UPDATE_DATE] ModifiedDate
      --,ppl.[LAST_UPDATED_BY]
      --,ppl.[BS_Class]
      --,ppl.[W9_Info]
	FROM [$(P2RMIS)].[dbo].[PRG_Program_LU] ppl
	inner join [dbo].[Client] C on ppl.[Client] = c.[CLIENTAbrv]
	left join [dbo].[ViewLegacyUserNameToUserId] v on ppl.[LAST_UPDATED_BY] = v.[username]
	
	-- do not add rows that are already present
	where ppl.[PROGRAM] not in 
	(select [LegacyProgramId]
	from [dbo].[ClientProgram] WHERE [LegacyProgramId] IS NOT NULL) and c.ClientId = @ClientId;
--ClientMeeting
	INSERT INTO [dbo].[ClientMeeting] (
	-- auto generated
		--[ClientMeetingId]
		  [LegacyMeetingId]
		  ,[ClientId]
		  ,[MeetingAbbreviation]
		  ,[MeetingDescription]
		  ,[StartDate]
		  ,[EndDate]
		  ,[MeetingLocation]
		  ,[MeetingTypeId]
		 -- ,[CreatedBy]
		 -- ,[CreatedDate]
		  ,[ModifiedBy]
		  ,[ModifiedDate])
		  /*
		  Meetings without any assigned panels not included
		  */
		  SELECT DISTINCT
		  mm.[Meeting_ID] LegacyMtgID
		  --,pl.[client] ClientFromProgramLU
		  ,c.[clientid] ClientID
		  ,mm.[Meeting_ID] MtgAbbrev
		  ,mm.[Meeting_Desc] MeetingDesc
		  ,mm.[Start_Date] StartDate
		  ,mm.[End_Date] EndDate
		  ,mm.[Meeting_Location] MtgLoc
		  ,ISNULL(mt.[MeetingTypeId], 1) MtgTypeID
		  ,v.[userid] ModBy
		  ,mm.[LAST_UPDATE_DATE] ModDate
	  FROM [$(P2RMIS)].[dbo].[MTG_Master] mm
		inner join [$(P2RMIS)].[dbo].[MTG_Session] mp on mm.[Meeting_ID] = mp.[Meeting_ID]
		inner join [$(P2RMIS)].[dbo].[PAN_Master] pan on mp.Session_ID = pan.Session_ID
		inner join [$(P2RMIS)].[dbo].[PRG_Program_LU] pl on pan.[program] = pl.[program]
		inner join [dbo].[Client] c on pl.[client] = c.[clientAbrv] 
		left join [dbo].[ViewLegacyUserNameToUserId] v on mm.[LAST_UPDATED_BY] = v.[username]
		left join [dbo].[MeetingType] mt on mm.[review_type] = mt.[LegacyMeetingTypeId]
	  WHERE c.ClientId = @ClientId AND
	  NOT EXISTS (Select 'X' From ClientMeeting WHERE ClientMeeting.LegacyMeetingId = mm.Meeting_ID AND ClientMeeting.ClientId = c.ClientID)
	--ClientAwardType
	  insert into [dbo].[ClientAwardType]
		([ClientId]
		,[LegacyAwardTypeId]
		,[AwardAbbreviation]
		,[AwardDescription]
		,[ModifiedDate]
		,[ModifiedBy])		
	SELECT DISTINCT c.[clientid]
	,pat.[AWARD_TYPE]
	,pat.[SHORT_DESC]
    ,pat.[DESCRIPTION]
	,pat.[LAST_UPDATE_DATE]
    ,v.[userid]
     --,pat.[AWARD_CATEGORY]
     --,pat.[CENTER]
     --,pat.[REV_TEMP_LOC]
     --,pat.[sort_order]
   FROM [$(P2RMIS)].[dbo].[PRO_Award_Type] PAT
	left join [$(P2RMIS)].[dbo].[PRO_Award_Type_Member] PATM ON PAT.[AWARD_TYPE] = PATM.[AWARD_TYPE]
	left join [$(P2RMIS)].[dbo].[PRG_Program_PA] PP ON patm.[pa_id] = pp.[pa_id]
	left join [$(P2RMIS)].[dbo].[PRG_Program] P on pp.[prg_id] = p.[prg_id]
	left join [$(P2RMIS)].[dbo].[PRG_Program_LU] PL ON  p.[program] = pl.[program]
	left join [dbo].[Client] C on pl.[Client] = c.[CLIENTAbrv]
	left join [dbo].[ViewLegacyUserNameToUserId] v on pat.[LAST_UPDATED_BY] = v.[username]
	
	-- do not add any clients that are not associated with an award type
	where c.[clientid] = @ClientId 
	-- do not add any award data that is already in the new table
	and 
	NOT EXISTS (Select 'X' From ClientAwardType WHERE ClientAwardType.LegacyAwardTypeId = pat.AWARD_TYPE AND ClientAwardType.ClientId = c.ClientID)
	order by pat.[AWARD_TYPE]

--ProgramYear
INSERT INTO [dbo].[ProgramYear]
(     [LegacyProgramId]
      ,[ClientProgramId]
      ,[Year]
      ,[DateClosed]
      ,[ModifiedDate]
      ,[ModifiedBy])
SELECT p.[PRG_ID] LegacyProgramID
      ,cp.[ClientProgramId] ClientProgramID
      ,p.[FY] ProgramYear
      ,p.[CLOSED] DateClosed
      ,p.[LAST_UPDATE_DATE] ModifiedDate
      ,v.[userid] ModifiedBy
  FROM [$(P2RMIS)].[dbo].[PRG_Program] p
    left join [$(P2RMIS)].[dbo].[PRG_Program_LU] pl on p.[program] = pl.[program]
    left join [dbo].[ViewLegacyUserNameToUserId] v on p.[LAST_UPDATED_BY] = v.[username]
    left join [dbo].[ClientProgram] cp on p.[program] = cp.[legacyProgramId]
    where p.[prg_id] not in 
    (select [LegacyProgramId]
    from [dbo].[ProgramYear] WHERE [LegacyProgramId] IS NOT NULL) AND cp.ClientId = @ClientId
    order by p.[prg_id]
--ProgramMechanism
INSERT INTO [dbo].[ProgramMechanism]
(--[ProgramMechanismId]
      [ProgramYearId]
      ,[ClientAwardTypeId]
      ,[ReceiptCycle]
      ,[LegacyAtmId]
      ,[ReceiptDeadline]
      ,[AbstractFormat]
      ,[ModifiedBy]
      ,[ModifiedDate])
 SELECT py.[ProgramYearID] ProgramYearId
	,cat.[ClientAwardTypeId] ClientAwardType
    ,pp.[receipt_cycle] ReceiptCycle
	,ptm.[ATM_ID] LegacyAtmID
	,ptm.[REC_DEADLINE] ReceiptDeadline
	,paf.[abstract_type] AbstractFormat
	,v.[userid] ModifiedBy
    ,ptm.[LAST_UPDATE_DATE] ModifiedDate
  FROM [$(P2RMIS)].[dbo].[PRO_Award_Type_Member] ptm
  left join [dbo].[ViewLegacyUserNameToUserId] v on ptm.[LAST_UPDATED_BY] = v.[username]
  left join [$(P2RMIS)].[dbo].[PRO_Abstract_Format] paf on ptm.[atm_id] = paf.[atm_id]
  left join [$(P2RMIS)].[dbo].[PRG_Program_PA] pp on ptm.[pa_id] = pp.[pa_id]
  left join [dbo].[ProgramYear] py on pp.[prg_id] = py.[LegacyProgramId]
  left join [dbo].[ClientProgram] cp on py.ClientProgramId = cp.ClientProgramId
  inner join [dbo].[ClientAwardType] cat on ptm.[award_type] = cat.[LegacyAwardTypeId] 
  AND cp.ClientId = cat.ClientId
  WHERE NOT EXISTS (Select 'X' FROM ViewProgramMechanism WHERE ProgramYearId = py.ProgramYearId AND ClientAwardTypeId = cat.ClientAwardTypeId) AND cp.ClientId = @ClientId
  order by cat.[ClientAwardTypeId]
--ClientElement
INSERT INTO .[dbo].[ClientElement]
           ([ClientId]
           ,[ElementTypeId]
           ,[ElementIdentifier]
           ,[ElementAbbreviation]
           ,[ElementDescription]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ClientAwardType.ClientId, 1, PRO_Evaluation_Criteria.Eval_Criteria, PRO_Evaluation_Criteria.Eval_Abrv, 
	PRO_Evaluation_Criteria.Description, VUN.UserId, PRO_Evaluation_Criteria.LAST_UPDATE_DATE
FROM	[$(P2RMIS)].dbo.PRO_Evaluation_Criteria INNER JOIN
	[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member ON PRO_Evaluation_Criteria.EVAL_Criteria = PRO_Evaluation_Criteria_Member.EVAL_Criteria INNER JOIN
	ProgramMechanism ON PRO_Evaluation_Criteria_Member.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
	ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId LEFT JOIN
	ViewLegacyUserNameToUserId VUN ON PRO_Evaluation_Criteria.Last_Updated_By = VUN.UserName LEFT JOIN
	ClientElement ON ClientAwardType.ClientId = ClientElement.ClientId AND PRO_Evaluation_Criteria.Eval_Criteria = ClientElement.ElementIdentifier
WHERE ClientElement.ClientElementId IS NULL AND ClientAwardType.ClientId = @ClientId
GROUP BY ClientAwardType.ClientId, PRO_Evaluation_Criteria.Eval_Criteria, PRO_Evaluation_Criteria.Eval_Abrv, 
	PRO_Evaluation_Criteria.Description, VUN.UserId, PRO_Evaluation_Criteria.LAST_UPDATE_DATE;
--MeetingSession
INSERT INTO [dbo].[MeetingSession] 
( --[MeetingSessionId],
      [LegacySessionId]
     ,[ClientMeetingId]
      ,[SessionAbbreviation]
      ,[SessionDescription]
      ,[StartDate]
      ,[EndDate]
     -- ,[CreatedBy]
     -- ,[CreatedDate]
      ,[ModifiedDate]
      ,[ModifiedBy]
 )
SELECT DISTINCT ms.[Session_ID] LegacySessionID
      ,cm.[ClientMeetingId] ClientMeetingID
      ,ms.[Session_ID] SessionAbbrev
      ,ms.[Session_Desc] SessionDesc
      ,ms.[Start_Date] StartDate
      ,ms.[End_Date] EndDate
      --,[critique_due_date]
      --,[final_critique_due_date]
      ,ms.[LAST_UPDATE_DATE]
      --,ms.[LAST_UPDATED_BY]
      --,v.[username]
      ,v.[userid]
     -- ,ms.[Meeting_ID] 
      --,cm.[LegacyMeetingId]
  FROM [$(P2RMIS)].[dbo].[MTG_Session] ms
  inner join [$(P2RMIS)].[dbo].PAN_Master pan on ms.Session_ID = pan.Session_ID
  inner join [$(P2RMIS)].[dbo].PRG_Program_LU prg on pan.Program = prg.Program
  inner join [dbo].Client cl on prg.Client = cl.ClientAbrv
  left join [dbo].[ViewLegacyUserNameToUserId] v on ms.[LAST_UPDATED_BY] = v.[username]
  inner join [dbo].[ClientMeeting] cm on ms.[Meeting_ID] = cm.[LegacyMeetingId] AND cl.ClientId = cm.ClientId
WHERE NOT EXISTS (Select 'X' FROM ViewMeetingSession WHERE LegacySessionId = ms.Session_ID AND ClientMeetingId = cm.ClientMeetingId) AND cl.ClientId = @ClientId;
--SessionPanel
INSERT INTO [dbo].[SessionPanel]
(--[SessionPanelId]
      [LegacyPanelId]
      ,[MeetingSessionId]
      ,[PanelAbbreviation]
      ,[PanelName]
      ,[StartDate]
      ,[EndDate]
      --,[CreatedBy]
     -- ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate])
 SELECT pm.[panel_id] LegacyPanelID
		,ms2.[MeetingSessionID]
		--,ms.[meeting_id]
		--,cm.[LegacyMeetingId] MeetingSessionID
		,pm.[Panel_Abrv] PanelAbbreviation
		,pm.[Panel_Name] PanelName
		--,ms.[Session_Desc]
		,ms2.[StartDate] StartDate
		,ms2.[EndDate] EndDate
		--,ms.[critique_due_date]
		--,ms.[final_critique_due_date]
		,v.[userid] ModifiedBy
		,pm.[LAST_UPDATE_DATE] ModifiedDate
		--,ms.session_id
		--,pm.session_id
		--,ms.meeting_id
  FROM  [$(P2RMIS)].[dbo].[PAN_Master] pm 
  inner join [$(P2RMIS)].[dbo].[PRG_Program_LU] prg on pm.Program = prg.Program
  inner join [dbo].[Client] cl on prg.Client = cl.ClientAbrv
  left join [dbo].[MeetingSession] ms2 on pm.[session_id] = ms2.[LegacySessionId]
  left join [dbo].[ClientMeeting] cm on cl.ClientId = cm.ClientId AND cm.ClientMeetingId = ms2.ClientMeetingId
  --for legacy purposes some panels did not have a session, but we want to keep record of participation regardless
  left join [dbo].[ViewLegacyUserNameToUserId] v on pm.[LAST_UPDATED_BY] = v.[username]
  WHERE NOT EXISTS (Select 'X' FROM ViewSessionPanel WHERE LegacyPanelId = pm.Panel_ID AND MeetingSessionId = ms2.MeetingSessionId) AND ms2.MeetingSessionId IS NOT NULL AND cl.ClientId = @ClientId;
--ProgramPanel
INSERT INTO [dbo].[ProgramPanel] (
   [ProgramYearId]
  ,[SessionPanelId]
  ,[ModifiedBy]
  ,[ModifiedDate]
 )
 SELECT py.[ProgramYearId] ProgramYearID
  ,sp.[SessionPanelId] SessionPanelID
  ,py.[ModifiedBy] ModifiedBy
  ,py.[ModifiedDate] ModifiedDate      
 FROM [SessionPanel] sp
 INNER JOIN [$(P2RMIS)].dbo.PAN_Master pm ON sp.LegacyPanelId = pm.Panel_Id
 INNER JOIN [$(P2RMIS)].dbo.PRG_Program_LU prg ON pm.Program = prg.Program
 INNER JOIN Client ON prg.Client = Client.ClientAbrv
 INNER JOIN ClientProgram cp ON prg.Program = cp.LegacyProgramId AND Client.ClientId = cp.ClientId
 INNER JOIN ProgramYear py ON cp.ClientProgramId = py.ClientProgramId AND pm.FY = py.Year 
 LEFT JOIN [MeetingSession] ms ON sp.MeetingSessionId = ms.MeetingSessionId
 LEFT JOIN [ClientMeeting] cm ON ms.ClientMeetingId = cm.ClientMeetingId AND cm.ClientId = Client.ClientID
 WHERE NOT EXISTS (Select 'X' FROM ViewProgramPanel WHERE ProgramYearId = py.ProgramYearId AND SessionPanelId = sp.SessionPanelId) AND Client.ClientId = @ClientId;
--ProgramMeeting
INSERT INTO [dbo].[ProgramMeeting]
           ([ProgramYearId]
           ,[ClientMeetingId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT DISTINCT ProgramYear.ProgramYearId, ClientMeeting.ClientMeetingId, ClientMeeting.CreatedBy, ClientMeeting.CreatedDate, ClientMeeting.ModifiedBy, ClientMeeting.ModifiedDate
FROM ViewClientMeeting ClientMeeting INNER JOIN
ViewMeetingSession MeetingSession ON ClientMeeting.ClientMeetingId = MeetingSession.ClientMeetingId INNER JOIN
ViewSessionPanel SessionPanel ON MeetingSession.MeetingSessionId = SessionPanel.MeetingSessionId INNER JOIN
ViewProgramPanel ProgramPanel ON SessionPanel.SessionPanelId = ProgramPanel.SessionPanelId INNER JOIN
ViewProgramYear ProgramYear ON ProgramPanel.ProgramYearId = ProgramYear.ProgramYearId
WHERE NOT EXISTS (Select 'X' FROM ProgramMeeting WHERE DeletedFlag = 0 AND ProgramMeeting.ClientMeetingId = ClientMeeting.ClientMeetingId AND ProgramMeeting.ProgramYearId = ProgramYear.ProgramYearId) AND ClientMeeting.ClientId = @ClientId;
--Application
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
WHERE pp.LOG_NO NOT IN (Select LogNumber FROM [ViewApplication]) AND cp.ClientId = @ClientId;
--Update Parent/child data for partnering (can probably do others here as well)
Update a
SET ParentApplicationId = a2.ApplicationId
FROM [Application] a 
	INNER JOIN [Application] a2 ON SUBSTRING(a.LogNumber, 1, LEN(a.LogNumber) - 2) = a2.LogNumber
WHERE SUBSTRING(a.LogNumber, LEN(a.LogNumber) - 1, 2) LIKE 'P%' AND
	SUBSTRING(a.LogNumber, LEN(a.LogNumber) - 1, 2) <> 'PP' AND
	a.ParentApplicationId IS NULL
--ApplicationBudget
INSERT INTO [ApplicationBudget]
           ([ApplicationId]
           ,[DirectCosts]
           ,[IndirectCosts]
           ,[TotalFunding]
           ,[Comments]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT app.ApplicationId, bud.Requested_Direct, bud.Requested_Indirect, bud.Req_Total_Funding, CASE WHEN CAST(bud.Budget_Comments AS varchar(max)) = 'No changes recommended' THEN NULL ELSE bud.Budget_Comments END, vun.UserID, bud.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRO_Budget bud INNER JOIN
	[ViewApplication] app ON bud.LOG_NO = app.LogNumber LEFT OUTER JOIN
	ViewLegacyUserNameToUserId vun ON bud.LAST_UPDATED_BY = vun.UserName
WHERE bud.LOG_NO IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewApplicationBudget WHERE ApplicationId = app.ApplicationId)
--ApplicationCompliance
--Import all compliance statuses from 1.0
INSERT INTO ApplicationCompliance ([ApplicationId]
           ,[ComplianceStatusId]
           ,[Comment]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT [ViewApplication].ApplicationId, [ComplianceStatus].ComplianceStatusId, PRO_Compliance.Comment, VUN.UserId, dbo.GetP2rmisDateTime()
FROM [$(P2RMIS)].dbo.PRO_Compliance PRO_Compliance INNER JOIN
[ViewApplication] ON PRO_Compliance.Log_No = [ViewApplication].LogNumber INNER JOIN
[ComplianceStatus] ON PRO_Compliance.Compliance_Status = ComplianceStatus.ComplianceStatusLabel LEFT OUTER JOIN
ViewLegacyUserNameToUserId VUN ON PRO_Compliance.Last_Updated_By = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM ApplicationCompliance WHERE ApplicationId = ViewApplication.ApplicationId AND DeletedFlag = 0);
--Import withdrawn statuses
UPDATE [Application] SET [WithdrawnFlag] = CASE WHEN PRO_Proposal.WITHDRAWN_DATE IS NOT NULL THEN 1 ELSE 0 END, [WithdrawnBy] = VUN.UserID, [WithdrawnDate] = PRO_Proposal.WITHDRAWN_DATE
FROM [Application] INNER JOIN
[$(P2RMIS)].dbo.PRO_Proposal PRO_Proposal ON [Application].LogNumber = PRO_Proposal.LOG_NO LEFT OUTER JOIN
ViewLegacyUserNameToUserId VUN ON PRO_Proposal.Withdrawn_By = VUN.UserName
WHERE DeletedFlag = 0 AND WithdrawnDate IS NULL AND PRO_Proposal.WITHDRAWN_DATE IS NOT NULL;
--ApplicationPersonnel
INSERT INTO [ApplicationPersonnel]
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
           ,[ModifiedBy]
           ,[ModifiedDate])
--PI Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.PI_First_Name, pro.PI_LAST_NAME, pro.PI_MIDDLE_INITIAL, pro.PI_ORG_NAME, 
pro.PI_PHONE_NUMBER, pro.PI_EMAIL, 1, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'PI'
WHERE (pro.PI_LAST_NAME IS NOT NULL OR pro.PI_ORG_NAME IS NOT NULL) AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId) AND cat.ClientId = @ClientId
UNION ALL
--PI2 Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.PI2_First_Name, pro.PI2_LAST_NAME, pro.PI2_MIDDLE_INITIAL, NULL, 
NULL, pro.PI2_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'PI2'
WHERE pro.PI2_LAST_NAME IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId) AND cat.ClientId = @ClientId
UNION ALL
--Admin1 Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.admin1_First_Name, pro.admin1_LAST_NAME, pro.admin1_MIDDLE_INITIAL, pro.admin1_ORG_NAME, 
pro.admin1_phone_number, pro.admin1_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'admin-1'
WHERE (pro.admin1_LAST_NAME IS NOT NULL OR pro.admin1_ORG_NAME IS NOT NULL) AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId) AND cat.ClientId = @ClientId
UNION ALL
--admin2 Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.admin2_First_Name, pro.admin2_LAST_NAME, pro.admin2_MIDDLE_INITIAL, NULL, 
pro.admin2_phone_number, pro.admin2_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'admin-2'
WHERE pro.admin2_LAST_NAME IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId) AND cat.ClientId = @ClientId
UNION ALL
--admin3 Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.admin3_First_Name, pro.admin3_LAST_NAME, pro.admin3_MIDDLE_INITIAL, NULL, 
pro.admin3_phone_number, pro.admin3_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'admin-3'
WHERE pro.admin3_LAST_NAME IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId) AND cat.ClientId = @ClientId
UNION ALL
--Mentor Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.mentor_First_Name, pro.mentor_LAST_NAME, pro.mentor_MIDDLE_INITIAL, NULL, 
NULL, NULL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'mentor'
WHERE pro.mentor_LAST_NAME IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId) AND cat.ClientId = @ClientId
UNION ALL
--COI Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, procoi.FirstName, procoi.LASTNAME, NULL, procoi.orgname, 
	procoi.phone, procoi.email, 0, procoi.coisource, NULL, procoi.datetimestamp
	FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[$(P2RMIS)].dbo.PRO_COI procoi ON pro.Log_No = procoi.Log_No INNER JOIN 
	[$(P2RMIS)].dbo.PRO_COI_Type procoitype ON procoi.coitypeid = procoitype.coitypeid INNER JOIN 
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND procoitype.coitype = capt.ApplicationPersonnelType
WHERE NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId) AND cat.ClientId = @ClientId;
--ApplicationInfo
INSERT INTO [ApplicationInfo]
           ([ApplicationId]
           ,[ClientApplicationInfoTypeId]
           ,[InfoText]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT app.ApplicationId, 1, prop.Grant_ID, vun.UserId, prop.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRO_Proposal prop INNER JOIN
[Application] app ON prop.LOG_NO = app.LogNumber INNER JOIN
[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId LEFT OUTER JOIN
[ViewLegacyUserNameToUserId] vun ON prop.LAST_UPDATED_BY = vun.UserName
WHERE Grant_ID IS NOT NULL AND cat.ClientId = @ClientId AND NOT EXISTS (Select 'X' FROM ViewApplicationInfo WHERE ApplicationId = app.ApplicationId);
--ApplicationText
INSERT INTO [ApplicationText]
           ([ApplicationId]
           ,[ClientApplicationTextTypeId]
           ,[BodyText]
           ,[AbstractFlag]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT app.ApplicationId, catt.ClientApplicationTextTypeId, protext.BodyText, CASE WHEN pm.AbstractFormat = 'Data' THEN 1 ELSE 0 END, 
		vun.UserID, protext.LAST_UPDATED_DATE
FROM [$(P2RMIS)].dbo.PRO_Text protext INNER JOIN
[$(P2RMIS)].dbo.PRO_Text_Type protexttype ON protext.TextTypeID = protexttype.TextTypeID INNER JOIN
	[ViewApplication] app ON protext.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationTextType] catt ON protexttype.TextType = catt.TextType AND cat.ClientId = catt.ClientId LEFT OUTER JOIN
	ViewLegacyUserNameToUserId vun ON protext.LAST_UPDATED_BY = vun.UserName
WHERE NOT EXISTS (Select 1 From ViewApplicationText WHERE ApplicationId = app.ApplicationId AND ClientApplicationTextTypeId = catt.ClientApplicationTextTypeId) AND cat.ClientId = @ClientId;
--PanelApplication
INSERT INTO [dbo].[PanelApplication]
( --[PanelApplicationId]
      [SessionPanelId]
      ,[ApplicationId]
      ,[ReviewOrder]
     -- ,[CreatedBy]
      --,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
 )
SELECT sp.[sessionPanelId] SessionPanelID
	  --,pp.[panel_id]
      ,app.ApplicationId
      ,pp.[Order_of_Review] ReviewOrder
      ,v.[userid] ModifiedBy
      ,pp.[LAST_UPDATE_Date] ModifiedDate
FROM [$(P2RMIS)].[dbo].[PRG_Panel_Proposals] pp
  inner join [dbo].[ViewApplication] app on pp.Log_No = app.LogNumber
  inner join [dbo].[ViewProgramMechanism] pm on app.ProgramMechanismId = pm.ProgramMechanismId
  inner join [dbo].[ClientAwardType] cat on pm.ClientAwardTypeId = cat.ClientAwardTypeId
  inner join [dbo].[ViewSessionPanel] sp on pp.[panel_id] = sp.[LegacyPanelId]
  inner join [dbo].[ViewMeetingSession] ms on sp.MeetingSessionId = ms.MeetingSessionId
  inner join [dbo].[ViewClientMeeting] cm on ms.ClientMeetingId = cm.ClientMeetingId
  left join [dbo].[ViewLegacyUserNameToUserId] v on pp.[LAST_UPDATED_BY] = v.[username]
WHERE NOT EXISTS (Select 'X' FROM ViewPanelApplication WHERE SessionPanelId = sp.SessionPanelId AND ApplicationId = app.ApplicationId) AND cat.ClientId = @ClientId and cm.ClientId = @ClientId
order by pp.[panel_id];
--PanelUserAssignment
INSERT INTO [dbo].[PanelUserAssignment]
( 
      [SessionPanelId]
      ,[UserId]
      ,[ClientParticipantTypeId]
      ,[ClientRoleId]
      ,[LegacyParticipantId]
	  ,[RestrictedAssignedFlag]
	  ,[ParticipationMethodId]
      ,[ModifiedBy]
      ,[ModifiedDate]
 )
 --Exclude duplicate participations
SELECT
    sp.[sessionPanelId] SessionPanelID
    ,u.[userid] UserId
    ,cpt.[ClientParticipantTypeId] ClientParticipantTypeId
    ,cr.[ClientRoleID] ClientRoleID
    ,p.[Prg_Part_ID] LegacyParticipantTypeId
	,PartMapping.RestrictedAssignedFlag
	,PartMapping.NewParticipantMethod
    ,v.[userid] ModifiedBy
    ,p.[LAST_UPDATE_DATE] ModifiedDate
  FROM [$(P2RMIS)].[dbo].[PRG_Participants] p
  inner join [dbo].[ViewSessionPanel] sp on p.[panel_id] = sp.[LegacyPanelId]
  inner join [$(P2RMIS)].[dbo].[PRG_Program_LU] prg on p.Program = prg.Program
  inner join [dbo].[Client] c on prg.Client = c.ClientAbrv 
  inner join ViewMeetingSession ms on sp.MeetingSessionId = ms.MeetingSessionId
  inner join ViewClientMeeting cm on ms.ClientMeetingId = cm.ClientMeetingId AND c.ClientId = cm.ClientId
  left join [dbo].[ClientRole] cr on p.[part_role_type] = cr.[RoleAbbreviation] and ISNULL(cm.[ClientId], 19) = cr.[ClientId]
  cross apply [dbo].udfLegacyToNewParticipantTypeMap(p.Part_Type, ISNULL(CR.SpecialistFlag, 0), CM.MeetingTypeId, CM.ClientId) PartMapping 
  INNER JOIN [dbo].[ClientParticipantType] cpt ON  PartMapping.NewParticipantTypeAbbreviation = cpt.ParticipantTypeAbbreviation AND
				CM.ClientId = cpt.ClientId
  inner join [dbo].[viewuser] u on p.[person_id] = u.[personid]
  left join [dbo].[ViewLegacyUserNameToUserId] v on p.[LAST_UPDATED_BY] = v.[username]
  --We need to exclude program participants and participants who were somehow orphaned from a panel in the old database
  WHERE p.Panel_ID IS NOT NULL AND sp.SessionPanelID IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewPanelUserAssignment WHERE SessionPanelId = sp.SessionPanelId AND LegacyParticipantId = p.Prg_Part_ID) AND c.ClientId = @ClientId;
--PanelApplicationReviewerExpertise
INSERT INTO [PanelApplicationReviewerExpertise]
           ([PanelApplicationId]
           ,[PanelUserAssignmentId]
           ,[ClientExpertiseRatingId]
           ,[ExpertiseComments]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT panapp.PanelApplicationId, pua.PanelUserAssignmentId, Cer.ClientExpertiseRatingId, coi.Comments, vun.UserId, rp.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRG_Reviewer_Preferences rp INNER JOIN
[ViewPanelUserAssignment] pua ON rp.Prg_Part_ID = pua.LegacyParticipantId INNER JOIN
[ViewApplication] app ON rp.Log_No = app.LogNumber INNER JOIN
[ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId AND pua.SessionPanelId = panapp.SessionPanelId INNER JOIN
[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
[ClientAwardType] ca ON pm.ClientAwardTypeId = ca.ClientAwardTypeId INNER JOIN
[ClientExpertiseRating] cer ON rp.Rev_Pref = cer.RatingAbbreviation AND ca.ClientId = cer.ClientId LEFT OUTER JOIN
[$(P2RMIS)].dbo.PRG_Reviewer_Preferences_COI coi ON rp.Prg_Part_ID = coi.Prg_Part_ID AND rp.Log_No = coi.Log_No LEFT OUTER JOIN
ViewLegacyUserNameToUserId vun ON rp.LAST_UPDATED_BY = vun.UserName
WHERE NOT EXISTS (Select 'X' FROM ViewPanelApplicationReviewerExpertise WHERE PanelApplicationId = panapp.PanelApplicationId AND PanelUserAssignmentId = pua.PanelUserAssignmentId) AND ca.ClientId = @ClientId;
--PanelApplicationReviewerCoiDetail
INSERT INTO [PanelApplicationReviewerCoiDetail]
           ([PanelApplicationReviewerExpertiseId]
           ,[ClientCoiTypeId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT panre.PanelApplicationReviewerExpertiseId, cct.ClientCoiTypeId, vun.UserId, coi.LAST_UPDATE_DATE
FROM ViewPanelApplicationReviewerExpertise panre INNER JOIN
ViewPanelApplication panapp ON panre.PanelApplicationId = panapp.PanelApplicationId INNER JOIN 
ViewPanelUserAssignment pua ON panre.PanelUserAssignmentId = pua.PanelUserAssignmentId INNER JOIN
[ViewApplication] app ON panapp.ApplicationId = app.ApplicationId INNER JOIN
[$(P2RMIS)].dbo.PRG_Reviewer_Preferences_COI coi ON app.LogNumber = coi.Log_No AND pua.LegacyParticipantId = coi.Prg_Part_ID INNER JOIN
[$(P2RMIS)].dbo.PRG_REV_COI_Type_LU coitype ON coi.COI_Type = coitype.COI_Type INNER JOIN
ViewProgramMechanism pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
ClientAwardType cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
ClientCoiType cct ON cat.ClientId = cct.ClientId AND coitype.COI_Type_DESC = cct.CoiTypeDescription LEFT OUTER JOIN
ViewLegacyUserNameToUserId vun ON coi.LAST_UPDATED_BY = vun.UserName
WHERE NOT EXISTS (SELECT 'X' FROM ViewPanelApplicationReviewerCoiDetail WHERE PanelApplicationReviewerExpertiseId = panre.PanelApplicationReviewerExpertiseId) AND cat.ClientId = @ClientId;
--PanelApplicationReviewerAssignment
INSERT INTO [dbo].[PanelApplicationReviewerAssignment]
           ([PanelApplicationId]
           ,[PanelUserAssignmentId]
           ,[ClientAssignmentTypeId]
           ,[SortOrder]
           ,[LegacyProposalAssignmentId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT PanelApplication.PanelApplicationId, PanelUserAssignment.PanelUserAssignmentId, ClientAssignmentType.ClientAssignmentTypeId,
	inserted.Sort_Order, inserted.PA_ID, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM [$(P2RMIS)].dbo.PRG_Proposal_Assignments inserted INNER JOIN
		[dbo].ViewPanelUserAssignment PanelUserAssignment ON inserted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
		[dbo].ViewApplication Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[dbo].ViewPanelApplication PanelApplication ON PanelUserAssignment.SessionPanelId = PanelApplication.SessionPanelId AND
		Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
		[dbo].ViewProgramMechanism ProgramMechanism ON Application.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
		[dbo].ClientAwardType ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId INNER JOIN
		[dbo].AssignmentType AssignmentType ON inserted.Assignment_ID = AssignmentType.LegacyAssignmentId INNER JOIN
		[dbo].ClientAssignmentType ClientAssignmentType ON AssignmentType.AssignmentTypeId = ClientAssignmentType.AssignmentTypeId AND
		ClientAwardType.ClientId = ClientAssignmentType.ClientId LEFT OUTER JOIN
		dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
WHERE NOT EXISTS (Select 'X' FROM ViewPanelApplicationReviewerAssignment WHERE LegacyProposalAssignmentId = inserted.PA_ID) AND ClientAwardType.ClientId = @ClientId;
--ProgramUserAssignment
INSERT INTO [dbo].[ProgramUserAssignment]
( --[ProgramUserAssignmentId]
      [ProgramYearId]
      ,[UserId]
      ,[ClientParticipantTypeId]
      ,[LegacyParticipantId]
      --,[CreatedBy]
      --,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
 )
  SELECT 
      py.[ProgramYearId] ProgramYearId
      ,u.[userid] UserId
      ,cpt.[ClientParticipantTypeId] ClientParticipantTypeId
      ,p.[Prg_Part_ID] LegacyParticipantID
      ,v.[userid] ModifiedBy
      ,p.[LAST_UPDATE_DATE] ModifiedDate
  FROM [$(P2RMIS)].[dbo].[PRG_Participants] p
  left join [dbo].[ClientProgram] cp on p.[program] = cp.[LegacyProgramId] 
  left join [dbo].[ClientParticipantType] cpt on  p.[part_type] = cpt.[LegacyPartTypeId] AND cp.ClientId = cpt.ClientId
  left join [dbo].[ProgramYear] py on p.[fy] = py.[year] and cp.[ClientProgramId] = py.[ClientProgramId]
 left join [dbo].[user] u on p.[person_id] = u.[personid]
  left join [dbo].[ViewLegacyUserNameToUserId] v on p.[LAST_UPDATED_BY] = v.[username]
  --Remove participants with programs that do not exist
  where p.panel_id is null AND py.ProgramYearId IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewProgramUserAssignment WHERE ProgramYearId = py.ProgramYearId AND LegacyParticipantId = p.Prg_Part_ID) 
	AND cp.ClientId = @ClientId
  order by py.[ProgramYearId];
  --ApplicationReviewStatus
--Insert only for those that don't already have a review status
INSERT INTO [ApplicationReviewStatus]
           ([PanelApplicationId]
           ,[ReviewStatusId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT panapp.PanelApplicationId, CASE opanapp.Triaged WHEN 1 THEN 1 ELSE 2 END, panapp.ModifiedBy, panapp.ModifiedDate
FROM [$(P2RMIS)].dbo.[PRG_Panel_Proposals] opanapp INNER JOIN
[ViewApplication] app ON opanapp.Log_No = app.LogNumber INNER JOIN
[dbo].[ViewProgramMechanism] pm on app.ProgramMechanismId = pm.ProgramMechanismId
	inner join [dbo].[ClientAwardType] cat on pm.ClientAwardTypeId = cat.ClientAwardTypeId
	inner join [ViewSessionPanel] sp ON opanapp.Panel_ID = sp.LegacyPanelId INNER JOIN
[ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId AND sp.SessionPanelId = panapp.SessionPanelId
WHERE panapp.PanelApplicationId NOT IN (Select PanelApplicationId FROM ViewApplicationReviewStatus ars INNER JOIN ReviewStatus rs ON ars.ReviewStatusId = rs.ReviewStatusId WHERE ReviewStatusTypeId = 1) AND cat.ClientId = @ClientId
--Add a record for qualifying range if doesn't exist
UNION ALL
SELECT panapp.PanelApplicationId, 4, panapp.ModifiedBy, panapp.ModifiedDate
FROM [$(P2RMIS)].dbo.[PRG_Panel_Proposals] opanapp INNER JOIN
[ViewApplication] app ON opanapp.Log_No = app.LogNumber INNER JOIN
[dbo].[ViewProgramMechanism] pm on app.ProgramMechanismId = pm.ProgramMechanismId
	inner join [dbo].[ClientAwardType] cat on pm.ClientAwardTypeId = cat.ClientAwardTypeId
	inner join [ViewSessionPanel] sp ON opanapp.Panel_ID = sp.LegacyPanelId INNER JOIN
[ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId AND sp.SessionPanelId = panapp.SessionPanelId
WHERE opanapp.Fundable_Range = 1 AND panapp.PanelApplicationId NOT IN (Select PanelApplicationId FROM ViewApplicationReviewStatus ars WHERE ReviewStatusId = 4) AND cat.ClientId = @ClientId
--Add a record for command draft if doesn't exist
UNION ALL
SELECT panapp.PanelApplicationId, 3, panapp.ModifiedBy, panapp.ModifiedDate
FROM [$(P2RMIS)].dbo.[SS_Tracking] sst INNER JOIN
[ViewApplication] app ON sst.Log_No = app.LogNumber INNER JOIN
[dbo].[ViewProgramMechanism] pm on app.ProgramMechanismId = pm.ProgramMechanismId
	inner join [dbo].[ClientAwardType] cat on pm.ClientAwardTypeId = cat.ClientAwardTypeId
	inner join [ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId
WHERE sst.Client_Review = 1 AND panapp.PanelApplicationId NOT IN (Select PanelApplicationId FROM ViewApplicationReviewStatus ars WHERE ReviewStatusId = 3) AND cat.ClientId = @ClientId;
--PanelStage
--Insert an asynch stage for all panels in legacy P2RMIS
INSERT INTO PanelStage
                      (SessionPanelId, ReviewStageId, StageOrder, ViewSessionPanel.CreatedBy, ViewSessionPanel.CreatedDate, ViewSessionPanel.ModifiedBy, ViewSessionPanel.ModifiedDate)
SELECT     SessionPanelId, 1 AS Expr1, 1 AS Expr2, ViewSessionPanel.CreatedBy, ViewSessionPanel.CreatedDate, ViewSessionPanel.ModifiedBy, ViewSessionPanel.ModifiedDate
FROM         ViewSessionPanel
INNER JOIN ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId
INNER JOIN ClientMeeting ON ViewMeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId
Where SessionPanelId NOT IN
(Select SessionPanelId FROM PanelStage Where ReviewStageId = 1) AND ClientMeeting.ClientId = @ClientId;

--Insert a synch stage for all panels in legacy P2RMIS with at least one score
INSERT INTO PanelStage
                      (SessionPanelId, ReviewStageId, StageOrder, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT     SessionPanelId, 2 AS Expr1, 2 AS Expr2, ViewSessionPanel.CreatedBy, ViewSessionPanel.CreatedDate, ViewSessionPanel.ModifiedBy, ViewSessionPanel.ModifiedDate
FROM         ViewSessionPanel 
INNER JOIN ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId
INNER JOIN ClientMeeting ON ViewMeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId
Where SessionPanelId NOT IN
(Select SessionPanelId FROM PanelStage Where ReviewStageId = 2) AND ClientMeeting.ClientId = @ClientId;
--PanelStageStep
INSERT INTO .[dbo].[PanelStageStep]
           ([PanelStageId]
           ,[StepTypeId]
           ,[StepName]
           ,[StepOrder]
           ,[StartDate]
           ,[EndDate]
           ,[ReOpenDate]
           ,[ReCloseDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT      PanelStage.PanelStageId, CASE MTG_Phase_Member.Phase_ID WHEN 1 THEN 5 WHEN 2 THEN 6 ELSE 7 END, CASE MTG_Phase_Member.Phase_ID WHEN 1 THEN 'Preliminary' WHEN 2 THEN 'Revised' ELSE 'Online Discussion' END AS StepName, Phase_Order AS StepOrder, Phase_Start AS StartDate, Phase_End AS EndDate, Phase_ReOpen AS ReOpenDate, Phase_ReClose AS ReCloseDate, VUN.UserID AS ModifiedBy, MTG_Phase_Member.LAST_UPDATE_DATE AS ModifiedDate
FROM         [$(P2RMIS)].[dbo].MTG_Phase_Member INNER JOIN
                      [$(P2RMIS)].[dbo].PAN_Master ON MTG_Phase_Member.Session_ID = PAN_Master.Session_ID INNER JOIN
                     ViewSessionPanel SessionPanel ON Pan_Master.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
					 ViewMeetingSession ON SessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId INNER JOIN 
					 ClientMeeting ON ViewMeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
                     ViewPanelStage PanelStage ON SessionPanel.SessionPanelId = PanelStage.SessionPanelId AND PanelStage.ReviewStageId = 1 LEFT OUTER JOIN
                     ViewLegacyUserNameToUserId VUN ON MTG_Phase_Member.LAST_UPDATED_BY = VUN.UserName 
WHERE NOT EXISTS (Select 'X' FROM ViewPanelStageStep WHERE PanelStageId = PanelStage.PanelStageId AND StepTypeId = CASE MTG_Phase_Member.Phase_ID WHEN 1 THEN 5 WHEN 2 THEN 6 ELSE 7 END) AND ClientMeeting.ClientId = @ClientId
UNION ALL
SELECT		PanelStage.PanelStageId, 8, 'Meeting', 1, SessionPanel.StartDate, SessionPanel.EndDate, NULL, NULL, SessionPanel.ModifiedBy, SessionPanel.ModifiedDate
FROM		ViewPanelStage PanelStage INNER JOIN
			ViewSessionPanel SessionPanel ON PanelStage.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
								 ViewMeetingSession ON SessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId INNER JOIN 
					 ClientMeeting ON ViewMeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId
WHERE PanelStage.ReviewStageId = 2 AND NOT EXISTS (Select 'X' FROM ViewPanelStageStep WHERE PanelStageId = PanelStage.PanelStageId AND StepTypeId = 8) AND ClientMeeting.ClientId = @ClientId;
--ApplicationStage
INSERT INTO [dbo].[ApplicationStage]
           ([PanelApplicationId]
           ,[ReviewStageId]
           ,[StageOrder]
           ,[ActiveFlag]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT PanelApplication.PanelApplicationId, PanelStage.ReviewStageId, PanelStage.StageOrder, 
	CASE WHEN ApplicationReviewStatus.ReviewStatusId = 1 AND PanelStage.ReviewStageId = 2 THEN 0 ELSE 1 END,
	PanelStage.ModifiedBy, PanelStage.ModifiedDate
FROM	ViewPanelApplication PanelApplication INNER JOIN 
	[dbo].[ViewApplication] app on PanelApplication.ApplicationId = app.ApplicationId
	inner join [dbo].[ViewProgramMechanism] pm on app.ProgramMechanismId = pm.ProgramMechanismId
	inner join [dbo].[ClientAwardType] cat on pm.ClientAwardTypeId = cat.ClientAwardTypeId
	inner join ViewPanelStage PanelStage ON PanelApplication.SessionPanelId = PanelStage.SessionPanelId INNER JOIN
	ViewApplicationReviewStatus ApplicationReviewStatus ON PanelApplication.PanelApplicationId = ApplicationReviewStatus.PanelApplicationId INNER JOIN
	ReviewStatus ON ApplicationReviewStatus.ReviewStatusId = ReviewStatus.ReviewStatusId
WHERE ReviewStatus.ReviewStatusTypeId = 1 AND PanelStage.ReviewStageId IN (1,2)
AND NOT EXISTS (Select 'X' FROM ViewApplicationStage WHERE PanelApplicationId = PanelApplication.PanelApplicationId AND ReviewStageId = PanelStage.ReviewStageId) AND cat.ClientId = @ClientId;

--ApplicationStageStep
INSERT INTO [dbo].[ApplicationStageStep]
           ([ApplicationStageId]
           ,[PanelStageStepId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationStage.ApplicationStageId, PanelStageStep.PanelStageStepId, ApplicationStage.ModifiedBy, ApplicationStage.ModifiedDate
FROM ViewApplicationStage ApplicationStage INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	ViewSessionPanel SessionPanel ON PanelApplication.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
	ViewMeetingSession ON SessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId INNER JOIN 
	ClientMeeting ON ViewMeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
	ViewPanelStage PanelStage ON SessionPanel.SessionPanelId = PanelStage.SessionPanelId AND ApplicationStage.ReviewStageId = PanelStage.ReviewStageId INNER JOIN
	ViewPanelStageStep PanelStageStep ON PanelStage.PanelStageId = PanelStageStep.PanelStageId
WHERE NOT EXISTS (Select 'X' FROM ApplicationStageStep WHERE DeletedFlag = 0 AND ApplicationStageId = ApplicationStage.ApplicationStageId AND PanelStageStepId = PanelStageStep.PanelStageStepId) AND ClientMeeting.ClientId = @ClientId;
--ApplicationStageStepDiscussion
INSERT INTO [dbo].[ApplicationStageStepDiscussion]
           ([ApplicationStageStepId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationStageStep.ApplicationStageStepId, ApplicationStageStep.ModifiedBy, ApplicationStageStep.ModifiedDate
FROM ViewApplicationStageStep ApplicationStageStep INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationStageStep.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelStageStep PanelStageStep ON ApplicationStageStep.PanelStageStepId = PanelStageStep.PanelStageStepId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
	[dbo].[ViewProgramMechanism] pm on [Application].ProgramMechanismId = pm.ProgramMechanismId inner join
	[dbo].[ClientAwardType] cat on pm.ClientAwardTypeId = cat.ClientAwardTypeId
WHERE PanelStageStep.StepTypeId = 7 AND Application.LogNumber IN (Select DISTINCT LOG_NO FROM [$(P2RMIS)].dbo.PRG_Panel_Proposal_Review_Discussion)
	AND NOT EXISTS (Select 'X' FROM ApplicationStageStepDiscussion WHERE DeletedFlag = 0 AND ApplicationStageStepId = ApplicationStageStep.ApplicationStageStepId) AND cat.ClientId = @ClientId;
--ApplicationStageStepDiscussionComment
INSERT INTO [dbo].[ApplicationStageStepDiscussionComment]
           ([ApplicationStageStepDiscussionId]
           ,[Comment]
		   ,[CreatedBy]
		   ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationStageStepDiscussion.ApplicationStageStepDiscussionId, CAST(PRG_Panel_Proposal_Review_Discussion.Discussion_Text AS nvarchar(4000)), U.UserId, PRG_Panel_Proposal_Review_Discussion.LAST_UPDATE_DATE, U.UserId, PRG_Panel_Proposal_Review_Discussion.LAST_UPDATE_DATE
FROM ApplicationStageStepDiscussion INNER JOIN
	ApplicationStageStep ON ApplicationStageStepDiscussion.ApplicationStageStepId = ApplicationStageStep.ApplicationStageStepId INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationStageStep.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	[ViewApplication] [Application] ON PanelApplication.ApplicationId = [Application].ApplicationId INNER JOIN
	[dbo].[ViewProgramMechanism] pm on [Application].ProgramMechanismId = pm.ProgramMechanismId inner join
	[dbo].[ClientAwardType] cat on pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[$(P2RMIS)].dbo.PRG_Panel_Proposal_Review_Discussion PRG_Panel_Proposal_Review_Discussion ON [Application].LogNumber = PRG_Panel_Proposal_Review_Discussion.LOG_NO INNER JOIN
	[$(P2RMIS)].dbo.PRG_Participants PRG_Participants ON PRG_Panel_Proposal_Review_Discussion.Prg_Part_ID = PRG_Participants.Prg_Part_ID INNER JOIN
	[$(P2RMIS)].dbo.PPL_People PPL_People ON PRG_Participants.Person_ID = PPL_People.Person_ID LEFT OUTER JOIN
	[ViewUser] U ON PPL_People.Person_ID = U.PersonID
WHERE NOT EXISTS (Select 'X' FROM ApplicationStageStepDiscussionComment WHERE DeletedFlag = 0 AND ApplicationStageStepDiscussionId = ApplicationStageStepDiscussion.ApplicationStageStepDiscussionId) AND cat.ClientId = @ClientId;
--MechanismTemplate
INSERT INTO .[dbo].[MechanismTemplate]
           ([ProgramMechanismId]
           ,[ReviewStageId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ProgramMechanism.ProgramMechanismId, PanelStage.ReviewStageId, ProgramMechanism.ModifiedBy, ProgramMechanism.ModifiedDate
FROM   ViewPanelStage	PanelStage INNER JOIN
	ViewSessionPanel SessionPanel ON PanelStage.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
	ViewPanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId INNER JOIN
	ViewApplication [Application] ON PanelApplication.ApplicationId = [Application].ApplicationId INNER JOIN
	ViewProgramMechanism ProgramMechanism ON [Application].ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
	ClientAwardType cat on ProgramMechanism.ClientAwardTypeId = cat.ClientAwardTypeId
WHERE NOT EXISTS (Select 'X' FROM ViewMechanismTemplate WHERE ProgramMechanismId = ProgramMechanism.ProgramMechanismId AND ReviewStageId = PanelStage.ReviewStageId) AND cat.ClientId = @ClientId
GROUP BY ProgramMechanism.ProgramMechanismId, ProgramMechanism.LegacyAtmId, PanelStage.ReviewStageId, ProgramMechanism.ModifiedBy, ProgramMechanism.ModifiedDate
HAVING PanelStage.ReviewStageId IN (1,2)
ORDER BY ProgramMechanismId, ReviewStageId;
--MechanismTemplateElement
INSERT INTO [dbo].[MechanismTemplateElement]
           ([MechanismTemplateId]
           ,[ClientElementId]
           ,[LegacyEcmId]
           ,[InstructionText]
           ,[SortOrder]
           ,[RecommendedWordCount]
           ,[ScoreFlag]
           ,[TextFlag]
           ,[OverallFlag]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT	MechanismTemplate.MechanismTemplateId, ClientElement.ClientElementId, PRO_Evaluation_Criteria_Member.ECM_ID,
	PRO_Evaluation_Criteria_Member.SCO_Description, PRO_Evaluation_Criteria_Member.SORT_ORDER,
	PRO_Evaluation_Criteria_Member.Word_Count, PRO_Evaluation_Criteria_Member.Score_Flag,
	PRO_Evaluation_Criteria_Member.Text_Flag, PRO_Evaluation_Criteria_Member.Overall_Eval,
	VUN.UserId, PRO_Evaluation_Criteria_Member.LAST_UPDATE_DATE
FROM	 ViewMechanismTemplate MechanismTemplate INNER JOIN
	ViewProgramMechanism ProgramMechanism ON MechanismTemplate.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
	ViewProgramYear ProgramYear ON ProgramMechanism.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
	ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
	[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member ON ProgramMechanism.LegacyAtmId = PRO_Evaluation_Criteria_Member.ATM_ID INNER JOIN
	ClientElement ON PRO_Evaluation_Criteria_Member.EVAL_Criteria = ClientElement.ElementIdentifier AND ClientProgram.ClientId = ClientElement.ClientId LEFT OUTER JOIN
	ViewLegacyUserNameToUserId VUN ON PRO_Evaluation_Criteria_Member.LAST_UPDATED_BY = VUN.UserName 
WHERE NOT EXISTS (Select 'X' FROM ViewMechanismTemplateElement WHERE MechanismTemplateId = MechanismTemplate.MechanismTemplateId AND LegacyEcmId = PRO_Evaluation_Criteria_Member.ECM_ID) AND ClientProgram.ClientId = @ClientId
ORDER BY MechanismTemplateId, ClientElementId;
--MechanismTemplateElementScoring
INSERT INTO MechanismTemplateElementScoring
                      (MechanismTemplateElementId, ClientScoringId, StepTypeId, ModifiedBy, ModifiedDate)
--Get all scoring_phases in legacy structure for pre-meeting stage
SELECT MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId, CASE PRO_ECM_Scoring.Scoring_phase WHEN 'Initial' THEN 5 WHEN 'Revised' THEN 6 WHEN 'Meeting' THEN 7 END, VUN.UserId, PRO_ECM_Scoring.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRO_ECM_Scoring INNER JOIN
[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member ON PRO_ECM_Scoring.ECM_ID = PRO_Evaluation_Criteria_Member.ECM_ID INNER JOIN
[$(P2RMIS)].dbo.PRO_Award_Type_Member ON PRO_Evaluation_Criteria_Member.ATM_ID = PRO_Award_Type_Member.ATM_ID INNER JOIN
ViewProgramMechanism ProgramMechanism ON PRO_Award_Type_Member.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
ViewProgramYear ProgramYear ON ProgramMechanism.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
ViewMechanismTemplate MechanismTemplate ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND ReviewStageId = 1 INNER JOIN
ViewMechanismTemplateElement MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId AND PRO_Evaluation_Criteria_Member.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
ClientScoringScale ON PRO_ECM_Scoring.Hi_Val = ClientScoringScale.HighValue AND
ISNULL(PRO_ECM_Scoring.Hi_Val_Desc, '') = ISNULL(ClientScoringScale.HighValueDescription, '') AND
PRO_ECM_Scoring.Middle_Val = ClientScoringScale.MiddleValue AND
ISNULL(PRO_ECM_Scoring.Middle_Val_Desc, '') = ISNULL(ClientScoringScale.MiddleValueDescription, '') AND
PRO_ECM_Scoring.Low_Val = ClientScoringScale.LowValue AND
ISNULL(PRO_ECM_Scoring.Low_Val_Desc, '') = ISNULL(ClientScoringScale.LowValueDescription, '') AND
PRO_ECM_Scoring.Scoring_Type = ClientScoringScale.ScoreType AND ClientProgram.ClientId = ClientScoringScale.ClientId LEFT OUTER JOIN
ViewLegacyUserNameToUserId VUN ON PRO_ECM_Scoring.LAST_UPDATED_BY = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM ViewMechanismTemplateElementScoring WHERE MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId AND StepTypeId = CASE PRO_ECM_Scoring.Scoring_phase WHEN 'Initial' THEN 5 WHEN 'Revised' THEN 6 WHEN 'Meeting' THEN 7 END) AND ClientScoringScale.ClientId = @ClientId
GROUP BY MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId, PRO_ECM_Scoring.Scoring_Phase, PRO_Evaluation_Criteria_Member.Overall_Eval, VUN.UserId, PRO_ECM_Scoring.LAST_UPDATE_DATE
UNION ALL
--Get only scoring_phase of Meeting in legacy structure for meeting stage
SELECT MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId, 8, VUN.UserId, PRO_ECM_Scoring.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRO_ECM_Scoring INNER JOIN
[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member ON PRO_ECM_Scoring.ECM_ID = PRO_Evaluation_Criteria_Member.ECM_ID INNER JOIN
[$(P2RMIS)].dbo.PRO_Award_Type_Member ON PRO_Evaluation_Criteria_Member.ATM_ID = PRO_Award_Type_Member.ATM_ID INNER JOIN
ViewProgramMechanism ProgramMechanism ON PRO_Award_Type_Member.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
ViewProgramYear ProgramYear ON ProgramMechanism.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
ViewMechanismTemplate MechanismTemplate ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND ReviewStageId = 2 INNER JOIN
ViewMechanismTemplateElement MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId AND PRO_Evaluation_Criteria_Member.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
ClientScoringScale ON PRO_ECM_Scoring.Hi_Val = ClientScoringScale.HighValue AND
ISNULL(PRO_ECM_Scoring.Hi_Val_Desc, '') = ISNULL(ClientScoringScale.HighValueDescription, '') AND
PRO_ECM_Scoring.Middle_Val = ClientScoringScale.MiddleValue AND
ISNULL(PRO_ECM_Scoring.Middle_Val_Desc, '') = ISNULL(ClientScoringScale.MiddleValueDescription, '') AND
PRO_ECM_Scoring.Low_Val = ClientScoringScale.LowValue AND
ISNULL(PRO_ECM_Scoring.Low_Val_Desc, '') = ISNULL(ClientScoringScale.LowValueDescription, '') AND
PRO_ECM_Scoring.Scoring_Type = ClientScoringScale.ScoreType AND ClientProgram.ClientId = ClientScoringScale.ClientId LEFT OUTER JOIN
ViewLegacyUserNameToUserId VUN ON PRO_ECM_Scoring.LAST_UPDATED_BY = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM ViewMechanismTemplateElementScoring WHERE MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId AND StepTypeId = 8) AND ClientScoringScale.ClientId = @ClientId
GROUP BY MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId, PRO_ECM_Scoring.Scoring_Phase, PRO_Evaluation_Criteria_Member.Overall_Eval, VUN.UserId, PRO_ECM_Scoring.LAST_UPDATE_DATE
HAVING Scoring_Phase = 'Meeting';
--ReviewerEvaluation
INSERT INTO [dbo].[ReviewerEvaluation]
           ([PanelUserAssignmentId]
           ,[Rating]
           ,[Comments]
           ,[RecommendChairFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT PanelUserAssignment.PanelUserAssignmentId,  PRG_Part_Evaluation.Rating,  PRG_Part_Evaluation.Comments,  ISNULL(PRG_Part_Evaluation.Rec_Chair, 0), VUN2.UserId,  PRG_Part_Evaluation.Evaluation_Date, VUN.UserId,  PRG_Part_Evaluation.Last_Update_Date
FROM [$(P2RMIS)].[dbo].PRG_Part_Evaluation 
	INNER JOIN ViewPanelUserAssignment PanelUserAssignment ON  PRG_Part_Evaluation.Prg_Part_ID = PanelUserAssignment.LegacyParticipantId
	INNER JOIN ViewSessionPanel SessionPanel ON PanelUserAssignment.SessionPanelId = SessionPanel.SessionPanelId
	INNER JOIN ViewMeetingSession MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId
	INNER JOIN ClientMeeting ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId
	LEFT OUTER JOIN [User] VUN2 ON PRG_Part_Evaluation.Owner_Person_ID = VUN2.PersonId
	LEFT OUTER JOIN [ViewLegacyUserNameToUserId] VUN ON PRG_Part_Evaluation.LAST_UPDATED_BY = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM ViewReviewerEvaluation WHERE PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId AND CreatedBy = VUN2.UserID) AND VUN2.UserID IS NOT NULL
	AND ClientMeeting.ClientId = @ClientId;
--ProgramPayRate
INSERT INTO [dbo].[ProgramPayRate]
           ([ProgramYearId]
           ,[ClientParticipantTypeId]
		   ,[ParticipantMethodId]
		   ,[RestrictedAssignedFlag]
		   ,[MeetingTypeId]
		   ,[EmploymentCategoryId]
           ,[HonorariumAccepted]
           ,[ConsultantFeeText]
		   ,[ConsultantFee]
           ,[PeriodStartDate]
           ,[PeriodEndDate]
           ,[ManagerList]
           ,[DescriptionOfWork]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT py.ProgramYearId, cpt.ClientParticipantTypeId, PartTypeMapping.NewParticipantMethod, PartTypeMapping.RestrictedAssignedFlag, MeetingType.MeetingTypeId, CASE inserted.EC_ID WHEN 13 THEN 1 WHEN 14 THEN 2 ELSE 3 END, CASE inserted.EC_ID WHEN 13 THEN 'Paid' WHEN 14 THEN 'Unpaid' ELSE 'Unpaid w/t' END, inserted.Consultant_Fee, inserted.Fixed_Amount,
	inserted.Period_Start, inserted.Period_End, inserted.SRA_Managers, inserted.Work_Description, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE 
	FROM [$(P2RMIS)].dbo.PRG_Part_Pay_Rate inserted
	INNER JOIN [dbo].ClientProgram cp ON inserted.Program = cp.LegacyProgramId 
	INNER JOIN [dbo].ProgramYear py ON cp.ClientProgramId = py.ClientProgramId AND inserted.FY = py.[Year] 
	CROSS APPLY [dbo].udfLegacyToNewParticipantTypeMap(inserted.Part_Type, 0, 1, cp.ClientId) PartTypeMapping
	INNER JOIN [dbo].ClientParticipantType cpt ON PartTypeMapping.NewParticipantTypeAbbreviation = cpt.ParticipantTypeAbbreviation AND cp.ClientId = cpt.ClientId
	LEFT OUTER JOIN dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	CROSS JOIN [dbo].MeetingType MeetingType
	WHERE cp.ClientId = @ClientId AND NOT EXISTS (SELECT 'X' FROM ViewProgramPayRate WHERE ProgramYearId = py.ProgramYearId AND ClientParticipantTypeId = cpt.ClientParticipantTypeId AND ParticipantMethodId = PartTypeMapping.NewParticipantMethod AND RestrictedAssignedFlag = PartTypeMapping.RestrictedAssignedFlag AND MeetingTypeId = MeetingType.MeetingTypeId)
	AND inserted.EC_ID IN (13, 14) AND (MeetingType.MeetingTypeId = CASE WHEN inserted.Part_Type IN ('OR', 'OCH', 'OCR') THEN 3 END OR MeetingType.MeetingTypeId = CASE WHEN inserted.Part_Type IN ('TC', 'CHT', 'CRT') THEN 2 END OR MeetingType.MeetingTypeId = CASE WHEN inserted.Part_Type IN ('TC','CHT','CRT','CR','SR','AH','CH') THEN 1 END)

	INSERT INTO [dbo].[ProgramPayRate]
           ([ProgramYearId]
           ,[ClientParticipantTypeId]
		   ,[ParticipantMethodId]
		   ,[RestrictedAssignedFlag]
		   ,[MeetingTypeId]
		   ,[EmploymentCategoryId]
           ,[HonorariumAccepted]
           ,[ConsultantFeeText]
		   ,[ConsultantFee]
           ,[PeriodStartDate]
           ,[PeriodEndDate]
           ,[ManagerList]
           ,[DescriptionOfWork]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT py.ProgramYearId, cpt.ClientParticipantTypeId, PartTypeMapping.NewParticipantMethod, PartTypeMapping.RestrictedAssignedFlag, MeetingType.MeetingTypeId, CASE inserted.EC_ID WHEN 13 THEN 1 WHEN 14 THEN 2 ELSE 3 END, CASE inserted.EC_ID WHEN 13 THEN 'Paid' WHEN 14 THEN 'Unpaid' ELSE 'Unpaid w/t' END, inserted.Consultant_Fee, inserted.Fixed_Amount,
	inserted.Period_Start, inserted.Period_End, inserted.SRA_Managers, inserted.Work_Description, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE 
	FROM [$(P2RMIS)].dbo.PRG_Part_Pay_Rate inserted
	INNER JOIN [dbo].ClientProgram cp ON inserted.Program = cp.LegacyProgramId 
	INNER JOIN [dbo].ProgramYear py ON cp.ClientProgramId = py.ClientProgramId AND inserted.FY = py.[Year] 
	CROSS APPLY [dbo].udfLegacyToNewParticipantTypeMap(inserted.Part_Type, 1, 1, cp.ClientId) PartTypeMapping
	INNER JOIN [dbo].ClientParticipantType cpt ON PartTypeMapping.NewParticipantTypeAbbreviation = cpt.ParticipantTypeAbbreviation AND cp.ClientId = cpt.ClientId
	LEFT OUTER JOIN dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	CROSS JOIN [dbo].MeetingType MeetingType
	WHERE cp.ClientId = @ClientId AND NOT EXISTS (SELECT 'X' FROM ViewProgramPayRate WHERE ProgramYearId = py.ProgramYearId AND ClientParticipantTypeId = cpt.ClientParticipantTypeId AND ParticipantMethodId = PartTypeMapping.NewParticipantMethod AND RestrictedAssignedFlag = PartTypeMapping.RestrictedAssignedFlag AND MeetingTypeId = MeetingType.MeetingTypeId)
	AND inserted.EC_ID IN (13, 14) AND inserted.Part_Type IN ('SR', 'OR', 'TC', 'AH') AND (MeetingType.MeetingTypeId = CASE WHEN inserted.Part_Type IN ('OR', 'OCH', 'OCR') THEN 3 END OR MeetingType.MeetingTypeId = CASE WHEN inserted.Part_Type IN ('TC', 'CHT', 'CRT') THEN 2 END OR MeetingType.MeetingTypeId = CASE WHEN inserted.Part_Type IN ('TC','CHT','CRT','CR','SR','AH','CH') THEN 1 END)
--ApplicationTemplate
INSERT INTO [dbo].[ApplicationTemplate]
           ([ApplicationId]
           ,[ApplicationStageId]
           ,[MechanismTemplateId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT [Application].ApplicationId, ApplicationStage.ApplicationStageId, MechanismTemplate.MechanismTemplateId, MechanismTemplate.ModifiedBy, MechanismTemplate.ModifiedDate
FROM [ViewApplication] [Application] INNER JOIN
	ViewPanelApplication PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
	ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
	ViewMechanismTemplate MechanismTemplate ON Application.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND ApplicationStage.ReviewStageId = MechanismTemplate.ReviewStageId
WHERE ApplicationStage.ReviewStageId IN (1,2) AND NOT EXISTS
(Select 'X' FROM ViewApplicationTemplate WHERE ViewApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId AND ViewApplicationTemplate.MechanismTemplateId = MechanismTemplate.MechanismTemplateId);
--ApplicationTemplateElement
INSERT INTO [dbo].[ApplicationTemplateElement]
           ([ApplicationTemplateId]
           ,[MechanismTemplateElementId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationTemplate.ApplicationTemplateId, MechanismTemplateElement.MechanismTemplateElementId, 
	MechanismTemplateElement.ModifiedBy, MechanismTemplateElement.ModifiedDate
FROM ViewApplicationTemplate ApplicationTemplate INNER JOIN
	ViewMechanismTemplate MechanismTemplate ON ApplicationTemplate.MechanismTemplateId = MechanismTemplate.MechanismTemplateId INNER JOIN
	ViewMechanismTemplateElement MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId
WHERE NOT EXISTS (Select 'X' FROM ViewApplicationTemplateElement WHERE ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId AND MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId);
--ApplicationWorkflow
INSERT INTO [dbo].[ApplicationWorkflow]
           ([WorkflowId]
           ,[ApplicationStageId]
           ,[ApplicationTemplateId]
           ,[PanelUserAssignmentId]
		   ,[ApplicationWorkflowName]
		   ,[DateAssigned]
		   ,[ModifiedBy]
		   ,[ModifiedDate])
SELECT CASE WHEN SUM(Phase_ID) = 1 THEN (SELECT WorkflowId FROM Workflow WHERE Workflow.WorkflowName = 'Pre-Meeting (Legacy)' AND Workflow.ClientId = ClientMeeting.ClientId)
			WHEN SUM(Phase_ID) = 3 THEN (SELECT WorkflowId FROM Workflow WHERE Workflow.WorkflowName = 'Pre-Meeting' AND Workflow.ClientId = ClientMeeting.ClientId)
			WHEN SUM(Phase_ID) = 4 THEN (SELECT WorkflowId FROM Workflow WHERE Workflow.WorkflowName = 'Online Discussion (Legacy)' AND Workflow.ClientId = ClientMeeting.ClientId)
			ELSE (SELECT WorkflowId FROM Workflow WHERE Workflow.WorkflowName = 'Online Discussion' AND Workflow.ClientId = ClientMeeting.ClientId) END,
			ApplicationStage.ApplicationStageId,
			ApplicationTemplate.ApplicationTemplateId,
			PanelUserAssignment.PanelUserAssignmentId,
			PanelUserAssignment.PanelUserAssignmentId,
			PanelApplicationReviewerAssignment.CreatedDate,
			PanelApplicationReviewerAssignment.ModifiedBy,
			PanelApplicationReviewerAssignment.ModifiedDate
FROM		ViewPanelApplication PanelApplication INNER JOIN
			ViewSessionPanel SessionPanel ON PanelApplication.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
			ViewMeetingSession MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId INNER JOIN
			ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
			[$(P2RMIS)].dbo.MTG_Phase_Member ON MeetingSession.LegacySessionId = MTG_Phase_Member.Session_ID INNER JOIN
			ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
			ViewApplicationTemplate ApplicationTemplate ON ApplicationStage.ApplicationStageId = ApplicationTemplate.ApplicationStageId INNER JOIN
			ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
			ClientAssignmentType ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
			ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId
WHERE		ApplicationStage.ReviewStageId = 1 AND ClientAssignmentType.AssignmentTypeId NOT IN (7, 8)
			AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflow WHERE ApplicationStageId = ApplicationStage.ApplicationStageId AND PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId)
GROUP BY PanelApplication.ApplicationId,
			ApplicationStage.ApplicationStageId,
			ApplicationTemplate.ApplicationTemplateId,
			PanelUserAssignment.PanelUserAssignmentId,
			ClientMeeting.ClientId,
			PanelApplicationReviewerAssignment.CreatedDate,
			PanelApplicationReviewerAssignment.ModifiedBy,
			PanelApplicationReviewerAssignment.ModifiedDate
UNION ALL
SELECT		(SELECT WorkflowId FROM Workflow WHERE Workflow.WorkflowName = 'Meeting' AND Workflow.ClientId = ClientMeeting.ClientId),
			ApplicationStage.ApplicationStageId,
			ApplicationTemplate.ApplicationTemplateId,
			PanelUserAssignment.PanelUserAssignmentId,
			PanelUserAssignment.PanelUserAssignmentId,
			PanelApplicationReviewerAssignment.CreatedDate,
			PanelApplicationReviewerAssignment.ModifiedBy,
			PanelApplicationReviewerAssignment.ModifiedDate
FROM		ViewPanelApplication PanelApplication INNER JOIN
			ViewSessionPanel SessionPanel ON PanelApplication.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
			ViewMeetingSession MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId INNER JOIN
			ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
			ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
			ViewApplicationTemplate ApplicationTemplate ON ApplicationStage.ApplicationStageId = ApplicationTemplate.ApplicationStageId INNER JOIN
			ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
			ClientAssignmentType ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
			ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId
WHERE		ApplicationStage.ReviewStageId = 2 AND ClientAssignmentType.AssignmentTypeId NOT IN (7, 8)
			AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflow WHERE ApplicationStageId = ApplicationStage.ApplicationStageId AND PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId)
ORDER BY ApplicationTemplateId, PanelUserAssignment.PanelUserAssignmentId;
			


--Next we create workflows for unassigned reviewers stage 2 (other than COIs) and only those that have meeting scores
INSERT INTO [dbo].[ApplicationWorkflow]
           ([WorkflowId]
           ,[ApplicationStageId]
           ,[ApplicationTemplateId]
		   ,[PanelUserAssignmentId]
           ,[ApplicationWorkflowName]
		   ,[DateAssigned]
		   ,[ModifiedBy]
		   ,[ModifiedDate])
SELECT (SELECT WorkflowId FROM Workflow WHERE Workflow.WorkflowName = 'Meeting' AND Workflow.ClientId = ClientMeeting.ClientId),
			ApplicationStage.ApplicationStageId,
			ApplicationTemplate.ApplicationTemplateId,
			PanelUserAssignment.PanelUserAssignmentId,
			PanelUserAssignment.PanelUserAssignmentId,
			PanelUserAssignment.CreatedDate,
			PanelUserAssignment.ModifiedBy,
			PanelUserAssignment.ModifiedDate
FROM         ClientAssignmentType INNER JOIN
                      ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON 
                      ClientAssignmentType.ClientAssignmentTypeId = PanelApplicationReviewerAssignment.ClientAssignmentTypeId RIGHT OUTER JOIN
                      ViewPanelUserAssignment PanelUserAssignment INNER JOIN
                      ViewSessionPanel SessionPanel ON PanelUserAssignment.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
                      ViewPanelApplication PanelApplication ON SessionPanel.SessionPanelId = PanelApplication.SessionPanelId INNER JOIN
					  ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
                      ViewMeetingSession MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId INNER JOIN
                      ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
                      ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
                      ViewApplicationTemplate ApplicationTemplate ON ApplicationStage.ApplicationStageId = ApplicationTemplate.ApplicationStageId ON 
                      PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId AND 
                      PanelApplicationReviewerAssignment.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
					  ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
WHERE      (ApplicationStage.ReviewStageId = 2) AND (ClientAssignmentType.AssignmentTypeId = 7 OR
                      ClientAssignmentType.AssignmentTypeId IS NULL) AND ClientParticipantType.ReviewerFlag = 1 AND EXISTS (Select 'X' FROM [$(P2RMIS)].dbo.PRG_Panel_Scores WHERE LOG_NO = [Application].LogNumber)
					  AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflow WHERE ApplicationStageId = ApplicationStage.ApplicationStageId AND PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId);
--ApplicationWorkflowStep
INSERT INTO [dbo].[ApplicationWorkflowStep]
           ([ApplicationWorkflowId]
           ,[StepTypeId]
           ,[StepName]
           ,[Active]
           ,[StepOrder]
           ,[Resolution]
		   ,[ResolutionDate]
		   ,[ModifiedBy]
		   ,[ModifiedDate])
SELECT     ApplicationWorkflow.ApplicationWorkflowId, WorkflowStep.StepTypeId, WorkflowStep.StepName, 
                      CASE WHEN COUNT([$(P2RMIS)].dbo.PRG_Panel_Proposal_Review_Discussion.Discussion_ID) > 0 THEN 1 ELSE WorkflowStep.ActiveDefault END AS Expr2, 
                      WorkflowStep.StepOrder, CASE WHEN PRG_Critique_Phase.Date_Submitted IS NULL THEN 0 ELSE 1 END, PRG_Critique_Phase.Date_Submitted, VUN.UserID, PRG_Critique_Phase.LAST_UPDATE_DATE 
FROM         ViewApplicationWorkflow ApplicationWorkflow INNER JOIN
                      Workflow ON ApplicationWorkflow.WorkflowId = Workflow.WorkflowId INNER JOIN
                      WorkflowStep ON Workflow.WorkflowId = WorkflowStep.WorkflowId INNER JOIN
					  ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
					  ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewApplication [Application] ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
					  ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId LEFT OUTER JOIN
					  [$(P2RMIS)].dbo.PRG_Proposal_Assignments PRG_Proposal_Assignments ON Application.LogNumber = PRG_Proposal_Assignments.Log_No AND PanelUserAssignment.LegacyParticipantId = PRG_Proposal_Assignments.Prg_Part_ID LEFT OUTER JOIN
					  [$(P2RMIS)].dbo.PRG_Critiques PRG_Critiques ON PRG_Proposal_Assignments.PA_ID = PRG_Critiques.PA_ID LEFT OUTER JOIN
					  [$(P2RMIS)].dbo.PRG_Critique_Phase PRG_Critique_Phase ON PRG_Critiques.Critique_ID = PRG_Critique_Phase.Critique_ID AND WorkflowStep.StepTypeId = CASE PRG_Critique_Phase.Scoring_Phase WHEN 'initial' THEN 5 WHEN 'revised' THEN 6 ELSE 7 END   LEFT OUTER JOIN
                      [$(P2RMIS)].dbo.PRG_Panel_Proposal_Review_Discussion ON 
                      Application.LogNumber = [$(P2RMIS)].dbo.PRG_Panel_Proposal_Review_Discussion.LOG_NO LEFT OUTER JOIN
                      dbo.ViewLegacyUserNameToUserId VUN ON  PRG_Critique_Phase.LAST_UPDATED_BY = VUN.UserName
WHERE     NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStep WHERE ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId AND StepTypeId = WorkflowStep.StepTypeId)
GROUP BY ApplicationWorkflow.ApplicationWorkflowId, WorkflowStep.StepTypeId, WorkflowStep.StepName, WorkflowStep.StepOrder, WorkflowStep.ActiveDefault, PRG_Critique_Phase.Date_Submitted, VUN.UserID, PRG_Critique_Phase.LAST_UPDATE_DATE

--ApplicationWorkflowStepAssignment
INSERT INTO [dbo].[ApplicationWorkflowStepAssignment]
           ([ApplicationWorkflowStepId]
           ,[UserId]
           ,[AssignmentId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT      DISTINCT ApplicationWorkflowStep.ApplicationWorkflowStepId, PanelUserAssignment.UserId, ClientAssignmentType.AssignmentTypeId, 
                      PanelApplicationReviewerAssignment.ModifiedBy, PanelApplicationReviewerAssignment.ModifiedDate
FROM         ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment INNER JOIN
                      ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
                      ViewPanelApplication PanelApplication ON PanelApplicationReviewerAssignment.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND 
                      PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ClientAssignmentType ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId
WHERE ApplicationStage.ReviewStageId IN (1,2) AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepAssignment WHERE UserId = PanelUserAssignment.UserId AND ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId)
UNION ALL
--Next, insert step assignments for reviewers not assigned at the application level but on the panel for stage 2
SELECT     DISTINCT ApplicationWorkflowStep.ApplicationWorkflowStepId, PanelUserAssignment.UserId, CASE WHEN ClientParticipantType.LegacyPartTypeId IN ('OCR', 'CRT', 'CR') 
                      THEN 6 ELSE 5 END AS Expr1, PanelUserAssignment.ModifiedBy, PanelUserAssignment.ModifiedDate
FROM         ViewPanelUserAssignment PanelUserAssignment INNER JOIN
                      ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
                      ViewPanelApplication PanelApplication ON PanelUserAssignment.SessionPanelId = PanelApplication.SessionPanelId INNER JOIN
                      ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND 
                      PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId LEFT OUTER JOIN
                      ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND 
                      PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId
WHERE     (ClientParticipantType.ReviewerFlag = 1) AND (ApplicationStage.ReviewStageId = 2) AND 
                      (PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId IS NULL) AND
					  NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepAssignment WHERE UserId = PanelUserAssignment.UserId AND ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId)
ORDER BY ApplicationWorkflowStepId, UserId;
--ApplicationWorkflowStepWorkLog
INSERT INTO .[dbo].[ApplicationWorkflowStepWorkLog]
           ([ApplicationWorkflowStepId]
           ,[UserId]
           ,[CheckOutDate]
           ,[CheckInDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT      ApplicationWorkflowStep.ApplicationWorkflowStepId, PanelUserAssignment.UserId, ISNULL(ApplicationWorkflowStep.ModifiedDate, '1/1/2002'), ApplicationWorkflowStep.ResolutionDate,
		ApplicationWorkflowStep.ModifiedBy, ApplicationWorkflowStep.ModifiedDate
FROM         ViewApplicationWorkflowStep ApplicationWorkflowStep INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
                      ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
                      ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId INNER JOIN
                      ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId 
WHERE     (ApplicationStage.ReviewStageId = 1) AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepWorkLog WHERE ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId);
--ApplicationWorkflowStepElement
INSERT INTO .[dbo].[ApplicationWorkflowStepElement]
           ([ApplicationWorkflowStepId]
           ,[ApplicationTemplateElementId]
           ,[AccessLevelId]
           ,[ClientScoringId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT DISTINCT    ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationTemplateElement.ApplicationTemplateElementId, 1, MechanismTemplateElementScoring.ClientScoringId, ApplicationTemplateElement.ModifiedBy, 
                      ApplicationTemplateElement.ModifiedDate
FROM         ViewApplicationTemplate ApplicationTemplate INNER JOIN
                      ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationTemplate.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
                      ViewApplicationStage ApplicationStage ON ApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationTemplate.ApplicationTemplateId = ApplicationWorkflow.ApplicationTemplateId AND 
                      ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ViewMechanismTemplateElement MechanismTemplateElement ON 
                      ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
					  ClientElement ON MechanismTemplateElement.ClientElementId = ClientElement.ClientElementId	LEFT OUTER JOIN
                      ViewMechanismTemplateElementScoring MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId AND ApplicationWorkflowStep.StepTypeId = MechanismTemplateElementScoring.StepTypeId
WHERE     (ApplicationStage.ReviewStageId = 1) AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepElement WHERE ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId AND ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId)
UNION ALL
--Assigned reviewers get all criteria for meeting stage
SELECT DISTINCT    ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationTemplateElement.ApplicationTemplateElementId, 1, MechanismTemplateElementScoring.ClientScoringId, ApplicationTemplateElement.ModifiedBy, 
                      ApplicationTemplateElement.ModifiedDate
FROM         ViewApplicationTemplate ApplicationTemplate INNER JOIN
                      ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationTemplate.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
                      ViewApplicationStage ApplicationStage ON ApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationTemplate.ApplicationTemplateId = ApplicationWorkflow.ApplicationTemplateId AND 
                      ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId INNER JOIN
                      ViewMechanismTemplateElement MechanismTemplateElement ON 
                      ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
					  ClientElement ON MechanismTemplateElement.ClientElementId = ClientElement.ClientElementId	LEFT OUTER JOIN
                      ViewMechanismTemplateElementScoring MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId AND ApplicationWorkflowStep.StepTypeId = MechanismTemplateElementScoring.StepTypeId
WHERE     (ApplicationStage.ReviewStageId = 2) AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepElement WHERE ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId AND ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId)
UNION ALL
--Unassigned reviewers get only scored criteria
SELECT DISTINCT    ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationTemplateElement.ApplicationTemplateElementId, 1, MechanismTemplateElementScoring.ClientScoringId, ApplicationTemplateElement.ModifiedBy, 
                      ApplicationTemplateElement.ModifiedDate
FROM         ViewApplicationTemplate ApplicationTemplate INNER JOIN
                      ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationTemplate.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
                      ViewApplicationStage ApplicationStage ON ApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationTemplate.ApplicationTemplateId = ApplicationWorkflow.ApplicationTemplateId AND 
                      ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewMechanismTemplateElement MechanismTemplateElement ON 
                      ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
					  ClientElement ON MechanismTemplateElement.ClientElementId = ClientElement.ClientElementId	LEFT OUTER JOIN
					  ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId LEFT OUTER JOIN
                      ViewMechanismTemplateElementScoring MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId AND ApplicationWorkflowStep.StepTypeId = MechanismTemplateElementScoring.StepTypeId
WHERE     (ApplicationStage.ReviewStageId = 2) AND PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId IS NULL AND MechanismTemplateElement.ScoreFlag = 1 AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepElement WHERE ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId AND ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId);
--ApplicationWorkflowStepElementContent
INSERT INTO  [dbo].[ApplicationWorkflowStepElementContent]
           ([ApplicationWorkflowStepElementId]
           ,[Score]
           ,[ContentText]
           ,[Abstain]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT     DISTINCT ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, [$(P2RMIS)].dbo.PRG_Criteria_Eval.Criteria_Score, 
                      CAST([$(P2RMIS)].dbo.PRG_Criteria_Eval.Criteria_Txt AS varchar(max)), CASE WHEN [$(P2RMIS)].dbo.PRG_Criteria_Eval.Criteria_Score IS NULL AND CAST([$(P2RMIS)].dbo.PRG_Criteria_Eval.Criteria_Txt AS varchar(max)) = 'n/a' THEN 1 ELSE 0 END,
					  VUN.UserId, [$(P2RMIS)].dbo.PRG_Criteria_Eval.LAST_UPDATE_DATE
FROM                  ViewApplicationWorkflowStep ApplicationWorkflowStep INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
                      ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement ON 
                      ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
                      ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
                      ViewMechanismTemplateElement MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
                      ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN                      
                      ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId INNER JOIN
                      ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
                      [$(P2RMIS)].dbo.PRG_Proposal_Assignments ON 
                      PanelApplicationReviewerAssignment.LegacyProposalAssignmentId = [$(P2RMIS)].dbo.PRG_Proposal_Assignments.PA_ID INNER JOIN
                      [$(P2RMIS)].dbo.PRG_Critiques ON [$(P2RMIS)].dbo.PRG_Proposal_Assignments.PA_ID = [$(P2RMIS)].dbo.PRG_Critiques.PA_ID INNER JOIN
                      [$(P2RMIS)].dbo.PRG_Criteria_Eval ON [$(P2RMIS)].dbo.PRG_Critiques.Critique_ID = [$(P2RMIS)].dbo.PRG_Criteria_Eval.Critique_ID AND 
                      CASE ApplicationWorkflowStep.StepTypeId WHEN 5 THEN 'Initial' WHEN 6 THEN 'Revised' ELSE 'Meeting' END = [$(P2RMIS)].dbo.PRG_Criteria_Eval.Scoring_Phase AND
                      MechanismTemplateElement.LegacyEcmId = [$(P2RMIS)].dbo.PRG_Criteria_Eval.Ecm_ID  LEFT OUTER JOIN
                      ViewLegacyUserNameToUserId VUN ON [$(P2RMIS)].dbo.PRG_Criteria_Eval.LAST_UPDATED_BY = VUN.UserName     
WHERE ApplicationStage.ReviewStageId IN (1,2) AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepElementContent WHERE ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId)
--Stage 2 score/unassigned insert
INSERT INTO [dbo].[ApplicationWorkflowStepElementContent]
           ([ApplicationWorkflowStepElementId]
           ,[Score]
           ,[Abstain]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT DISTINCT ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, CASE WHEN ScorePosition.OverallFlag = 1 THEN [$(P2RMIS)].dbo.PRG_Panel_Scores.Global_Score
				WHEN ScorePosition.Position = 1 THEN [$(P2RMIS)].dbo.PRG_Panel_Scores.Eval1
				WHEN ScorePosition.Position = 2 THEN [$(P2RMIS)].dbo.PRG_Panel_Scores.Eval2
				WHEN ScorePosition.Position = 3 THEN [$(P2RMIS)].dbo.PRG_Panel_Scores.Eval3
				WHEN ScorePosition.Position = 4 THEN [$(P2RMIS)].dbo.PRG_Panel_Scores.Eval4
				WHEN ScorePosition.Position = 5 THEN [$(P2RMIS)].dbo.PRG_Panel_Scores.Eval5
				WHEN ScorePosition.Position = 6 THEN [$(P2RMIS)].dbo.PRG_Panel_Scores.Eval6
				WHEN ScorePosition.Position = 7 THEN [$(P2RMIS)].dbo.PRG_Panel_Scores.Eval7
				WHEN ScorePosition.Position = 8 THEN [$(P2RMIS)].dbo.PRG_Panel_Scores.Eval8
				WHEN ScorePosition.Position = 9 THEN [$(P2RMIS)].dbo.PRG_Panel_Scores.Eval9
				WHEN ScorePosition.Position = 10 THEN [$(P2RMIS)].dbo.PRG_Panel_Scores.Eval10 END,
		CASE WHEN [$(P2RMIS)].dbo.PRG_Panel_Scores.Global_Score IS NULL AND [$(P2RMIS)].dbo.PRG_Panel_Scores.Eval1 IS NULL THEN 1 ELSE 0 END,
		VUN.UserId, [$(P2RMIS)].dbo.PRG_Panel_Scores.LAST_UPDATE_DATE
FROM 	ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement INNER JOIN
	ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
	ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
	ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN 
	ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
	(SELECT DENSE_RANK() OVER (PARTITION BY MechanismTemplateElement.MechanismTemplateId ORDER BY MechanismTemplateElement.OverallFlag, MechanismTemplateElement.SortOrder) AS Position, MechanismTemplateElement.MechanismTemplateElementId, MechanismTemplateElement.LegacyEcmId, MechanismTemplateElement.OverallFlag
		FROM ViewMechanismTemplateElement MechanismTemplateElement
		WHERE MechanismTemplateElement.ScoreFlag = 1) ScorePosition ON ApplicationTemplateElement.MechanismTemplateElementId = ScorePosition.MechanismTemplateElementId INNER JOIN
	[$(P2RMIS)].dbo.PRG_Panel_Scores ON PanelUserAssignment.LegacyParticipantId = [$(P2RMIS)].dbo.PRG_Panel_Scores.PRG_Part_Id AND [Application].LogNumber = [$(P2RMIS)].dbo.PRG_Panel_Scores.Log_No	LEFT OUTER JOIN
	ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND
	PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId LEFT OUTER JOIN
	ViewLegacyUserNameToUserId VUN ON [$(P2RMIS)].dbo.PRG_Panel_Scores.LAST_UPDATED_BY = VUN.UserName
WHERE ApplicationStage.ReviewStageId = 2 AND NOT EXISTS (Select 1 FROM ApplicationWorkflowStepElementContent WHERE ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId) 

--Update ApplicationReviewStatus for scored apps
UPDATE ApplicationReviewStatus
SET ReviewStatusId = 6
WHERE ReviewStatusId = 2 AND PanelApplicationId IN
(Select PanelApplication.PanelApplicationId
FROM PanelApplication INNER JOIN
ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
ApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId
WHERE ApplicationStage.ReviewStageId = 2 AND ApplicationWorkflowStep.StepTypeId = 8 AND ApplicationWorkflowStepElementContent.DeletedFlag = 0)


INSERT INTO ApplicationWorkflowStepElementContent ([ApplicationWorkflowStepElementId]
           ,[ContentText]
           ,[Abstain]
           ,[CritiqueRevised]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, PRG_Criteria_Eval.Criteria_Txt, CASE WHEN CAST(PRG_Criteria_Eval.Criteria_Txt AS varchar(max)) = 'n/a' THEN 1 ELSE 0 END,
	CASE WHEN PRG_Criteria_Eval.Scoring_Phase = 'Meeting' THEN 1 ELSE 0 END, VUN.UserId, PRG_Criteria_Eval.LAST_UPDATE_DATE
		   FROM ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement INNER JOIN
ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
ViewApplicationReviewStatus ApplicationReviewStatus ON PanelApplication.PanelApplicationId = ApplicationReviewStatus.PanelApplicationId INNER JOIN
ReviewStatus ON ApplicationReviewStatus.ReviewStatusId = ReviewStatus.ReviewStatusId AND ReviewStatus.ReviewStatusTypeId = 1 INNER JOIN
ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN 
ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
ViewMechanismTemplateElement MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND
	PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
	[$(P2RMIS)].dbo.PRG_Proposal_Assignments ON 
                      PanelApplicationReviewerAssignment.LegacyProposalAssignmentId = [$(P2RMIS)].dbo.PRG_Proposal_Assignments.PA_ID INNER JOIN
                      [$(P2RMIS)].dbo.PRG_Critiques ON [$(P2RMIS)].dbo.PRG_Proposal_Assignments.PA_ID = [$(P2RMIS)].dbo.PRG_Critiques.PA_ID INNER JOIN
	(Select Critique_ID, Criteria_Txt, Scoring_Phase, ECM_ID, PRG_Criteria_Eval.Last_Updated_By, PRG_Criteria_Eval.LAST_UPDATE_DATE, ROW_NUMBER() OVER (Partition By Critique_ID, ECM_ID Order By Last_Update_Date DESC) AS Ranking
	FROM [$(P2RMIS)].dbo.PRG_Criteria_Eval) PRG_Criteria_Eval ON  [$(P2RMIS)].dbo.PRG_Critiques.Critique_ID = PRG_Criteria_Eval.Critique_ID AND MechanismTemplateElement.LegacyEcmId = PRG_Criteria_Eval.ECM_ID AND PRG_Criteria_Eval.Ranking = 1 LEFT OUTER JOIN
                      ViewLegacyUserNameToUserId VUN ON PRG_Criteria_Eval.LAST_UPDATED_BY = VUN.UserName  
WHERE ApplicationStage.ReviewStageId = 2 AND MechanismTemplateElement.ScoreFlag = 0 AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepElementContent WHERE ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId)
AND ApplicationReviewStatus.ReviewStatusId = 6
--ApplicationComments (see script for prod)
--Unnassigned reviewer comments
INSERT INTO [dbo].[UserApplicationComment]
           ([UserID]
           ,[PanelApplicationId]
           ,[ApplicationID]
           ,[Comments]
           ,[CommentTypeID]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT DISTINCT ViewPanelUserAssignment.UserID, ViewPanelApplication.PanelApplicationId, ViewPanelApplication.ApplicationId, CAST(PRG_Panel_Proposal_Comment.comment AS nvarchar(max)), 5, VUN.UserId, PRG_Panel_Proposal_Comment.Last_Update_Date
FROM [$(P2RMIS)].dbo.PRG_Panel_Proposal_Comment PRG_Panel_Proposal_Comment
INNER JOIN [ViewApplication] ON PRG_Panel_Proposal_Comment.Log_No = [ViewApplication].LogNumber
INNER JOIN [ViewPanelApplication] ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId
INNER JOIN [ViewPanelUserAssignment] ON PRG_Panel_Proposal_Comment.Prg_Part_ID = ViewPanelUserAssignment.LegacyParticipantId
INNER JOIN ViewLegacyUserNameToUserId VUN ON PRG_Panel_Proposal_Comment.Last_Updated_By = VUN.UserName
WHERE LEN(LTRIM(CAST(comment AS nvarchar(max)))) > 1 AND NOT EXISTS (SELECT 'X' FROM ViewUserApplicationComment WHERE PanelApplicationId = ViewPanelApplication.PanelApplicationId AND UserId = ViewPanelUserAssignment.UserId AND CommentTypeID = 5)

--Application stage active status
UPDATE ApplicationStage SET ActiveFlag = 1
FROM ApplicationStage INNER JOIN
ViewPanelApplication ON ApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
ViewApplicationReviewStatus ON ViewPanelApplication.PanelApplicationId = ViewApplicationReviewStatus.PanelApplicationId
WHERE ApplicationStage.ReviewStageId = 2 AND ApplicationStage.ActiveFlag = 0 AND ViewApplicationReviewStatus.ReviewStatusId = 6

GO

 EXEC sp_msforeachtable 'ALTER TABLE ? ENABLE TRIGGER all'
 GO
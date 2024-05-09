--SET VIA CALLER-- DECLARE @LegacyPanelId INT = 0;
DECLARE @NewPanelId INT;
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
  WHERE NOT EXISTS (Select 'X' FROM ViewSessionPanel WHERE LegacyPanelId = pm.Panel_ID AND MeetingSessionId = ms2.MeetingSessionId) AND ms2.MeetingSessionId IS NOT NULL AND pm.Panel_Id = @LegacyPanelId;
  
  SELECT @NewPanelId = SCOPE_IDENTITY();
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
 WHERE NOT EXISTS (Select 'X' FROM ViewProgramPanel WHERE ProgramYearId = py.ProgramYearId AND SessionPanelId = sp.SessionPanelId) AND sp.SessionPanelId = @NewPanelId;

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
WHERE NOT EXISTS (Select 'X' FROM ViewPanelApplication WHERE SessionPanelId = sp.SessionPanelId AND ApplicationId = app.ApplicationId) AND sp.SessionPanelId = @NewPanelId
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
  WHERE p.Panel_ID IS NOT NULL AND sp.SessionPanelID IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewPanelUserAssignment WHERE SessionPanelId = sp.SessionPanelId AND LegacyParticipantId = p.Prg_Part_ID) AND sp.SessionPanelId = @NewPanelId;
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
WHERE NOT EXISTS (Select 'X' FROM ViewPanelApplicationReviewerExpertise WHERE PanelApplicationId = panapp.PanelApplicationId AND PanelUserAssignmentId = pua.PanelUserAssignmentId) AND panapp.SessionPanelId = @NewPanelId;
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
WHERE NOT EXISTS (SELECT 'X' FROM ViewPanelApplicationReviewerCoiDetail WHERE PanelApplicationReviewerExpertiseId = panre.PanelApplicationReviewerExpertiseId) AND panapp.SessionPanelId = @NewPanelId;
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
WHERE NOT EXISTS (Select 'X' FROM ViewPanelApplicationReviewerAssignment WHERE LegacyProposalAssignmentId = inserted.PA_ID) AND PanelApplication.SessionPanelId = @NewPanelId;


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
WHERE panapp.PanelApplicationId NOT IN (Select PanelApplicationId FROM ViewApplicationReviewStatus ars INNER JOIN ReviewStatus rs ON ars.ReviewStatusId = rs.ReviewStatusId WHERE ReviewStatusTypeId = 1) AND sp.SessionPanelId = @NewPanelId
--Add a record for qualifying range if doesn't exist
UNION ALL
SELECT panapp.PanelApplicationId, 4, panapp.ModifiedBy, panapp.ModifiedDate
FROM [$(P2RMIS)].dbo.[PRG_Panel_Proposals] opanapp INNER JOIN
[ViewApplication] app ON opanapp.Log_No = app.LogNumber INNER JOIN
[dbo].[ViewProgramMechanism] pm on app.ProgramMechanismId = pm.ProgramMechanismId
	inner join [dbo].[ClientAwardType] cat on pm.ClientAwardTypeId = cat.ClientAwardTypeId
	inner join [ViewSessionPanel] sp ON opanapp.Panel_ID = sp.LegacyPanelId INNER JOIN
[ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId AND sp.SessionPanelId = panapp.SessionPanelId
WHERE opanapp.Fundable_Range = 1 AND panapp.PanelApplicationId NOT IN (Select PanelApplicationId FROM ViewApplicationReviewStatus ars WHERE ReviewStatusId = 4) AND sp.SessionPanelId = @NewPanelId
--Add a record for command draft if doesn't exist
UNION ALL
SELECT panapp.PanelApplicationId, 3, panapp.ModifiedBy, panapp.ModifiedDate
FROM [$(P2RMIS)].dbo.[SS_Tracking] sst INNER JOIN
[ViewApplication] app ON sst.Log_No = app.LogNumber INNER JOIN
[dbo].[ViewProgramMechanism] pm on app.ProgramMechanismId = pm.ProgramMechanismId
	inner join [dbo].[ClientAwardType] cat on pm.ClientAwardTypeId = cat.ClientAwardTypeId
	inner join [ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId
WHERE sst.Client_Review = 1 AND panapp.PanelApplicationId NOT IN (Select PanelApplicationId FROM ViewApplicationReviewStatus ars WHERE ReviewStatusId = 3) AND panapp.SessionPanelId = @NewPanelId;
--PanelStage
--Insert an asynch stage for all panels in legacy P2RMIS
INSERT INTO PanelStage
                      (SessionPanelId, ReviewStageId, StageOrder, ViewSessionPanel.CreatedBy, ViewSessionPanel.CreatedDate, ViewSessionPanel.ModifiedBy, ViewSessionPanel.ModifiedDate)
SELECT     SessionPanelId, 1 AS Expr1, 1 AS Expr2, ViewSessionPanel.CreatedBy, ViewSessionPanel.CreatedDate, ViewSessionPanel.ModifiedBy, ViewSessionPanel.ModifiedDate
FROM         ViewSessionPanel
INNER JOIN ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId
INNER JOIN ClientMeeting ON ViewMeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId
Where SessionPanelId NOT IN
(Select SessionPanelId FROM PanelStage Where ReviewStageId = 1) AND ViewSessionPanel.SessionPanelId = @NewPanelId;

--Insert a synch stage for all panels in legacy P2RMIS with at least one score
INSERT INTO PanelStage
                      (SessionPanelId, ReviewStageId, StageOrder, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT     SessionPanelId, 2 AS Expr1, 2 AS Expr2, ViewSessionPanel.CreatedBy, ViewSessionPanel.CreatedDate, ViewSessionPanel.ModifiedBy, ViewSessionPanel.ModifiedDate
FROM         ViewSessionPanel 
INNER JOIN ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId
INNER JOIN ClientMeeting ON ViewMeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId
Where SessionPanelId NOT IN
(Select SessionPanelId FROM PanelStage Where ReviewStageId = 2) AND ViewSessionPanel.SessionPanelId = @NewPanelId;
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
WHERE NOT EXISTS (Select 'X' FROM ViewPanelStageStep WHERE PanelStageId = PanelStage.PanelStageId AND StepTypeId = CASE MTG_Phase_Member.Phase_ID WHEN 1 THEN 5 WHEN 2 THEN 6 ELSE 7 END) AND SessionPanel.SessionPanelId = @NewPanelId
UNION ALL
SELECT		PanelStage.PanelStageId, 8, 'Meeting', 1, SessionPanel.StartDate, SessionPanel.EndDate, NULL, NULL, SessionPanel.ModifiedBy, SessionPanel.ModifiedDate
FROM		ViewPanelStage PanelStage INNER JOIN
			ViewSessionPanel SessionPanel ON PanelStage.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
								 ViewMeetingSession ON SessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId INNER JOIN 
					 ClientMeeting ON ViewMeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId
WHERE PanelStage.ReviewStageId = 2 AND NOT EXISTS (Select 'X' FROM ViewPanelStageStep WHERE PanelStageId = PanelStage.PanelStageId AND StepTypeId = 8) AND SessionPanel.SessionPanelId = @NewPanelId;
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
AND NOT EXISTS (Select 'X' FROM ViewApplicationStage WHERE PanelApplicationId = PanelApplication.PanelApplicationId AND ReviewStageId = PanelStage.ReviewStageId) AND PanelApplication.SessionPanelId = @NewPanelId;

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
WHERE NOT EXISTS (Select 'X' FROM ApplicationStageStep WHERE DeletedFlag = 0 AND ApplicationStageId = ApplicationStage.ApplicationStageId AND PanelStageStepId = PanelStageStep.PanelStageStepId) AND SessionPanel.SessionPanelId = @NewPanelId;
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
	AND NOT EXISTS (Select 'X' FROM ApplicationStageStepDiscussion WHERE DeletedFlag = 0 AND ApplicationStageStepId = ApplicationStageStep.ApplicationStageStepId) AND PanelApplication.SessionPanelId = @NewPanelId;
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
WHERE NOT EXISTS (Select 'X' FROM ApplicationStageStepDiscussionComment WHERE DeletedFlag = 0 AND ApplicationStageStepDiscussionId = ApplicationStageStepDiscussion.ApplicationStageStepDiscussionId) AND PanelApplication.SessionPanelId = @NewPanelId;

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
	AND SessionPanel.SessionPanelId = @NewPanelId;
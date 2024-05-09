--------Adding Awardabbreviation
CREATE PROCEDURE [dbo].[uspSSData] 
	@ApplicationWorkflowId int,
	@PanelApplicationId int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	  -- New summary statement procedures can return up to 5 result sets
	--1: ApplicationInfo 2: CritiqueData 3: Generic 4: Generic 5: Generic
	SELECT [dbo].[ViewApplication].[LogNumber], ViewApplicationPersonnel.LastName AS PILastName, ViewApplicationPersonnel.FirstName AS PIFirstName, [dbo].[ViewApplication].[ApplicationTitle],
	ViewApplicationPersonnel.OrganizationName AS PIOrgName, [dbo].[ClientProgram].[ProgramDescription] As ProgramDesc, [dbo].[ClientProgram].[ProgramAbbreviation] AS ProgramAbrv, [dbo].[ViewProgramYear].[Year],
	[dbo].[ClientAwardType].[AwardAbbreviation] AS AwardAbrv,[dbo].[ClientAwardType].[AwardDescription] AS AwardDesc, [dbo].[ViewSessionPanel].[PanelName], case when [dbo].[ViewApplicationInfo].[InfoText] Is null then 'N/A' else [dbo].[ViewApplicationInfo].[InfoText] end AS Field1,
	ApplicationPersonnelAdmin.OrganizationName AS AdminOrgName, [dbo].[ClientProgram].[ProgramDescription], [dbo].[ViewMeetingSession].[StartDate] AS [PanelStartDate], [dbo].[ViewMeetingSession].[EndDate] AS [PanelEndDate],
	[dbo].[ViewApplication].[ProjectStartDate] AS [ProjectStartDate], [dbo].[ViewApplication].[ProjectEndDate] as [ProjectEndDate],
	cast(round(datediff(d, [dbo].[ViewApplication].[ProjectStartDate], [dbo].[ViewApplication].[ProjectEndDate])/365.0 *12,2) AS decimal(19,2)) AS Duration, [dbo].[ViewApplicationBudget].[TotalFunding] AS TotalBudget, [dbo].[ViewApplicationBudget].[DirectCosts],
	[dbo].[ViewApplicationBudget].[IndirectCosts], ViewProgramMechanism.ReceiptCycle,
	(SELECT dbo.ViewApplicationPersonnel.FirstName + ' ' + dbo.ViewApplicationPersonnel.LastName
	FROM dbo.ViewPanelApplication INNER JOIN
	dbo.ViewApplicationPersonnel ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplicationPersonnel.ApplicationId
	WHERE (dbo.ViewApplicationPersonnel.ClientApplicationPersonnelTypeId = 216) AND (dbo.ViewPanelApplication.PanelApplicationId = @PanelApplicationId))AS Field3,
	(SELECT ViewApplicationPersonnel_1.OrganizationName
	FROM dbo.ViewPanelApplication AS ViewPanelApplication_1 INNER JOIN
	dbo.ViewApplicationPersonnel AS ViewApplicationPersonnel_1 ON ViewPanelApplication_1.ApplicationId = ViewApplicationPersonnel_1.ApplicationId
	WHERE (ViewApplicationPersonnel_1.ClientApplicationPersonnelTypeId = 216) AND (ViewPanelApplication_1.PanelApplicationId = @PanelApplicationId))AS Field2,
	FORMAT(TotalBudget.TotalFunding, '###,###,###,##0', 'en-us') AS Field6, FORMAT(TotalBudget.TotalDirectCosts, '###,###,###,##0', 'en-us') AS Field4, FORMAT(TotalBudget.TotalIndirectCosts, '###,###,###,##0', 'en-us') AS Field5,dbo.ApplicationTopicCodes.TopicCode as Field7
	FROM [dbo].[ViewApplicationPersonnel] INNER JOIN
	[dbo].[ViewApplication] ON ViewApplicationPersonnel.ApplicationId = [dbo].[ViewApplication].[ApplicationId] INNER JOIN
	[dbo].[ViewProgramMechanism] ON [dbo].[ViewApplication].[ProgramMechanismId] = [dbo].[ViewProgramMechanism].[ProgramMechanismId] INNER JOIN
	[dbo].[ClientAwardType] ON [dbo].[ViewProgramMechanism].[ClientAwardTypeId] = [dbo].[ClientAwardType].[ClientAwardTypeId] INNER JOIN
	[dbo].[ViewPanelApplication] ON [dbo].[ViewApplication].[ApplicationId] = [dbo].[ViewPanelApplication].[ApplicationId] INNER JOIN
	[dbo].[ViewProgramYear] ON [dbo].[ViewProgramMechanism].[ProgramYearId] = [dbo].[ViewProgramYear].[ProgramYearId] INNER JOIN
	[dbo].[ViewSessionPanel] ON [dbo].[ViewPanelApplication].[SessionPanelId] = [dbo].[ViewSessionPanel].[SessionPanelId] INNER JOIN
	[dbo].[ClientProgram] ON [dbo].[ViewProgramYear].[ClientProgramId] = [dbo].[ClientProgram].[ClientProgramId] INNER JOIN
	[dbo].[ViewMeetingSession] ON [dbo].[ViewSessionPanel].[MeetingSessionId] = [dbo].[ViewMeetingSession].[MeetingSessionId] LEFT OUTER JOIN
	[dbo].[ClientApplicationPersonnelType] INNER JOIN
	[dbo].[ViewApplicationPersonnel] AS ApplicationPersonnelAdmin ON
	[dbo].[ClientApplicationPersonnelType].[ClientApplicationPersonnelTypeId] = ApplicationPersonnelAdmin.ClientApplicationPersonnelTypeId AND
	[dbo].[ClientApplicationPersonnelType].[ApplicationPersonnelTypeAbbreviation] = 'Admin-1' ON
	[dbo].[ViewApplication].[ApplicationId] = ApplicationPersonnelAdmin.ApplicationId LEFT OUTER JOIN
	[dbo].[ViewApplicationBudget] ON [dbo].[ViewApplication].[ApplicationId] = [dbo].[ViewApplicationBudget].[ApplicationId]
	OUTER APPLY [dbo].udfApplicationTotalBudget(ViewApplication.ApplicationId) TotalBudget LEFT OUTER JOIN 
	[dbo].[ApplicationTopicCodes] ON [dbo].[ViewApplication].[ApplicationId] = [dbo].[ApplicationTopicCodes].[ApplicationId]
	LEFT OUTER JOIN [dbo].[ViewApplicationInfo] ON [dbo].[ViewApplication].[ApplicationId] = [dbo].[ViewApplicationInfo].[ApplicationId] AND [dbo].[ViewApplicationInfo].[ClientApplicationInfoTypeId] = 1
	WHERE (ViewPanelApplication.PanelApplicationId = @PanelApplicationId) AND (ViewApplicationPersonnel.PrimaryFlag = 1)
	
	
	
	SELECT DISTINCT [dbo].[ViewPanelApplicationReviewerAssignment].[SortOrder] AS AssignmentOrder, [dbo].[ClientParticipantType].[ParticipantTypeName] AS PartTypeDesc,
	[dbo].[ClientParticipantType].[ParticipantTypeAbbreviation] AS PartType, [dbo].[ClientRole].[RoleName] AS Role, [dbo].[ClientRole].[RoleAbbreviation] AS RoleType,
	[dbo].[ClientElement].[ElementDescription] AS ElementDesc, [dbo].[ClientElement].[ClientElementId], [dbo].[ViewMechanismTemplateElement].[SortOrder] AS SortOrder,
	[dbo].[ViewMechanismTemplateElement].[SummarySortOrder] As ElementSortOrder, ISNULL([dbo].[ViewMechanismTemplateElement].[SummarySortOrder], [dbo].[ViewMechanismTemplateElement].[SortOrder]) AS ElementOrder, [dbo].[ViewMechanismTemplateElement].[ScoreFlag],
	[dbo].[ViewMechanismTemplateElement].[TextFlag], [dbo].[ViewMechanismTemplateElement].[OverallFlag], ApplicationWorkflowStepElementContent_1.Score,
	ApplicationWorkflowStepElementContent_1.ContentText AS ContentText, ISNULL ( ApplicationWorkflowStepElementContent_1.Abstain,0) AS Abstain, [dbo].[ViewApplicationTemplateElement].[PanelApplicationReviewerAssignmentId], PanelScore.AvgScore, ROUND(ROUND(PanelScore.StDev, 2), 1) AS StandardDeviation,
	[SummaryReviewerDescription].[CustomOrder] AS ReviewerOrder, [SummaryReviewerDescription].[DisplayName] AS ReviewerDisplayName, [dbo].[ViewApplicationTemplateElement].[DiscussionNoteFlag], ApplicationWorkflowStepElement_2.ApplicationTemplateElementId, ClientScoringScaleAdjectival.ScoreLabel AS AdjectivalScoreLabel, ClientScoringScale.ScoreType,
	ClientElement.ElementTypeId, ViewProgramMechanism.ReceiptCycle
	FROM [dbo].[ViewApplicationWorkflowStepElement] AS ApplicationWorkflowStepElement_2 INNER JOIN
	[dbo].[ViewApplicationTemplateElement] ON
	ApplicationWorkflowStepElement_2.ApplicationTemplateElementId = [dbo].[ViewApplicationTemplateElement].[ApplicationTemplateElementId] INNER JOIN
	[dbo].[ViewMechanismTemplateElement] ON
	[dbo].[ViewApplicationTemplateElement].[MechanismTemplateElementId] = [dbo].[ViewMechanismTemplateElement].[MechanismTemplateElementId] INNER JOIN
	[dbo].[ClientElement] ON [dbo].[ViewMechanismTemplateElement].[ClientElementId] = [dbo].[ClientElement].[ClientElementId] LEFT OUTER JOIN
	[dbo].[ViewApplicationWorkflowStepElementContent] AS ApplicationWorkflowStepElementContent_1 ON
	ApplicationWorkflowStepElement_2.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent_1.ApplicationWorkflowStepElementId LEFT OUTER JOIN
	[dbo].[ViewApplicationPersonnel] INNER JOIN
	[dbo].[ViewApplicationWorkflow] INNER JOIN
	[dbo].[ViewApplicationStage] ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
	[dbo].[ViewPanelApplication] ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
	[dbo].[ViewApplication] ON ViewPanelApplication.ApplicationId = [dbo].[ViewApplication].[ApplicationId] INNER JOIN
	[dbo].[ViewProgramMechanism] ON [dbo].[ViewApplication].[ProgramMechanismId] = [dbo].[ViewProgramMechanism].[ProgramMechanismId] INNER JOIN
	[dbo].[ClientAwardType] ON [dbo].[ViewProgramMechanism].[ClientAwardTypeId] = [dbo].[ClientAwardType].[ClientAwardTypeId] INNER JOIN
	[dbo].[ViewProgramYear] ON [dbo].[ViewProgramMechanism].[ProgramYearId] = [dbo].[ViewProgramYear].[ProgramYearId] INNER JOIN
	[dbo].[ViewSessionPanel] ON [dbo].[ViewPanelApplication].[SessionPanelId] = [dbo].[ViewSessionPanel].[SessionPanelId] INNER JOIN
	[dbo].[ClientProgram] ON [dbo].[ViewProgramYear].[ClientProgramId] = [dbo].[ClientProgram].[ClientProgramId] INNER JOIN
	[dbo].[ViewMeetingSession] ON [dbo].[ViewSessionPanel].[MeetingSessionId] = [dbo].[ViewMeetingSession].[MeetingSessionId] INNER JOIN
	(SELECT ApplicationWorkflow_1.ApplicationWorkflowId, ApplicationWorkflow_1.WorkflowId,
	ISNULL(ViewApplicationWorkflowLastStep.StepName, ViewApplicationWorkflowActiveStep.StepName) AS StepName,
	ISNULL(ViewApplicationWorkflowLastStep.ApplicationWorkflowStepId, ViewApplicationWorkflowActiveStep.ApplicationWorkflowStepId)
	AS ApplicationWorkflowStepId, ISNULL(ViewApplicationWorkflowLastStep.StepOrder, ViewApplicationWorkflowActiveStep.StepOrder) AS StepOrder
	FROM [dbo].[ViewApplicationWorkflow] AS ApplicationWorkflow_1 OUTER APPLY
	[dbo].[udfApplicationWorkflowActiveStep](ApplicationWorkflow_1.ApplicationWorkflowId) AS ViewApplicationWorkflowLastStep OUTER APPLY
	[dbo].[udfApplicationWorkflowLastStep](ApplicationWorkflow_1.ApplicationWorkflowId) AS ViewApplicationWorkflowActiveStep) AS AppWorkflowStep ON
	ViewApplicationWorkflow.ApplicationWorkflowId = AppWorkflowStep.ApplicationWorkflowId INNER JOIN
	[dbo].[ApplicationWorkflowStep] ON ViewApplicationWorkflow.ApplicationWorkflowId = [dbo].[ApplicationWorkflowStep].[ApplicationWorkflowId] AND
	AppWorkflowStep.ApplicationWorkflowStepId = [dbo].[ApplicationWorkflowStep].[ApplicationWorkflowStepId] ON
	ViewApplicationPersonnel.ApplicationId = [dbo].[ViewApplication].[ApplicationId] AND ViewApplicationPersonnel.PrimaryFlag = 1 ON
	ApplicationWorkflowStepElement_2.ApplicationWorkflowStepId = [dbo].[ApplicationWorkflowStep].[ApplicationWorkflowStepId] LEFT OUTER JOIN
	[dbo].[ClientApplicationPersonnelType] INNER JOIN
	[dbo].[ViewApplicationPersonnel] AS ApplicationPersonnelAdmin ON
	[dbo].[ClientApplicationPersonnelType].[ClientApplicationPersonnelTypeId] = ApplicationPersonnelAdmin.ClientApplicationPersonnelTypeId AND
	[dbo].[ClientApplicationPersonnelType].[ApplicationPersonnelTypeAbbreviation] = 'Admin-1' ON
	[dbo].[ViewApplication].[ApplicationId] = ApplicationPersonnelAdmin.ApplicationId LEFT OUTER JOIN
	[dbo].[ViewApplicationBudget] ON [dbo].[ViewApplication].[ApplicationId] = [dbo].[ViewApplicationBudget].[ApplicationId] LEFT OUTER JOIN
	[dbo].[ViewApplicationInfo] ON [dbo].[ViewApplication].[ApplicationId] = [dbo].[ViewApplicationInfo].[ApplicationId] AND [dbo].[ViewApplicationInfo].[ClientApplicationInfoTypeId] = 1 LEFT OUTER JOIN
	[dbo].[ViewPanelUserAssignment] INNER JOIN
	[dbo].[ViewPanelApplicationReviewerAssignment] ON
	[dbo].[ViewPanelUserAssignment].[PanelUserAssignmentId] = [dbo].[ViewPanelApplicationReviewerAssignment].[PanelUserAssignmentId] LEFT OUTER JOIN
	[dbo].[ClientParticipantType] ON [dbo].[ViewPanelUserAssignment].[ClientParticipantTypeId] = [dbo].[ClientParticipantType].[ClientParticipantTypeId] LEFT OUTER JOIN
	[dbo].[ClientRole] ON [dbo].[ViewPanelUserAssignment].[ClientRoleId] = [dbo].[ClientRole].[ClientRoleId] ON
	[dbo].[ViewApplicationTemplateElement].[PanelApplicationReviewerAssignmentId] = [dbo].[ViewPanelApplicationReviewerAssignment].[PanelApplicationReviewerAssignmentId] OUTER APPLY
	[dbo].[udfLastUpdatedCritiquePhaseAverage]([dbo].[ViewPanelApplication].[PanelApplicationId], [dbo].[ClientElement].ClientElementId) AS PanelScore OUTER APPLY
	--We use special joins in order to force NULLs to act as wildcards
	udfGetSummaryReviewerDescription(ViewProgramMechanism.ProgramMechanismId, ClientParticipantType.ClientParticipantTypeId, ClientRole.ClientRoleId, ViewPanelApplicationReviewerAssignment.SortOrder) SummaryReviewerDescription LEFT OUTER JOIN
	ClientScoringScale ON ApplicationWorkflowStepElement_2.ClientScoringId = ClientScoringScale.ClientScoringId LEFT OUTER JOIN
	ClientScoringScaleAdjectival ON ClientScoringScale.ClientScoringId = ClientScoringScaleAdjectival.ClientScoringId AND ApplicationWorkflowStepElementContent_1.Score = ClientScoringScaleAdjectival.NumericEquivalent
	WHERE (ViewApplicationWorkflow.ApplicationWorkflowId = @ApplicationWorkflowId) AND ([dbo].[ClientElement].[ElementIdentifier] <> 'DE') --Hack to exclude description for now
	Order by [dbo].[ViewMechanismTemplateElement].[SummarySortOrder], [dbo].[ViewMechanismTemplateElement].[SortOrder]
	
	SELECT cast(dbo.ApplicationText.ClientApplicationTextTypeId as varchar) as Field2, dbo.ApplicationText.BodyText as Field1
	FROM dbo.ApplicationText INNER JOIN
	dbo.ViewPanelApplication ON dbo.ApplicationText.ApplicationId = dbo.ViewPanelApplication.ApplicationId
	WHERE (dbo.ApplicationText.AbstractFlag = 1) AND (dbo.ViewPanelApplication.PanelApplicationId = @PanelApplicationId)
	
	SELECT PartnerPersonnel.FirstName as Field1, PartnerPersonnel.LastName as Field2,
	PartnerPersonnel.OrganizationName as Field3, AdminInfo.OrganizationName /* Admin_Org */ as Field4, FORMAT(PartnerBudget.DirectCosts, '###,###,###,##0', 'en-us') as Field5,
	FORMAT(PartnerBudget.IndirectCosts, '###,###,###,##0', 'en-us') AS Field6, FORMAT(PartnerBudget.TotalFunding, '###,###,###,##0', 'en-us') as Field7, PartnerInfo.InfoText as Field8, CAST(ROW_NUMBER() OVER (ORDER BY ViewApplication.LogNumber) AS varchar(10)) as Field9
	FROM dbo.ViewApplication INNER JOIN
	dbo.ViewPanelApplication ON dbo.ViewApplication.ApplicationId = dbo.ViewPanelApplication.ApplicationId LEFT OUTER JOIN
	ViewApplication PartnerApplication ON ViewApplication.ApplicationId = PartnerApplication.ParentApplicationId LEFT OUTER JOIN
	ViewApplicationPersonnel PartnerPersonnel ON PartnerApplication.ApplicationId = PartnerPersonnel.ApplicationId AND PartnerPersonnel.PrimaryFlag = 1 LEFT OUTER JOIN
	ViewApplicationBudget PartnerBudget ON PartnerApplication.ApplicationId = PartnerBudget.ApplicationId LEFT OUTER JOIN
	ViewApplicationInfo PartnerInfo ON PartnerApplication.ApplicationId = PartnerInfo.ApplicationId LEFT OUTER JOIN
	(Select PartnerAdmin.OrganizationName, PartnerAdmin.ApplicationId FROM dbo.ViewApplicationPersonnel PartnerAdmin INNER JOIN
	ClientApplicationPersonnelType AS PartnerAdminType ON PartnerAdmin.ClientApplicationPersonnelTypeId = PartnerAdminType.ClientApplicationPersonnelTypeId AND PartnerAdminType.ApplicationPersonnelTypeAbbreviation = 'Admin-1') AdminInfo ON PartnerApplication.ApplicationId = AdminInfo.ApplicationId
	WHERE (dbo.ViewPanelApplication.PanelApplicationId = @PanelApplicationId)
END
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspSSData] TO [NetSqlAzMan_Users]
    AS [dbo];
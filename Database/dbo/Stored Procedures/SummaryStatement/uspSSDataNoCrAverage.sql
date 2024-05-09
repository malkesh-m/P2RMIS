
----Adding Awardabbreviation
CREATE PROCEDURE [dbo].[uspSSDataNoCrAverage] 
	@ApplicationWorkflowId int,
	@PanelApplicationId int	
AS
BEGIN

	SET NOCOUNT ON;
	--This SS is intended for use with programs like CPRIT RES who do not 
     -- New summary statement procedures can return up to 5 result sets
	--1: ApplicationInfo  2: CritiqueData  3: Generic  4: Generic  5: Generic
	SELECT   [dbo].[ViewApplication].[LogNumber], ViewApplicationPersonnel.LastName AS PILastName, ViewApplicationPersonnel.FirstName AS PIFirstName, [dbo].[ViewApplication].[ApplicationTitle], 
                      ViewApplicationPersonnel.OrganizationName AS PIOrgName, [dbo].[ClientProgram].[ProgramDescription] As ProgramDesc, [dbo].[ClientProgram].[ProgramAbbreviation] AS ProgramAbrv, [dbo].[ViewProgramYear].[Year], 
                      [dbo].[ClientAwardType].[AwardAbbreviation] AS AwardAbrv,[dbo].[ClientAwardType].[AwardDescription] AS AwardDesc, [dbo].[ViewSessionPanel].[PanelName], [dbo].[ViewApplicationInfo].[InfoText] AS Field1, 
                      ApplicationPersonnelAdmin.OrganizationName AS AdminOrgName, [dbo].[ClientProgram].[ProgramDescription], [dbo].[ViewMeetingSession].[StartDate] AS [PanelStartDate], [dbo].[ViewMeetingSession].[EndDate] AS [PanelEndDate], 
					  [dbo].[ViewApplication].[ProjectStartDate] AS [ProjectStartDate], [dbo].[ViewApplication].[ProjectEndDate] as [ProjectEndDate],
                      cast(round(datediff(d, [dbo].[ViewApplication].[ProjectStartDate], [dbo].[ViewApplication].[ProjectEndDate])/365.0,2) AS decimal(19,2)) AS Duration, [dbo].[ViewApplicationBudget].[TotalFunding] AS TotalBudget, [dbo].[ViewApplicationBudget].[DirectCosts], 
                      [dbo].[ViewApplicationBudget].[IndirectCosts], ViewProgramMechanism.ReceiptCycle,
					   (SELECT     dbo.ViewApplicationPersonnel.FirstName + ' ' + dbo.ViewApplicationPersonnel.LastName 
                            FROM          dbo.ViewPanelApplication INNER JOIN
                                                   dbo.ViewApplicationPersonnel ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplicationPersonnel.ApplicationId
                            WHERE      (dbo.ViewApplicationPersonnel.ClientApplicationPersonnelTypeId = 216) AND (dbo.ViewPanelApplication.PanelApplicationId = @PanelApplicationId))AS Field3,
                          (SELECT     ViewApplicationPersonnel_1.OrganizationName 
                            FROM          dbo.ViewPanelApplication AS ViewPanelApplication_1 INNER JOIN
                                                   dbo.ViewApplicationPersonnel AS ViewApplicationPersonnel_1 ON ViewPanelApplication_1.ApplicationId = ViewApplicationPersonnel_1.ApplicationId
                            WHERE      (ViewApplicationPersonnel_1.ClientApplicationPersonnelTypeId = 216) AND (ViewPanelApplication_1.PanelApplicationId = @PanelApplicationId))AS Field2
	FROM              [dbo].[ViewApplicationPersonnel] INNER JOIN
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
                      [dbo].[ViewApplicationBudget] ON [dbo].[ViewApplication].[ApplicationId] = [dbo].[ViewApplicationBudget].[ApplicationId] LEFT OUTER JOIN
                      [dbo].[ViewApplicationInfo] ON [dbo].[ViewApplication].[ApplicationId] = [dbo].[ViewApplicationInfo].[ApplicationId] AND [dbo].[ViewApplicationInfo].[ClientApplicationInfoTypeId] = 1
	WHERE     (ViewPanelApplication.PanelApplicationId = @PanelApplicationId) AND (ViewApplicationPersonnel.PrimaryFlag = 1) 
	
	

	SELECT  DISTINCT   [dbo].[ViewPanelApplicationReviewerAssignment].[SortOrder] AS AssignmentOrder, [dbo].[ClientParticipantType].[ParticipantTypeName] AS PartTypeDesc, 
                      [dbo].[ClientParticipantType].[ParticipantTypeAbbreviation] AS PartType, [dbo].[ClientRole].[RoleName] AS Role, [dbo].[ClientRole].[RoleAbbreviation] AS RoleType, 
                      [dbo].[ClientElement].[ElementDescription] AS ElementDesc, [dbo].[ClientElement].[ClientElementId], [dbo].[ViewMechanismTemplateElement].[SortOrder] AS SortOrder,
	[dbo].[ViewMechanismTemplateElement].[SummarySortOrder] As ElementSortOrder,  ISNULL([dbo].[ViewMechanismTemplateElement].[SummarySortOrder], [dbo].[ViewMechanismTemplateElement].[SortOrder]) AS ElementOrder, [dbo].[ViewMechanismTemplateElement].[ScoreFlag], 
                      [dbo].[ViewMechanismTemplateElement].[TextFlag], [dbo].[ViewMechanismTemplateElement].[OverallFlag], ApplicationWorkflowStepElementContent_1.Score, 
                      ApplicationWorkflowStepElementContent_1.ContentText AS ContentText, ISNULL ( ApplicationWorkflowStepElementContent_1.Abstain,0) AS Abstain,  [dbo].[ViewApplicationTemplateElement].[PanelApplicationReviewerAssignmentId], PanelScore.AvgScore, ROUND(ROUND(PanelScore.StDev, 2), 1) AS StandardDeviation,
					  [SummaryReviewerDescription].[CustomOrder] AS ReviewerOrder, [SummaryReviewerDescription].[DisplayName] AS ReviewerDisplayName, [dbo].[ViewApplicationTemplateElement].[DiscussionNoteFlag], ApplicationWorkflowStepElement_2.ApplicationTemplateElementId, ClientScoringScaleAdjectival.ScoreLabel AS AdjectivalScoreLabel, ClientScoringScale.ScoreType,
					  ClientElement.ElementTypeId, ViewProgramMechanism.ReceiptCycle
FROM         [dbo].[ViewApplicationWorkflowStepElement] AS ApplicationWorkflowStepElement_2 INNER JOIN
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
                          (SELECT     ApplicationWorkflow_1.ApplicationWorkflowId, ApplicationWorkflow_1.WorkflowId, 
                                                   ISNULL(ViewApplicationWorkflowLastStep.StepName, ViewApplicationWorkflowActiveStep.StepName) AS StepName, 
                                                   ISNULL(ViewApplicationWorkflowLastStep.ApplicationWorkflowStepId, ViewApplicationWorkflowActiveStep.ApplicationWorkflowStepId) 
                                                   AS ApplicationWorkflowStepId, ISNULL(ViewApplicationWorkflowLastStep.StepOrder, ViewApplicationWorkflowActiveStep.StepOrder) AS StepOrder
                            FROM  [dbo].[ViewApplicationWorkflow] AS ApplicationWorkflow_1 OUTER APPLY        
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
                      [dbo].[udfLastUpdatedCritiquePhaseAverageWoCRs]([dbo].[ViewPanelApplication].[PanelApplicationId], [dbo].[ClientElement].ClientElementId) AS PanelScore OUTER APPLY
					  --We use special joins in order to force NULLs to act as wildcards
					  udfGetSummaryReviewerDescription(ViewProgramMechanism.ProgramMechanismId, ClientParticipantType.ClientParticipantTypeId, ClientRole.ClientRoleId, ViewPanelApplicationReviewerAssignment.SortOrder) SummaryReviewerDescription LEFT OUTER JOIN
					  ClientScoringScale ON ApplicationWorkflowStepElement_2.ClientScoringId = ClientScoringScale.ClientScoringId LEFT OUTER JOIN
					  ClientScoringScaleAdjectival ON ClientScoringScale.ClientScoringId = ClientScoringScaleAdjectival.ClientScoringId AND ApplicationWorkflowStepElementContent_1.Score = ClientScoringScaleAdjectival.NumericEquivalent
WHERE     (ViewApplicationWorkflow.ApplicationWorkflowId = @ApplicationWorkflowId) AND ([dbo].[ClientElement].[ElementIdentifier] <> 'DE') --Hack to exclude description for now
Order by [dbo].[ViewMechanismTemplateElement].[SummarySortOrder], [dbo].[ViewMechanismTemplateElement].[SortOrder]

SELECT     cast(dbo.ApplicationText.ClientApplicationTextTypeId as varchar) as Field2, dbo.ApplicationText.BodyText as Field1
FROM         dbo.ApplicationText INNER JOIN
                      dbo.ViewPanelApplication ON dbo.ApplicationText.ApplicationId = dbo.ViewPanelApplication.ApplicationId
WHERE      (dbo.ApplicationText.AbstractFlag = 1) AND (dbo.ViewPanelApplication.PanelApplicationId = @PanelApplicationId)
END
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspSSDataNoCrAverage] TO [NetSqlAzMan_Users]
    AS [dbo];
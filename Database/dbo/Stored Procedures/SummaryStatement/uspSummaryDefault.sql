/* Default query for Summary Statement Document Data */
CREATE PROCEDURE [dbo].[uspSummaryDefault]
	@ApplicationWorkflowId int = 0
AS
SELECT  DISTINCT   [dbo].[ViewApplication].[LogNumber], ViewApplicationPersonnel.LastName AS PILastName, ViewApplicationPersonnel.FirstName AS PIFirstName, [dbo].[ViewApplication].[ApplicationTitle], 
                      ViewApplicationPersonnel.OrganizationName AS PIOrgName, [dbo].[ClientProgram].[ProgramDescription] As ProgramDesc, [dbo].[ClientProgram].[ProgramAbbreviation] AS ProgramAbrv, [dbo].[ViewProgramYear].[Year], 
                      [dbo].[ClientAwardType].[AwardDescription] AS AwardDesc, [dbo].[ViewSessionPanel].[PanelName], [dbo].[ViewApplicationInfo].[InfoText] AS GrantID, 
                      ApplicationPersonnelAdmin.OrganizationName AS AdminOrgName, [dbo].[ClientProgram].[ProgramDescription], [dbo].[ViewMeetingSession].[StartDate], [dbo].[ViewMeetingSession].[EndDate], 
                      [dbo].[ViewApplication].[ProjectStartDate],[dbo].[ViewApplication].[ProjectEndDate],cast(round(datediff(d, [dbo].[ViewApplication].[ProjectStartDate], [dbo].[ViewApplication].[ProjectEndDate])/365.0,2) AS decimal(19,2)) AS Duration, [dbo].[ViewApplicationBudget].[TotalFunding] AS TotalBudget, [dbo].[ViewApplicationBudget].[DirectCosts], 
                      [dbo].[ViewApplicationBudget].[IndirectCosts], [dbo].[ViewPanelApplicationReviewerAssignment].[SortOrder] AS AssignmentOrder, [dbo].[ClientParticipantType].[ParticipantTypeName] AS PartTypeDesc, 
                      [dbo].[ClientParticipantType].[ParticipantTypeAbbreviation] AS PartType, [dbo].[ClientRole].[RoleName] AS Role, [dbo].[ClientRole].[RoleAbbreviation] AS RoleType, 
                      [dbo].[ClientElement].[ElementDescription], [dbo].[ClientElement].[ClientElementId], [dbo].[ViewMechanismTemplateElement].[SortOrder] AS ElementOrder, [dbo].[ViewMechanismTemplateElement].[ScoreFlag], 
                      [dbo].[ViewMechanismTemplateElement].[TextFlag], [dbo].[ViewMechanismTemplateElement].[OverallFlag], ApplicationWorkflowStepElementContent_1.Score, 
                      ApplicationWorkflowStepElementContent_1.ContentTextNoMarkup AS ContentText, ApplicationWorkflowStepElementContent_1.Abstain, [dbo].[ViewApplicationTemplateElement].[PanelApplicationReviewerAssignmentId], PanelScore.AvgScore AS AverageScore, ROUND(ROUND(PanelScore.StDev, 2), 1) AS StdDev,
					  [SummaryReviewerDescription].[CustomOrder] AS ReviewerOrder, [SummaryReviewerDescription].[DisplayName] AS ReviewerDisplayName, [dbo].[ViewApplicationTemplateElement].[DiscussionNoteFlag], ApplicationWorkflowStepElement_2.ApplicationWorkflowStepElementId, ClientScoringScaleAdjectival.ScoreLabel AS AdjectivalScoreLabel, ClientScoringScale.ScoreType,
					  Partner1.ApplicationId AS Partner1ApplicationId, Partner1.FirstName AS Partner1FirstName, Partner1.LastName AS Partner1LastName, Partner1.PerformingOrgName AS Partner1PerformingOrg, Partner1.ContractingOrgName AS Partner1ContractingOrg, Partner1.GrantId AS Partner1GrantId, Partner1.TotalFunding AS Partner1TotalFunding, Partner1.DirectCosts AS Partner1DirectCosts, Partner1.IndirectCosts AS Partner1IndirectCosts,
					  Partner2.ApplicationId AS Partner2ApplicationId, Partner2.FirstName AS Partner2FirstName, Partner2.LastName AS Partner2LastName, Partner2.PerformingOrgName AS Partner2PerformingOrg, Partner2.ContractingOrgName AS Partner2ContractingOrg, Partner2.GrantId AS Partner2GrantId, Partner2.TotalFunding AS Partner2TotalFunding, Partner2.DirectCosts AS Partner2DirectCosts, Partner2.IndirectCosts AS Partner2IndirectCosts,
					  ClientElement.ElementTypeId, ViewProgramMechanism.ReceiptCycle, ViewApplicationReviewStatus.ReviewStatusId
FROM         [dbo].[ViewApplicationWorkflowStepElement] AS ApplicationWorkflowStepElement_2 INNER JOIN
                      [dbo].[ViewApplicationTemplateElement] ON 
                      ApplicationWorkflowStepElement_2.ApplicationTemplateElementId = [dbo].[ViewApplicationTemplateElement].[ApplicationTemplateElementId] INNER JOIN
                      [dbo].[ViewMechanismTemplateElement] ON 
                      [dbo].[ViewApplicationTemplateElement].[MechanismTemplateElementId] = [dbo].[ViewMechanismTemplateElement].[MechanismTemplateElementId] INNER JOIN
                      [dbo].[ClientElement] ON [dbo].[ViewMechanismTemplateElement].[ClientElementId] = [dbo].[ClientElement].[ClientElementId] INNER JOIN
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
                      [dbo].[udfLastUpdatedCritiquePhaseAverage]([dbo].[ViewPanelApplication].[PanelApplicationId], [dbo].[ClientElement].ClientElementId) AS PanelScore OUTER APPLY
					  --We use special joins in order to force NULLs to act as wildcards
					  udfGetSummaryReviewerDescription(ViewProgramMechanism.ProgramMechanismId, ClientParticipantType.ClientParticipantTypeId, ClientRole.ClientRoleId, ViewPanelApplicationReviewerAssignment.SortOrder) SummaryReviewerDescription LEFT OUTER JOIN
					  ClientScoringScale ON ApplicationWorkflowStepElement_2.ClientScoringId = ClientScoringScale.ClientScoringId LEFT OUTER JOIN
					  ClientScoringScaleAdjectival ON ClientScoringScale.ClientScoringId = ClientScoringScaleAdjectival.ClientScoringId AND ApplicationWorkflowStepElementContent_1.Score = ClientScoringScaleAdjectival.NumericEquivalent OUTER APPLY
					  udfApplicationPartnerData(ViewApplication.ApplicationId, 1) AS Partner1 OUTER APPLY
					  udfApplicationPartnerData(ViewApplication.ApplicationId, 2) AS Partner2 INNER JOIN
					ViewApplicationReviewStatus ON ViewPanelApplication.PanelApplicationId = ViewApplicationReviewStatus.PanelApplicationId INNER JOIN
					ReviewStatus ON ViewApplicationReviewStatus.ReviewStatusId = ReviewStatus.ReviewStatusId
WHERE     (ViewApplicationWorkflow.ApplicationWorkflowId = @ApplicationWorkflowId) AND ([dbo].[ClientElement].[ElementIdentifier] <> 'DE') --Hack to exclude description for now
		AND ReviewStatus.ReviewStatusTypeId = 1
ORDER BY ElementOrder, [dbo].[ViewApplicationTemplateElement].[DiscussionNoteFlag], ReviewerOrder, AssignmentOrder
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspSummaryDefault] TO [NetSqlAzMan_Users]
    AS [dbo];
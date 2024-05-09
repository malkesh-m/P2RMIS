-- =============================================
-- Author: Pushpa Unnithan
-- Create date: 6/14/2016
-- Description: Storeprocedure to Create Client Report Final Scores
-- ===========================================
CREATE PROCEDURE [dbo].[uspReportScoresSummary] 
-- Add the parameters for the stored procedure here
@ProgramList varchar(4000),
@FiscalYearList varchar(4000),
--@CycleList varchar(4000),	
@PanelList varchar(4000)

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

-- Insert statements for procedure here
WITH ProgramParams(ClientProgramId)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
FiscalYearParams(FY)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
--CycleParams(Cycle) 
--AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@CycleList)),
PanelParams(PanelId)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))


SELECT      ClientProgram.ProgramAbbreviation, 
			ViewProgramYear.Year, 
			ViewSessionPanel.PanelAbbreviation, 
			ViewSessionPanel.PanelName, 
			ViewApplication.LogNumber, 
            ViewClientAwardType.AwardAbbreviation, 
			ViewClientAwardType.AwardDescription, 
			ViewApplicationWorkflowStepElementContent.Score, 
            ClientAssignmentType.AssignmentTypeId, 
			SUBSTRING(ViewUserInfo.FirstName, 1, 1) 
              + '.' + ' ' + ViewUserInfo.LastName As AllRev,
			ViewApplicationWorkflowStepElementContent.Score As AllScores ,
			ClientScoringScale.ScoreType,
					 ClientScoringScaleAdjectival.ScoreLabel,
			CASE WHEN ClientAssignmentType.AssignmentTypeId <> 6 
				THEN SUBSTRING(ViewUserInfo.FirstName, 1, 1) 
                  + '.' + ' ' + ViewUserInfo.LastName ELSE ' ' END AS Rev_Name, 
            CASE WHEN ClientAssignmentType.AssignmentTypeId = 6 
				THEN SUBSTRING(ViewUserInfo.FirstName, 1, 1) 
                  + ' ' + ViewUserInfo.LastName  ELSE ' ' END AS CR_Name, 
            CASE WHEN ClientAssignmentType.AssignmentTypeId <> 6 
				THEN ViewApplicationWorkflowStepElementContent.Score ELSE NULL END AS Rev_Score, 
            CASE WHEN ClientAssignmentType.AssignmentTypeId = 6 
				THEN ViewApplicationWorkflowStepElementContent.Score ELSE NULL END AS CR_Score, 
            ViewApplicationPersonnel.LastName, 
			ViewApplicationPersonnel.FirstName,
			ViewApplication.ApplicationTitle, 
			ViewApplicationPersonnel.OrganizationName, 
			ViewApplicationBudget.TotalFunding,  
            ViewPanelApplication.ReviewOrder,
			ViewPanelApplicationReviewerAssignment.SortOrder,
			ClientAssignmentType.AssignmentAbbreviation,
			CASE WHEN ClientApplicationInfoType.ClientID = 9 THEN ViewApplicationInfo.InfoText ELSE ' ' END AS InfoText,
            ViewProgramMechanism.ReceiptCycle, 
			ViewApplicationWorkflowStep.ResolutionDate

FROM            ClientApplicationInfoType INNER JOIN
                         ViewApplicationInfo ON ClientApplicationInfoType.ClientApplicationInfoTypeId = ViewApplicationInfo.ClientApplicationInfoTypeId RIGHT OUTER JOIN
                         ViewProgramPanel INNER JOIN
                         ClientProgram INNER JOIN
                         ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
                         ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
                         ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId INNER JOIN
                         ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
                         ViewProgramMechanism ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
                         ViewClientAwardType ON ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId LEFT OUTER JOIN
                         ViewApplicationBudget ON ViewApplication.ApplicationId = ViewApplicationBudget.ApplicationId INNER JOIN
                         ViewApplicationPersonnel ON ViewApplication.ApplicationId = ViewApplicationPersonnel.ApplicationId INNER JOIN
                         ClientApplicationPersonnelType ON ViewApplicationPersonnel.ClientApplicationPersonnelTypeId = ClientApplicationPersonnelType.ClientApplicationPersonnelTypeId INNER JOIN
                         ViewPanelApplicationReviewerAssignment ON ViewPanelApplication.PanelApplicationId = ViewPanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
                         ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
                         ViewPanelUserAssignment ON ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId AND 
                         ViewSessionPanel.SessionPanelId = ViewPanelUserAssignment.SessionPanelId INNER JOIN
                         ViewUserInfo ON ViewPanelUserAssignment.UserId = ViewUserInfo.UserID INNER JOIN
                         ViewApplicationTemplate ON ViewPanelApplication.ApplicationId = ViewApplicationTemplate.ApplicationId INNER JOIN
                         ViewMechanismTemplate ON ViewApplicationTemplate.MechanismTemplateId = ViewMechanismTemplate.MechanismTemplateId AND 
                         ViewApplication.ProgramMechanismId = ViewMechanismTemplate.ProgramMechanismId INNER JOIN
                         ViewMechanismTemplateElement ON ViewMechanismTemplate.MechanismTemplateId = ViewMechanismTemplateElement.MechanismTemplateId INNER JOIN
                         ViewApplicationWorkflow ON ViewApplicationTemplate.ApplicationTemplateId = ViewApplicationWorkflow.ApplicationTemplateId AND 
                         ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewApplicationWorkflow.PanelUserAssignmentId INNER JOIN
                         ViewApplicationWorkflowStep ON ViewApplicationWorkflow.ApplicationWorkflowId = ViewApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                         ViewApplicationTemplateElement ON ViewMechanismTemplateElement.MechanismTemplateElementId = ViewApplicationTemplateElement.MechanismTemplateElementId AND 
                         ViewApplicationTemplate.ApplicationTemplateId = ViewApplicationTemplateElement.ApplicationTemplateId INNER JOIN
                         ViewApplicationWorkflowStepElement ON ViewApplicationWorkflowStep.ApplicationWorkflowStepId = ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId AND 
                         ViewApplicationTemplateElement.ApplicationTemplateElementId = ViewApplicationWorkflowStepElement.ApplicationTemplateElementId ON ClientApplicationInfoType.ClientId = ClientProgram.ClientId AND 
                         ViewApplicationInfo.ApplicationId = ViewApplication.ApplicationId LEFT OUTER JOIN
                         ViewApplicationWorkflowStepElementContent ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId left join 
						 ClientScoringScaleAdjectival on ClientScoringScaleAdjectival.ClientScoringId = ViewApplicationWorkflowStepElement.ClientScoringId and ClientScoringScaleAdjectival.NumericEquivalent = ViewApplicationWorkflowStepElementContent.Score left join 
						 ClientScoringScale on ClientScoringScale.ClientScoringId = ClientScoringScaleAdjectival.ClientScoringId INNER JOIN
						ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
						FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
						--CycleParams ON CycleParams.Cycle =0 or ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
						PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId

WHERE     (ViewApplicationPersonnel.PrimaryFlag = 1) AND 
                      (ViewMechanismTemplateElement.OverallFlag = 1) AND (ViewMechanismTemplate.ReviewStageId = 1) AND (ViewApplicationWorkflowStep.StepTypeId in (5,6))
	--and LogNumber = 'BC180719'
	--and ClientProgram.ClientProgramId = 55
	--and ViewProgramYear.year = 2018
	--and ViewSessionPanel.SessionPanelId = 4454

	and ViewApplicationWorkflowStep.ResolutionDate in (
		select submitDate from (
			SELECT     ClientProgram.ClientProgramId,
					ViewProgramYear.year,
					ViewSessionPanel.SessionPanelId,
					ViewApplication.LogNumber,
					ViewUserInfo.UserInfoID, max( ViewApplicationWorkflowStep.ResolutionDate) as submitDate
			FROM      ViewProgramPanel INNER JOIN
                      ClientProgram INNER JOIN
                      ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId ON 
                      ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
                      ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
                      ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId INNER JOIN
                      ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
                      ViewProgramMechanism ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
                      ViewClientAwardType ON ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId LEFT OUTER JOIN
                      ViewApplicationBudget ON ViewApplication.ApplicationId = ViewApplicationBudget.ApplicationId INNER JOIN
                      ViewApplicationPersonnel ON ViewApplication.ApplicationId = ViewApplicationPersonnel.ApplicationId INNER JOIN
                      ClientApplicationPersonnelType ON 
                      ViewApplicationPersonnel.ClientApplicationPersonnelTypeId = ClientApplicationPersonnelType.ClientApplicationPersonnelTypeId INNER JOIN
                      ViewPanelApplicationReviewerAssignment ON ViewPanelApplication.PanelApplicationId = ViewPanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
                      ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
                      ViewPanelUserAssignment ON ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId AND 
                      ViewSessionPanel.SessionPanelId = ViewPanelUserAssignment.SessionPanelId INNER JOIN
                      ViewUserInfo ON ViewPanelUserAssignment.UserId = ViewUserInfo.UserID INNER JOIN
                      ViewApplicationTemplate ON ViewPanelApplication.ApplicationId = ViewApplicationTemplate.ApplicationId INNER JOIN
                      ViewMechanismTemplate ON ViewApplicationTemplate.MechanismTemplateId = ViewMechanismTemplate.MechanismTemplateId AND 
                      ViewApplication.ProgramMechanismId = ViewMechanismTemplate.ProgramMechanismId INNER JOIN
                      ViewMechanismTemplateElement ON ViewMechanismTemplate.MechanismTemplateId = ViewMechanismTemplateElement.MechanismTemplateId INNER JOIN
                      ViewApplicationWorkflow ON ViewApplicationTemplate.ApplicationTemplateId = ViewApplicationWorkflow.ApplicationTemplateId AND 
                      ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewApplicationWorkflow.PanelUserAssignmentId INNER JOIN
                      ViewApplicationWorkflowStep ON ViewApplicationWorkflow.ApplicationWorkflowId = ViewApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ViewApplicationTemplateElement ON 
                      ViewMechanismTemplateElement.MechanismTemplateElementId = ViewApplicationTemplateElement.MechanismTemplateElementId AND 
                      ViewApplicationTemplate.ApplicationTemplateId = ViewApplicationTemplateElement.ApplicationTemplateId INNER JOIN
                      ViewApplicationWorkflowStepElement ON 
                      ViewApplicationWorkflowStep.ApplicationWorkflowStepId = ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId AND 
                      ViewApplicationTemplateElement.ApplicationTemplateElementId = ViewApplicationWorkflowStepElement.ApplicationTemplateElementId LEFT OUTER JOIN
                      ViewApplicationInfo ON ViewApplication.ApplicationId = ViewApplicationInfo.ApplicationId LEFT OUTER JOIN
                      ViewApplicationWorkflowStepElementContent ON 
                      ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId 
					 INNER JOIN
					  ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
						FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
						--CycleParams ON CycleParams.Cycle =0 or ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
						PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId
		  
			WHERE     (ViewApplicationPersonnel.PrimaryFlag = 1) AND 
                      (ViewMechanismTemplateElement.OverallFlag = 1) AND (ViewMechanismTemplate.ReviewStageId = 1) AND (ViewApplicationWorkflowStep.StepTypeId in (5,6))
				--and LogNumber = 'BC180719'
				--and ClientProgram.ClientProgramId = 55
				--and ViewProgramYear.year = 2018
				--and ViewSessionPanel.SessionPanelId = 4454

			group by	ClientProgram.ClientProgramId,
						ViewProgramYear.year,
						ViewSessionPanel.SessionPanelId,
						ViewApplication.LogNumber,
						ViewUserInfo.UserInfoID

		) A1 )


	ORDER BY 
   IIF(ViewPanelApplication.ReviewOrder IS NULL, 1, 0),
   ViewPanelApplication.ReviewOrder, ViewApplication.LogNumber

END

 GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportScoresSummary] TO [NetSqlAzMan_Users]
    AS [dbo];                   
               
Create PROCEDURE [dbo].[uspReportPedigree]
--Add the parameters for the stored procedure here
@ProgramList varchar(4000),
@FiscalYearList varchar(4000),
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
	PanelParams(PanelId)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))
	

SELECT      dbo.ClientProgram.ClientId, dbo.ViewProgramYear.Year, dbo.ViewProgramMechanism.ReceiptCycle, dbo.ClientProgram.ProgramDescription, 
                      dbo.ClientProgram.ProgramAbbreviation, dbo.ViewClientAwardType.AwardDescription, dbo.ViewApplication.LogNumber, dbo.ViewApplication.ApplicationTitle, 
                      dbo.ViewApplicationPersonnel.FirstName, dbo.ViewApplicationPersonnel.LastName, dbo.ViewApplicationPersonnel.OrganizationName, 
                      dbo.ClientApplicationPersonnelType.ApplicationPersonnelType, dbo.ViewSessionPanel.PanelAbbreviation, dbo.ViewSessionPanel.PanelName, 
                      dbo.ViewProgramMechanism.ReceiptDeadline, dbo.ViewApplicationStage.ModifiedDate AS ReleasedDate, dbo.ViewSessionPanel.StartDate, 
                      dbo.ViewSessionPanel.EndDate, dbo.ViewPanelStageStep.StepName, dbo.ViewPanelStageStep.EndDate AS DueDate, 
                      dbo.ViewPanelApplicationReviewerAssignment.SortOrder, dbo.ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId,dbo.ViewApplicationWorkflowStep.ResolutionDate AS CritiqueSubmitted, 
                      dbo.ViewPanelUserRegistrationDocument.DateSigned, dbo.ViewPanelUserRegistrationDocument.ClientRegistrationDocumentId

    FROM         ViewSessionPanel INNER JOIN
                      ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId INNER JOIN
                      ClientProgram INNER JOIN
                      ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
                      Client ON ClientProgram.ClientId = Client.ClientID INNER JOIN
                      ViewProgramMechanism INNER JOIN
                      ViewClientAwardType ON ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId ON 
                      ViewProgramYear.ProgramYearId = ViewProgramMechanism.ProgramYearId AND Client.ClientID = ViewClientAwardType.ClientId INNER JOIN
                      ViewApplication ON ViewProgramMechanism.ProgramMechanismId = ViewApplication.ProgramMechanismId INNER JOIN
                      ViewApplicationPersonnel ON ViewApplication.ApplicationId = ViewApplicationPersonnel.ApplicationId INNER JOIN
                      ClientApplicationPersonnelType ON Client.ClientID = ClientApplicationPersonnelType.ClientId AND 
                      ViewApplicationPersonnel.ClientApplicationPersonnelTypeId = ClientApplicationPersonnelType.ClientApplicationPersonnelTypeId ON 
                      ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
                      ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId INNER JOIN
                      ViewPanelStage ON ViewSessionPanel.SessionPanelId = ViewPanelStage.SessionPanelId INNER JOIN
                      ViewPanelStageStep ON ViewPanelStage.PanelStageId = ViewPanelStageStep.PanelStageId INNER JOIN
                      ViewPanelApplicationReviewerAssignment ON ViewPanelApplication.PanelApplicationId = ViewPanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
                      ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId AND 
                      ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewApplicationWorkflow.PanelUserAssignmentId INNER JOIN
                      ViewApplicationWorkflowStep ON ViewApplicationWorkflow.ApplicationWorkflowId = ViewApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ViewApplicationWorkflowStepElement ON 
                      ViewApplicationWorkflowStep.ApplicationWorkflowStepId = ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
                      ViewPanelUserRegistration ON 
                      ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserRegistration.PanelUserAssignmentId LEFT OUTER JOIN
                      ViewPanelUserRegistrationDocument ON ViewPanelUserRegistration.PanelUserRegistrationId = ViewPanelUserRegistrationDocument.PanelUserRegistrationId
					   INNER JOIN ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
                      FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
					  PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId
WHERE     (ViewApplicationStage.ReviewStageId = 1)
GROUP BY ClientProgram.ClientId, ViewProgramYear.Year, ViewProgramMechanism.ReceiptCycle, ClientProgram.ProgramDescription, ClientProgram.ProgramAbbreviation, 
                      ViewClientAwardType.AwardDescription, ViewApplication.LogNumber, ViewApplication.ApplicationTitle, ViewApplicationPersonnel.FirstName, 
                      ViewApplicationPersonnel.LastName, ViewApplicationPersonnel.OrganizationName, ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.PanelName, 
                      ViewProgramMechanism.ReceiptDeadline, ViewApplicationStage.ModifiedDate, ViewSessionPanel.StartDate, ViewSessionPanel.EndDate, 
                      ViewPanelStageStep.StepName, ViewPanelStageStep.StepTypeId, ViewPanelStageStep.EndDate, ClientApplicationPersonnelType.ApplicationPersonnelType, 
                      ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId, ViewPanelApplicationReviewerAssignment.SortOrder, 
                      ViewApplicationWorkflowStep.ResolutionDate, ViewApplicationWorkflowStep.StepTypeId, ViewPanelUserRegistration.PanelUserRegistrationId, 
                      ViewPanelUserRegistrationDocument.DateSigned, ViewPanelUserRegistrationDocument.ClientRegistrationDocumentId
HAVING      (ViewPanelStageStep.StepTypeId = 5) AND (ClientApplicationPersonnelType.ApplicationPersonnelType = 'Principal Investigator') AND 
                      (ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId <> 3) AND (ViewApplicationWorkflowStep.StepTypeId = 5) AND 
                      (ViewPanelUserRegistrationDocument.ClientRegistrationDocumentId = 4)
ORDER BY ViewClientAwardType.AwardDescription

END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportPedigree] TO [NetSqlAzMan_Users]
    AS [dbo];                   
                    
                   
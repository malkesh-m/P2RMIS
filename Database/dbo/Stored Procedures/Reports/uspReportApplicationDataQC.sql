CREATE PROCEDURE [dbo].[uspReportApplicationDataQC]
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@CycleList varchar(4000)	
AS
BEGIN
SET NOCOUNT ON;
WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	CycleParams(Cycle) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@CycleList))
SELECT ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ProgramYear.Year, Application.LogNumber, 
               ProgramMechanism.ReceiptCycle, ClientAwardType.AwardAbbreviation, SessionPanel.PanelAbbreviation, ApplicationInfo.InfoText,
               ApplicationPi.FirstName, ApplicationPi.LastName, ApplicationPi.OrganizationName AS PiOrg, ApplicationCr.OrganizationName AS CrOrg,
			  -- case ApplicationReviewStatus.ReviewStatusId 
					--when 1 then 'ND'
					--else 
					convert(varchar(20),ROUND(CAST(AVG(ApplicationWorkflowStepElementContentOverall.Score) AS decimal(18,1)), 1))
			  -- end 
			  as OverallScore,
        
			   ROUND(ROUND(CAST(STDEV(ApplicationWorkflowStepElementContentOverall.Score) as decimal(18,1)), 2), 1) AS StandardDev,
			   
			   ApplicationBudget.DirectCosts,  ApplicationBudget.IndirectCosts, ApplicationBudget.TotalFunding, 
			   Application.ProjectStartDate, Application.ProjectEndDate, ApplicationBudget.Comments,
			   CASE (SELECT Stuff(
				  (SELECT N', ' + CAST(ReviewStatusId AS varchar(10)) FROM ViewApplicationReviewStatus WHERE PanelApplicationId = PanelApplication.PanelApplicationId AND ReviewStatusId IN (3,4) FOR XML PATH(''),TYPE)
				  .value('text()[1]','nvarchar(max)'),1,2,N'')) WHEN '3, 4' THEN 'Priority12' WHEN '3' THEN 'Priority1' WHEN '4' THEN 'Priority2' ELSE 'NoPriority' END AS SSPriority
FROM  ClientProgram INNER JOIN
               ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
               ViewProgramMechanism ProgramMechanism ON ProgramYear.ProgramYearId = ProgramMechanism.ProgramYearId INNER JOIN
			   ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN 
               FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN 
			   CycleParams ON CycleParams.Cycle = 0 OR ProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
               ViewApplication Application ON ProgramMechanism.ProgramMechanismId = Application.ProgramMechanismId INNER JOIN
               ViewPanelApplication PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId LEFT OUTER JOIN
			   ViewApplicationReviewStatus ApplicationReviewStatus ON PanelApplication.PanelApplicationId = ApplicationReviewStatus.PanelApplicationId INNER JOIN
               ViewSessionPanel SessionPanel ON PanelApplication.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
               ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId LEFT OUTER JOIN
               ViewApplicationInfo ApplicationInfo ON [Application].ApplicationId = ApplicationInfo.ApplicationId AND ApplicationInfo.ClientApplicationInfoTypeId = 1 LEFT OUTER JOIN
               ViewApplicationPersonnel ApplicationPi ON Application.ApplicationId = ApplicationPi.ApplicationId AND ApplicationPi.PrimaryFlag = 1 LEFT OUTER JOIN
			   (SELECT ClientApplicationPersonnelType.ApplicationPersonnelType, ViewApplicationPersonnel.* FROM dbo.ViewApplicationPersonnel 
				INNER JOIN dbo.ClientApplicationPersonnelType on ViewApplicationPersonnel.ClientApplicationPersonnelTypeID = ClientApplicationPersonnelType.ClientApplicationPersonnelTypeID
			   ) ApplicationCr ON Application.ApplicationId = ApplicationCr.ApplicationId AND ApplicationCr.ApplicationPersonnelType = 'Admin-1' LEFT OUTER JOIN
               ViewApplicationBudget ApplicationBudget ON [Application].ApplicationId = ApplicationBudget.ApplicationId LEFT OUTER JOIN
               ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId AND ApplicationStage.ReviewStageId = 2 LEFT OUTER JOIN
               ViewApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId LEFT OUTER JOIN
               ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId LEFT OUTER JOIN
				   (Select ApplicationWorkflowStepElementContent.Score, ApplicationWorkflowStepElement.ApplicationWorkflowStepId 
				   FROM ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement INNER JOIN 
				   ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
				   ViewMechanismTemplateElement MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
				   ViewApplicationWorkflowStepElementContent ApplicationWorkflowStepElementContent On ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId
				   WHERE MechanismTemplateElement.OverallFlag = 1) 
				ApplicationWorkflowStepElementContentOverall ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElementContentOverall.ApplicationWorkflowStepId
GROUP BY ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ProgramYear.Year, Application.LogNumber, 
               ProgramMechanism.ReceiptCycle, ClientAwardType.AwardAbbreviation, SessionPanel.PanelAbbreviation, ApplicationInfo.InfoText,
               ApplicationPi.FirstName, ApplicationPi.LastName, ApplicationPi.OrganizationName, ApplicationCr.OrganizationName,
			   --ApplicationReviewStatus.ReviewStatusId, 
			   ApplicationBudget.DirectCosts,  ApplicationBudget.IndirectCosts,
               ApplicationBudget.TotalFunding, Application.ProjectStartDate, Application.ProjectEndDate, ApplicationBudget.Comments, PanelApplication.PanelApplicationId    
END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportApplicationDataQC] TO [NetSqlAzMan_Users]
    AS [dbo];
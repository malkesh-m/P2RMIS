
-- =============================================
-- Author: Pushpa Unnithan
-- Create date: 3/26/2020
-- Description: Storeprocedure to Create Phases according to the selection of panels
-- =============================================
CREATE PROCEDURE [dbo].[uspReportPhasesbyPanels]

@ProgramList varchar(4000),
@FiscalYearList varchar(4000),
@PanelList varchar(4000)


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
	PanelParams(PanelId)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))

                        SELECT DISTINCT ViewWorkflowStep.StepName, ViewWorkflowStep.StepTypeId
FROM         PanelStage INNER JOIN
                      ViewWorkflow ON PanelStage.WorkflowId = ViewWorkflow.WorkflowId INNER JOIN
                      ViewWorkflowStep ON ViewWorkflow.WorkflowId = ViewWorkflowStep.WorkflowId INNER JOIN
                      ViewSessionPanel ON PanelStage.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
                      ViewProgramPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId INNER JOIN
                      ClientProgram INNER JOIN
                      ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId ON 
                      ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
		              ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
	FiscalYearParams ON FiscalYearParams.FY = 0 OR ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
	PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId
	ORDER by ViewWorkflowStep.StepTypeId


END	

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportPhasesbyPanels] TO [NetSqlAzMan_Users]
    AS [dbo];


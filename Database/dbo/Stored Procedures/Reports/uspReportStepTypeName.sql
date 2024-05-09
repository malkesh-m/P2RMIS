
--EXEC [dbo].[uspReportStepTypeName]  '51','2011','2275'
Create PROCEDURE [dbo].[uspReportStepTypeName] 
-- Add the parameters for the stored procedure here
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


SELECT distinct steptype.StepTypeId,StepType.StepTypeName,viewpanelstagestep.StepOrder , viewpanelstage.StageOrder




FROM ClientProgram INNER JOIN
ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
ViewProgramPanel ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId INNER JOIN
ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
viewpanelstage on viewpanelstage.SessionPanelId = ViewSessionPanel.SessionPanelId inner join 
ViewPanelStageStep on viewpanelstagestep.PanelStageId = viewpanelstage.PanelStageId join
steptype on steptype.steptypeId =viewpanelstagestep.StepTypeId 
join
ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId 

order by viewpanelstage.StageOrder,viewpanelstagestep.StepOrder

END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportStepTypeName] TO [NetSqlAzMan_Users]
    AS [dbo];
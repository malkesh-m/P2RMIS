CREATE View [dbo].[ViewApplicationRevStdev] AS
SELECT     ViewPanelApplication.ApplicationID, ViewApplicationRevStdev.AvgScore, ViewApplicationRevStdev.StDev
FROM         ViewPanelApplication INNER JOIN
                      ViewProgramPanel ON ViewPanelApplication.SessionPanelId = ViewProgramPanel.SessionPanelId INNER JOIN
                      ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
                      ViewProgramYear ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
                      ClientProgram ON ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId OUTER 
                      APPLY udfLastUpdatedCritiquePhaseAverageOverall(ViewPanelApplication.PanelApplicationId) AS ViewApplicationRevStdev




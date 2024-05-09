IF OBJECT_ID('dbo.View_AssignmentReleaseCheck', 'V') IS NOT NULL
    DROP VIEW dbo.View_AssignmentReleaseCheck
GO

CREATE VIEW [dbo].[View_AssignmentReleaseCheck]
AS
(
	-- Returns of assignments released for 2.0 : > 0 = assignments released
	SELECT MeetingSession.LegacySessionId AS Session_ID, ProgMech.LegacyAtmId AS ATM_ID
	FROM [$(DatabaseName)].dbo.ViewApplicationStage AppStage INNER JOIN
	[$(DatabaseName)].dbo.ViewPanelApplication PanApp ON AppStage.PanelApplicationId = PanApp.PanelApplicationId INNER JOIN
	[$(DatabaseName)].dbo.ViewSessionPanel SessionPanel ON PanApp.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
	[$(DatabaseName)].dbo.ViewMeetingSession MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId INNER JOIN
	[$(DatabaseName)].dbo.ViewApplication App ON PanApp.ApplicationId = App.ApplicationId INNER JOIN
	[$(DatabaseName)].dbo.ViewProgramMechanism ProgMech ON App.ProgramMechanismId = ProgMech.ProgramMechanismId
	WHERE AppStage.AssignmentVisibilityFlag = 1
)
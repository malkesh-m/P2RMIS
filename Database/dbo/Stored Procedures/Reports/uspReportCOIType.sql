CREATE PROCEDURE [dbo].[uspReportCOIType]
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@PanelList varchar(4000)
AS
BEGIN
	
	SET NOCOUNT ON;

    -- Insert statements for procedure here
WITH 
	ProgramParams(ClientProgramID) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))
SELECT     dbo.ViewProgramYear.Year, dbo.ClientProgram.ProgramAbbreviation, dbo.ClientProgram.ProgramDescription, dbo.ViewSessionPanel.PanelAbbreviation, 
                      dbo.ViewSessionPanel.PanelName, dbo.ViewApplication.LogNumber, dbo.ViewUserInfo.FirstName, dbo.ViewUserInfo.LastName, 
                      dbo.ViewPanelUserAssignment.LegacyParticipantId, dbo.ViewPanelApplicationReviewerExpertise.ClientExpertiseRatingId, 
                      dbo.ViewPanelApplicationReviewerExpertise.ExpertiseComments, dbo.ViewPanelApplicationReviewerExpertise.DeletedFlag, 
                      dbo.ViewPanelApplicationReviewerCoiDetail.ClientCoiTypeId, 
                      dbo.ClientCoiType.CoiTypeDescription
FROM  dbo.ViewPanelUserAssignment INNER JOIN
                      dbo.ViewPanelApplication ON dbo.ViewPanelUserAssignment.SessionPanelId = dbo.ViewPanelApplication.SessionPanelId INNER JOIN
                      dbo.ViewApplication ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId INNER JOIN
                      dbo.[User] ON dbo.ViewPanelUserAssignment.UserId = dbo.[User].UserID INNER JOIN
                      dbo.ViewPanelApplicationReviewerExpertise ON 
                      dbo.ViewPanelUserAssignment.PanelUserAssignmentId = dbo.ViewPanelApplicationReviewerExpertise.PanelUserAssignmentId AND 
                      dbo.ViewPanelApplication.PanelApplicationId = dbo.ViewPanelApplicationReviewerExpertise.PanelApplicationId INNER JOIN
                      dbo.ClientExpertiseRating ON dbo.ViewPanelApplicationReviewerExpertise.ClientExpertiseRatingId = dbo.ClientExpertiseRating.ClientExpertiseRatingId  INNER JOIN
                      dbo.ViewPanelApplicationReviewerCoiDetail ON 
                      dbo.ViewPanelApplicationReviewerExpertise.PanelApplicationReviewerExpertiseId = dbo.ViewPanelApplicationReviewerCoiDetail.PanelApplicationReviewerExpertiseId
                       INNER JOIN
                      dbo.ClientCoiType ON dbo.ViewPanelApplicationReviewerCoiDetail.ClientCoiTypeId = dbo.ClientCoiType.ClientCoiTypeId INNER JOIN
                      dbo.ViewSessionPanel ON dbo.ViewPanelUserAssignment.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId INNER JOIN
                      dbo.ViewUserInfo ON dbo.[User].UserID = dbo.ViewUserInfo.UserID INNER JOIN
                      dbo.ViewProgramPanel ON dbo.ViewSessionPanel.SessionPanelId = dbo.ViewProgramPanel.SessionPanelId INNER JOIN
                      dbo.ViewProgramYear ON dbo.ViewProgramPanel.ProgramYearId = dbo.ViewProgramYear.ProgramYearId INNER JOIN
                      dbo.ClientProgram ON dbo.ViewProgramYear.ClientProgramId = dbo.ClientProgram.ClientProgramId INNER JOIN
	ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
    FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
     PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId
WHERE     (dbo.ClientExpertiseRating.ConflictFlag = 1) 


END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportCOIType] TO [NetSqlAzMan_Users]
    AS [dbo];
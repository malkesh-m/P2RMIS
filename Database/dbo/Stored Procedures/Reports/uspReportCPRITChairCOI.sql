

-- =============================================================
-- Author: Sajeed Poudyal
-- Create date: 1/30/2020
-- Description: Stored procedure for Chair COI (PRMIS - 23699)
-- =============================================================

CREATE PROCEDURE [dbo].[uspReportCPRITChairCOI]
	(@ProgramList varchar(5000),
	@FiscalYearList varchar(5000),
	@PanelList varchar (5000))
AS
BEGIN

	SET NOCOUNT ON;

	WITH Programs(ProgramID) AS 
		(SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
	Years(FY) AS 
		(SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
	Panel(PA) AS 
		(SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))

		SELECT cp.ProgramDescription,
        cp.ProgramAbbreviation,
        vpy.[Year],
        vsp.PanelAbbreviation,
        va.LogNumber,
        cat.AwardAbbreviation,
        vap.OrganizationName AS PIOrganizationName,
        vui.FirstName,
        vui.LastName,
        vui.Lastname + ',' + vui.FirstName AS FullName,
        vui.Institution AS ReviewersOrgName,
        va.ApplicationTitle,
        CASE WHEN cer.RatingAbbreviation = 'COI' THEN 'Yes'
                        WHEN vpar.PanelApplicationReviewerExpertiseId IS NULL THEN 'N/A'
        ELSE 'No' END AS Coi,
        vpar.ExpertiseComments,
        cpt.ParticipantTypeAbbreviation,
        cr.RoleAbbreviation AS ParticipantRole,
        cct.CoiTypeName AS COIType
	FROM dbo.ClientProgram cp
        INNER JOIN dbo.ViewProgramYear vpy ON cp.ClientProgramId = vpy.ClientProgramId
        INNER JOIN dbo.ViewProgramPanel vpp ON vpy.ProgramYearId = vpp.ProgramYearId
        INNER JOIN dbo.ViewSessionPanel vsp ON vpp.SessionPanelId = vsp.SessionPanelId
        INNER JOIN dbo.ViewPanelUserAssignment vpu ON vsp.SessionPanelId = vpu.SessionPanelId
        INNER JOIN dbo.ViewUserInfo vui ON vpu.UserId = vui.UserID
        INNER JOIN dbo.ViewPanelApplication vpa ON vsp.SessionPanelId = vpa.SessionPanelId
        INNER JOIN dbo.ViewApplication va ON vpa.ApplicationId = va.ApplicationId
        INNER JOIN dbo.ViewUserSystemRole ON vpu.UserId = ViewUserSystemRole.UserID
        LEFT OUTER JOIN dbo.ViewPanelApplicationReviewerExpertise vpar ON vpu.PanelUserAssignmentId = vpar.PanelUserAssignmentId
                        AND vpa.PanelApplicationId = vpar.PanelApplicationId
        LEFT OUTER JOIN dbo.ClientExpertiseRating cer ON vpar.ClientExpertiseRatingId = cer.ClientExpertiseRatingId
                        AND cp.ClientId = cer.ClientId
        INNER JOIN dbo.ProgramMechanism pm ON va.ProgramMechanismId = pm.ProgramMechanismId
        INNER JOIN dbo.ClientAwardType cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId
        INNER JOIN dbo.ViewApplicationPersonnel vap ON va.ApplicationId = vap.ApplicationId
        INNER JOIN dbo.ClientParticipantType cpt ON vpu.ClientParticipantTypeId = cpt.ClientParticipantTypeId
        LEFT JOIN dbo.ClientRole cr ON vpu.ClientRoleId = cr.ClientRoleId
        LEFT OUTER JOIN dbo.ViewPanelApplicationReviewerCoiDetail rcd ON vpar.PanelApplicationReviewerExpertiseId = rcd.PanelApplicationReviewerExpertiseId
        LEFT OUTER JOIN dbo.ClientCoiType cct ON rcd.ClientCoiTypeId = cct.ClientCoiTypeId
        ---------------------------Parameters-----------------------------
        INNER JOIN Programs ON Programs.ProgramID = cp.ClientProgramId
        INNER JOIN Years ON Years.FY = vpy.[Year]
        INNER JOIN Panel ON Panel.PA = 0 OR Panel.PA = vsp.SessionPanelId
	WHERE (vap.PrimaryFlag = 1)
	AND (cpt.ReviewerFlag = 1 OR cpt.ElevatedChairpersonFlag = 1)
	ORDER BY va.LogNumber, vui.LastName, vsp.PanelAbbreviation

END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportCPRITChairCOI] TO [NetSqlAzMan_Users]
    AS [dbo];



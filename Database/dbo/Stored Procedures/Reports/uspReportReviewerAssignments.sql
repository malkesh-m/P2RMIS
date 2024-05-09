--+++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
-- AUthor: Dinh, Ngan
-- Date : 2/20/2019
-- Purpose: For Assignments - Individual Reviewers Report - PRMIS-23701

-- exec [uspReportReviewerAssignments] 47, 2013, 2712
--+++++++++++++++++++++++++++++++++++++++++++++++++++++++++--

CREATE PROCEDURE [dbo].[uspReportReviewerAssignments]
-- Add the parameters for the stored procedure here
@ProgramList varchar(4000),
@FiscalYearList varchar(4000),
@PanelList varchar(4000)

AS

BEGIN
-- SET NOCOUNT ON added to prevent extra result sets fromcom
-- interfering with SELECT statements.
SET NOCOUNT ON;

-- Insert statements for procedure here
WITH ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))


	SELECT			  ViewUserInfo.FirstName,ViewUserInfo.LastName,ViewUserInfo.SuffixText,
					  ViewUserInfo.Institution, ViewUserInfo.Department,
					  ViewApplication.LogNumber, 
					  ViewApplication.ApplicationTitle,
					  ViewClientAwardType.AwardAbbreviation AS Award, 
					  ViewPanelApplicationReviewerAssignment.SortOrder,
					  case 
						when ClientExpertiseRating.RatingDescription = 'Conflict of Interest' then 'COI'
						when ClientExpertiseRating.RatingDescription = 'High Expertise' then 'High'
						when ClientExpertiseRating.RatingDescription = 'Medium Expertise' then 'Med'
						when ClientExpertiseRating.RatingDescription = 'Low Expertise' then 'Low'
						when ClientExpertiseRating.RatingDescription = 'None' then 'None' 
						else ''
					  end as [Expertise Level],
					  UserInfo.Expertise,
					  ClientParticipantType.ParticipantTypeAbbreviation,
					  ParticipationMethod.ParticipationMethodLabel,
					  ClientRole.RoleName,
					  ClientAssignmentType.AssignmentAbbreviation AS Type, 
					  ViewSessionPanel.SessionPanelID, 
					  ViewSessionPanel.PanelAbbreviation , 
					  ViewSessionPanel.PanelName,
					  ClientProgram.ProgramAbbreviation, 
                      ViewProgramYear.Year, 
					  ClientProgram.ProgramDescription, 
					  ViewApplication.ParentApplicationId, 
                      ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId
	
	FROM    
             ViewApplication 
			 INNER JOIN ClientProgram 
			 INNER JOIN ViewProgramYear 
			 ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId 
			 INNER JOIN ViewProgramMechanism 
			 ON ViewProgramYear.ProgramYearId = ViewProgramMechanism.ProgramYearId 
			 ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId 
			 INNER JOIN ViewClientAwardType 
			 ON ClientProgram.ClientId = ViewClientAwardType.ClientId 
			 AND ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId	 
			 INNER JOIN ViewUserInfo 
			 INNER JOIN ViewPanelUserAssignment 
			 INNER JOIN ViewSessionPanel 
			 ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelID 
			 ON ViewUserInfo.UserID = ViewPanelUserAssignment.UserId  
			 INNER JOIN ViewPanelApplicationReviewerAssignment 
			 ON ViewPanelUserAssignment.PanelUserAssignmentId = ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId  
			 INNER JOIN ClientAssignmentType 
			 ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId  
			 INNER JOIN ViewPanelApplication 
			 ON ViewPanelApplicationReviewerAssignment.PanelApplicationId = ViewPanelApplication.PanelApplicationId 
			 AND ViewSessionPanel.SessionPanelID = ViewPanelApplication.SessionPanelId 
			 ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId
			 LEFT JOIN ViewPanelApplicationReviewerExpertise 
			 ON ViewPanelApplicationReviewerExpertise.PanelApplicationId = ViewPanelApplication.PanelApplicationId 
			 AND ViewPanelApplicationReviewerExpertise.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId 
		     LEFT JOIN ClientExpertiseRating 
			 ON ClientExpertiseRating.ClientExpertiseRatingId = ViewPanelApplicationReviewerExpertise.ClientExpertiseRatingId
			 INNER JOIN ClientParticipantType 
			 ON ClientParticipantType.ClientParticipantTypeId = ViewPanelUserAssignment.ClientParticipantTypeId  
			 INNER JOIN ParticipationMethod 
			 ON ParticipationMethod.ParticipationMethodId = ViewPanelUserAssignment.ParticipationMethodId 
			 LEFT JOIN  ClientRole 
			 ON ClientRole.ClientRoleId = ViewPanelUserAssignment.ClientRoleId		  
			 INNER JOIN UserInfo 
			 ON UserInfo.UserID = ViewUserInfo.UserID
			  
			 INNER JOIN
	
             ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
             FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
			 PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId
	
	ORDER BY	  ViewUserInfo.LastName ,  ViewUserInfo.FirstName, ViewApplication.LogNumber

END


GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportReviewerAssignments] TO [NetSqlAzMan_Users]
    AS [dbo];
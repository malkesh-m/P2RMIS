CREATE PROCEDURE [dbo].[uspReportReviewerExpertise] 
-- Add the parameters for the stored procedure here
@ProgramList varchar(4000),
@FiscalYearList varchar(4000),
@PanelList varchar(4000)

 
AS

BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @SROs TABLE
(
		Panel varchar(100),
		SROName varchar(200)
)

DECLARE @SROs2 TABLE
(
		Panel varchar(100),
		SROName varchar(200)
)

insert into @SROs
select  ViewSessionPanel.PanelName, ViewUserInfo.FirstName + ' ' + ViewUserInfo.LastName as SROName
from ClientProgram
join ViewProgramYear on ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId
join ViewProgramPanel on ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
join ViewSessionPanel on ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId
join ViewPanelUserAssignment on ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId
join dbo.ViewUserSystemRole ON dbo.ViewPanelUserAssignment.UserId = dbo.ViewUserSystemRole.UserID 
join dbo.ViewUserInfo ON dbo.ViewUserSystemRole.UserID = dbo.ViewUserInfo.UserID
where ViewUserSystemRole.SystemRoleId = 11
and ClientProgram.ClientProgramId = @ProgramList
and ViewProgramYear.Year = @FiscalYearList
and ViewSessionPanel.SessionPanelId = @PanelList

insert into @SROs2
select 
	Panel,
	stuff( (SELECT ', ' +  SROName
               FROM @SROs a2
               WHERE a1.Panel = a2.Panel
               FOR XML PATH(''), TYPE).value('.', 'varchar(max)')
            ,1,1,'')
       AS SROs
from @SROs a1
GROUP BY Panel

declare @SROName varchar(500)
select @SROName = Panel + '/ ' + SROName from @SROs2

SELECT    @SROName SROs, dbo.Client.ClientID, dbo.ClientProgram.ProgramDescription, dbo.ViewProgramYear.Year, dbo.ViewSessionPanel.PanelName, dbo.ViewApplication.LogNumber, 
                      CASE WHEN dbo.ClientExpertiseRating.RatingName IS NULL THEN 'No Selection' 
							WHEN dbo.ClientExpertiseRating.RatingName = 'Conflict of Interest' THEN 'COI'
							ELSE dbo.ClientExpertiseRating.RatingName 
					  END AS RatingName, 
                      CASE WHEN dbo.ClientExpertiseRating.RatingAbbreviation IS NULL 
                      THEN 'Not Selected' ELSE dbo.ClientExpertiseRating.RatingAbbreviation END AS RatingAbbreviation, dbo.ClientProgram.ClientProgramId, 
                      dbo.ViewPanelApplication.SessionPanelId, 
                      CASE WHEN dbo.ClientExpertiseRating.RatingAbbreviation = 'High' THEN 1 WHEN dbo.ClientExpertiseRating.RatingAbbreviation = 'Med' THEN 2 WHEN dbo.ClientExpertiseRating.RatingAbbreviation
                       = 'low' THEN 3 WHEN dbo.ClientExpertiseRating.RatingAbbreviation = 'None' THEN 4 WHEN dbo.ClientExpertiseRating.RatingAbbreviation = 'COI' THEN 5 WHEN dbo.ClientExpertiseRating.RatingAbbreviation
                       IS NULL THEN 7 ELSE ' ' END AS expertise, CASE WHEN dbo.ViewPanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId IS NULL 
                      THEN 1 ELSE dbo.ViewPanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId END AS Assignment, 
                      dbo.ClientParticipantType.ParticipantTypeAbbreviation
FROM         dbo.ViewPanelApplicationReviewerAssignment RIGHT OUTER JOIN
                      dbo.Client INNER JOIN
                      dbo.ClientProgram ON dbo.Client.ClientID = dbo.ClientProgram.ClientId INNER JOIN
                      dbo.ViewProgramYear ON dbo.ClientProgram.ClientProgramId = dbo.ViewProgramYear.ClientProgramId INNER JOIN
                      dbo.ClientParticipantType ON dbo.Client.ClientID = dbo.ClientParticipantType.ClientId INNER JOIN
                      dbo.ViewPanelApplication INNER JOIN
                      dbo.ViewSessionPanel ON dbo.ViewPanelApplication.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId INNER JOIN
                      dbo.ViewApplication ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId INNER JOIN
                      dbo.ViewPanelUserAssignment AS ViewPanelUserAssignment_1 ON dbo.ViewPanelApplication.SessionPanelId = ViewPanelUserAssignment_1.SessionPanelId ON 
                      dbo.ClientParticipantType.ClientParticipantTypeId = ViewPanelUserAssignment_1.ClientParticipantTypeId ON 
                      dbo.ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserAssignment_1.PanelUserAssignmentId AND 
                      dbo.ViewPanelApplicationReviewerAssignment.PanelApplicationId = dbo.ViewPanelApplication.PanelApplicationId LEFT OUTER JOIN
                      dbo.ViewPanelApplicationReviewerExpertise RIGHT OUTER JOIN
                      dbo.ClientExpertiseRating ON dbo.ViewPanelApplicationReviewerExpertise.ClientExpertiseRatingId = dbo.ClientExpertiseRating.ClientExpertiseRatingId ON 
                      ViewPanelUserAssignment_1.PanelUserAssignmentId = dbo.ViewPanelApplicationReviewerExpertise.PanelUserAssignmentId AND 
                      dbo.ViewPanelApplication.PanelApplicationId = dbo.ViewPanelApplicationReviewerExpertise.PanelApplicationId
WHERE dbo.ClientProgram.ClientProgramId = @ProgramList
AND dbo.ViewProgramYear.Year = @FiscalYearList
AND dbo.ViewSessionPanel.SessionPanelId = @PanelList  
AND dbo.ClientParticipantType.ReviewerFlag = 1 
AND dbo.ClientParticipantType.ConsumerFlag = 0
ORDER BY dbo.ViewApplication.LogNumber, expertise

END
GO


GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportReviewerExpertise] TO [NetSqlAzMan_Users]
    AS [dbo];
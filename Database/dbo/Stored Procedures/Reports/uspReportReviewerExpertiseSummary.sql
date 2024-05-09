CREATE PROCEDURE [dbo].[uspReportReviewerExpertiseSummary] 
-- Add the parameters for the stored procedure here
@ProgramList varchar(4000),
@FiscalYearList varchar(4000),
@PanelList varchar(4000)

 
AS
SET NOCOUNT ON;

BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @ReviewerExpertiseSummary TABLE
(
		RankExpertise varchar(10),
		Panel varchar(50),
		LogNumber varchar(50),
		RatingName varchar(40),
		ReviewerOrder varchar(10)
)


DECLARE @ReviewerExpertiseSummary2 TABLE
(
		Panel varchar(50),
		LogNumber varchar(50),
		RatingName varchar(40)
)


INSERT INTO @ReviewerExpertiseSummary
SELECT		-- Rank each SR for same LOgNumber group

Row_number() OVER (partition by ViewSessionPanel.SessionPanelId,lognumber ORDER BY LogNumber ASC) as ROW_NUMBER,
			dbo.ViewSessionPanel.SessionPanelId,
			dbo.ViewApplication.LogNumber, 
            Case when dbo.ClientExpertiseRating.RatingName is NULL then 'Not Selected' 
				 else dbo.ClientExpertiseRating.RatingName 
			end as RatingName,
			ViewPanelApplicationReviewerAssignment.SortOrder
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
WHERE     (dbo.ClientProgram.ClientProgramId = @ProgramList) AND (dbo.ViewProgramYear.Year = @FiscalYearList) AND (dbo.ViewSessionPanel.SessionPanelId = @PanelList) AND 
                      (dbo.ClientParticipantType.ReviewerFlag = 1) AND (dbo.ClientParticipantType.ConsumerFlag = 0)  AND 
                      (CASE WHEN dbo.ViewPanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId IS NULL 
                      THEN 1 ELSE dbo.ViewPanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId END > 1) AND 
                      (CASE WHEN dbo.ClientExpertiseRating.RatingAbbreviation = 'High' THEN 1 WHEN dbo.ClientExpertiseRating.RatingAbbreviation = 'Med' THEN 2 WHEN dbo.ClientExpertiseRating.RatingAbbreviation
                       = 'low' THEN 3 WHEN dbo.ClientExpertiseRating.RatingAbbreviation = 'None' THEN 4 WHEN dbo.ClientExpertiseRating.RatingAbbreviation = 'COI' THEN 5 WHEN dbo.ClientExpertiseRating.RatingAbbreviation
                       IS NULL THEN 7 ELSE ' ' END <> 5)

-- insert first two SRs of each LogNumber group
insert into @ReviewerExpertiseSummary2
select 	Panel,
		LogNumber,
		RatingName  from @ReviewerExpertiseSummary where RankExpertise < 3

-- Concat Rating for each LogNumber group for Summary 
select 
	Panel, LogNumber ,
	stuff( (SELECT '/'+RatingName 
               FROM @ReviewerExpertiseSummary2 a2
               WHERE a1.LogNumber = a2.LogNumber
               FOR XML PATH(''), TYPE).value('.', 'varchar(max)')
            ,1,1,'')
       AS Rating
from @ReviewerExpertiseSummary2 a1
GROUP BY Panel,LogNumber

END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportReviewerExpertiseSummary] TO [NetSqlAzMan_Users]
    AS [dbo];
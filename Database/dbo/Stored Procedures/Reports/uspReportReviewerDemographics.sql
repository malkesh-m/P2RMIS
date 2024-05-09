
-- =============================================
-- Author:		<Pushpa Unnithan>
-- Create date: <9/20/2018,,>
-- Description:	Storeprocedure to Create Reviewer Demographics Report,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspReportReviewerDemographics]
(@ProgramList varchar(5000),
@FiscalYearList varchar(5000),
@PanelList varchar (5000))

as

Begin
SET NOCOUNT ON;

--SET ANSI_NULLS ON
--GO
with Programs(ProgramID)
as
(SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
Years(FY)
as
(SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
Panel(PA)
as
(SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))


Select * from
(SELECT     dbo.ClientProgram.ClientProgramId, dbo.ClientProgram.ProgramAbbreviation, dbo.ViewProgramYear.Year, dbo.ClientProgram.ProgramDescription, 
                      dbo.ViewSessionPanel.PanelAbbreviation, dbo.ViewSessionPanel.SessionPanelId, dbo.ClientParticipantType.ParticipantTypeAbbreviation, dbo.UserInfo.FirstName, 
                      dbo.UserInfo.LastName,  CASE WHEN dbo.Ethnicity.Ethnicity IS NULL THEN 'Not Selected' ELSE dbo.Ethnicity.Ethnicity END AS Ethnicity, 
                      CASE WHEN dbo.Gender.Gender IS NULL THEN 'Not Selected' ELSE dbo.Gender.Gender END AS Gender, CASE WHEN dbo.AcademicRank.Rank IS NULL 
                      THEN 'Not Selected' ELSE dbo.AcademicRank.Rank END AS Rank, dbo.UserInfo.UserInfoID
FROM         dbo.ClientProgram INNER JOIN
                      dbo.ViewProgramYear ON dbo.ClientProgram.ClientProgramId = dbo.ViewProgramYear.ClientProgramId INNER JOIN
                      dbo.ViewProgramPanel ON dbo.ViewProgramYear.ProgramYearId = dbo.ViewProgramPanel.ProgramYearId INNER JOIN
                      dbo.ViewSessionPanel ON dbo.ViewProgramPanel.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId INNER JOIN
					  programs on programs.ProgramID= clientprogram.ClientProgramId join
					  Years on Years.FY =ViewProgramYear.Year join
					  panel on panel.PA =0 or panel.PA=ViewSessionPanel.SessionPanelId join					
					  dbo.ViewPanelUserAssignment ON dbo.ViewSessionPanel.SessionPanelId = dbo.ViewPanelUserAssignment.SessionPanelId INNER JOIN
                      dbo.UserInfo ON dbo.ViewPanelUserAssignment.UserId = dbo.UserInfo.UserID INNER JOIN
                      dbo.ClientParticipantType ON dbo.ViewPanelUserAssignment.ClientParticipantTypeId = dbo.ClientParticipantType.ClientParticipantTypeId LEFT OUTER JOIN
                      dbo.AcademicRank ON dbo.UserInfo.AcademicRankId = dbo.AcademicRank.AcademicRankId LEFT OUTER JOIN
                      dbo.Gender ON dbo.UserInfo.GenderId = dbo.Gender.GenderId LEFT OUTER JOIN
                      dbo.Ethnicity ON dbo.UserInfo.EthnicityId = dbo.Ethnicity.EthnicityId
WHERE                 (dbo.ClientParticipantType.ReviewerFlag = 1) AND (dbo.ClientParticipantType.ConsumerFlag = 0)

) t1

LEFT OUTER JOIN

 (select * from
 (SELECT  UserInfoID as 'ID', [M.D.] as 'MD', [Ph.D.] as 'Ph.D.', [M.D./Ph.D.] 'MD/PhD'
FROM         (SELECT     dbo.ViewUserDegree.UserInfoId, dbo.ViewUserDegree.DegreeId, dbo.Degree.DegreeName
FROM         dbo.ViewUserDegree INNER JOIN
                      dbo.Degree ON dbo.ViewUserDegree.DegreeId = dbo.Degree.DegreeId
WHERE     (dbo.Degree.DegreeId = 19 OR
                      dbo.Degree.DegreeId = 28 OR
                      dbo.Degree.DegreeId = 36) 
)p

PIVOT
(Count(DegreeID)
For DegreeName in ([M.D.], [Ph.D.], [M.D./Ph.D.])) as c) t2) t3

on t3.ID = t1.UserInfoID

end

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportReviewerDemographics] TO [NetSqlAzMan_Users]
    AS [dbo];

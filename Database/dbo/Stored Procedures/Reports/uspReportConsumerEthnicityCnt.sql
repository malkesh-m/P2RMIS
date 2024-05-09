
-- =============================================
-- Author:		<Pushpa Unnithan>
-- Create date: <1/31/2019,,>
-- Description:	Storeprocedure to Create Consumer Ethnicity Count Report,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspReportConsumerEthnicityCnt]
(@ProgramList varchar(5000),
@FiscalYearList varchar(5000))--,
--@List

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
(SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList))



SELECT     dbo.ClientProgram.ClientProgramId, dbo.ClientProgram.ProgramAbbreviation, dbo.ViewProgramYear.Year, dbo.ClientProgram.ProgramDescription, 
                      dbo.ClientParticipantType.ParticipantTypeAbbreviation, CASE WHEN dbo.Ethnicity.Ethnicity IS NULL THEN 'Not Selected' ELSE dbo.Ethnicity.Ethnicity END AS Ethnicity,
                       COUNT(dbo.UserInfo.UserInfoID) AS Count 
FROM         dbo.ClientProgram INNER JOIN
                      dbo.ViewProgramYear ON dbo.ClientProgram.ClientProgramId = dbo.ViewProgramYear.ClientProgramId INNER JOIN
                      dbo.ViewProgramPanel ON dbo.ViewProgramYear.ProgramYearId = dbo.ViewProgramPanel.ProgramYearId INNER JOIN
                      dbo.ViewSessionPanel ON dbo.ViewProgramPanel.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId INNER JOIN
					  programs on programs.ProgramID= clientprogram.ClientProgramId join
					  Years on Years.FY =ViewProgramYear.Year  join					
					  dbo.ViewPanelUserAssignment ON dbo.ViewSessionPanel.SessionPanelId = dbo.ViewPanelUserAssignment.SessionPanelId INNER JOIN
                      dbo.UserInfo ON dbo.ViewPanelUserAssignment.UserId = dbo.UserInfo.UserID INNER JOIN
                      dbo.ClientParticipantType ON dbo.ViewPanelUserAssignment.ClientParticipantTypeId = dbo.ClientParticipantType.ClientParticipantTypeId  LEFT OUTER JOIN
                      dbo.Ethnicity ON dbo.UserInfo.EthnicityId = dbo.Ethnicity.EthnicityId
WHERE                 (dbo.ClientParticipantType.ConsumerFlag = 1)
GROUP BY dbo.ClientProgram.ClientProgramId, dbo.ClientProgram.ProgramAbbreviation, dbo.ViewProgramYear.Year, dbo.ClientProgram.ProgramDescription, 
                      dbo.ClientParticipantType.ParticipantTypeAbbreviation, CASE WHEN dbo.Ethnicity.Ethnicity IS NULL THEN 'Not Selected' ELSE dbo.Ethnicity.Ethnicity END

end

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportConsumerEthnicityCnt] TO [NetSqlAzMan_Users]
    AS [dbo];


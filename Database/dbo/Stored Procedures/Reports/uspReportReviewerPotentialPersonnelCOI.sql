-- =============================================
-- Author:	Ginger Young
-- Create date: 6/27/2018
-- Description:	Used as source for report Reviewer Potential Personnel COI
-- ============================================= 



CREATE PROCEDURE [dbo].[uspReportReviewerPotentialPersonnelCOI] 
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(5000),
	@FiscalYearList varchar(5000),
	@PanelList varchar(5000)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

WITH Programs(ProgramID) as	(SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)), 
     Years(FY) as (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
     Panel(PA) as (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))

SELECT DISTINCT dbo.ViewSessionPanel.PanelName, dbo.ViewApplication.LogNumber, 
       ltrim(rtrim(dbo.ViewApplicationPersonnel.LastName)) + ', ' + ltrim(rtrim(ViewApplicationPersonnel.FirstName)) AS PersonName,
       ClientApplicationPersonnelType.ApplicationPersonnelType as PersonRole, 
       ltrim(rtrim(COIUserInfo.LastName)) + ', ' + ltrim(rtrim(COIUserInfo.FirstName)) as ReviewerName,
       COICPT.ParticipantTypeAbbreviation as ParticipantType, 
       dbo.ViewApplicationPersonnel.LastName, dbo.ViewApplicationPersonnel.FirstName, 
	   ClientProgram.ProgramDescription, ViewProgramYear.Year,
	   (SELECT TOP 1 SROUserInfo.LastName + ', ' + SROUserInfo.FirstName FROM 
	    dbo.ViewPanelUserAssignment as SROUserAssign join 
        dbo.ClientParticipantType AS SROClientParticipantType ON SROClientParticipantType.ClientParticipantTypeId = SROUserAssign.ClientParticipantTypeId 
           AND SROClientParticipantType.LegacyPartTypeId = 'SRA' INNER JOIN
        dbo.ViewUserInfo AS SROUserInfo ON SROUserInfo.UserID = SROUserAssign.UserId
		where SROUserAssign.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId) as SROName
FROM   dbo.ClientProgram INNER JOIN
        Programs on Programs.ProgramID= clientprogram.ClientProgramId JOIN
	    dbo.ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
        dbo.ViewProgramPanel ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
        dbo.ViewSessionPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId  INNER JOIN
        Years on Years.FY =ViewProgramYear.Year JOIN
        Panel on Panel.PA =0 or Panel.PA=ViewSessionPanel.SessionPanelId JOIN              
        dbo.ViewPanelUserAssignment AS COIUserAssign ON COIUserAssign.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
        dbo.ClientParticipantType AS COICPT ON COICPT.ClientParticipantTypeId = COIUserAssign.ClientParticipantTypeId and COICPT.ReviewerFlag = 1 INNER JOIN
        dbo.ViewUserInfo AS COIUserInfo ON COIUserInfo.UserID = COIUserAssign.UserId join
        dbo.ViewPanelApplication on viewpanelapplication.SessionPanelId =ViewProgramPanel.SessionPanelId join
        dbo.ViewApplicationPersonnel on ViewApplicationPersonnel.ApplicationId=ViewPanelApplication.ApplicationId join
        dbo.viewapplication on viewapplication.ApplicationId =viewpanelapplication.ApplicationId join
        dbo.ClientApplicationPersonnelType on ViewApplicationPersonnel.ClientApplicationPersonnelTypeId = dbo.ClientApplicationPersonnelType.ClientApplicationPersonnelTypeId
                  and ClientProgram.clientid=dbo.ClientApplicationPersonnelType.ClientId 
WHERE  (lower(ltrim(rtrim(COIUserInfo.LastName))) = lower(ltrim(rtrim(dbo.ViewApplicationPersonnel.LastName))))
	     and (lower((ltrim(rtrim(SUBSTRING(COIUserInfo.FirstName,1,1))))) = lower((ltrim(rtrim(SUBSTRING(dbo.ViewApplicationPersonnel.FirstName,1,1))))))
ORDER BY dbo.ViewSessionPanel.PanelName, dbo.ViewApplication.LogNumber, ViewApplicationPersonnel.LastName, ViewApplicationPersonnel.FirstName

END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportReviewerPotentialPersonnelCOI] TO [NetSqlAzMan_Users]
    AS [dbo];
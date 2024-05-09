-- =============================================
-- Author:	Ginger Young
-- Create date: 7/2/2018
-- Description:	Used as source for report Reviewer Potential Institutional COI
-- ============================================= 

CREATE PROCEDURE [dbo].[uspReportReviewerPotentialInstitutionalCOI] 
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(5000),
	@FiscalYearList varchar(5000),
	@PanelList varchar(5000)

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @Programs TABLE(
    ProgramID varchar(100)
);
INSERT INTO @Programs SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)

DECLARE @Years TABLE(
     FY varchar(10)
);
INSERT INTO @Years SELECT ParameterValue FROM dbo.SplitReportParameterInt(@FiscalYearList)

DECLARE @Panel TABLE(
    PA varchar(100)
);
INSERT INTO @Panel SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList)

-- Left side of the report
DECLARE @AppRev TABLE (
	lrowno int,
	lpanelid varchar(100),
	rpanelid varchar(100),
	panelname varchar(500),
	appid varchar(100),
	appinstitution varchar(1000),
	appname varchar(500),
	approle varchar(100),
	sroname varchar(500),
	fy varchar(10),
	programdescription varchar(1000),
	appfirstname varchar(500),
	applastname varchar(500),
	rrowno int,
	revinstitution varchar(1000),
	revname varchar(500),
	revptype varchar(100)
);

 --Right side of the report
DECLARE @RevInst TABLE(
   rowno int,
   panelid varchar(100),
   revinstitution varchar(1000),
   revname varchar(500),
   revptype varchar(100),
   revlastname varchar(500),
   revfirstname varchar(500)
);

   -- Join left and rightside
DECLARE @AppInst TABLE(
   rowno int,
   appid varchar(100),
   appinstitution varchar(1000),
   appname varchar(500),
   approle varchar(100),
   panelid varchar(100),
   PANELNAME varchar(500),
   sroname varchar(500),
   applastname varchar(500),
   appfirstname varchar(500),
   fy varchar(10),
   programdescription varchar(1000),
   lastname varchar(100),
   firstname varchar(100)
);

-- Update result set to update panel if null
DECLARE @AppRev2 TABLE(
	lrowno int,
	lpanelid varchar(100),
	rpanelid varchar(100),
	countLeftPanel int,
    countRightPanel int,
	panelname varchar(500),
	appid varchar(100),
	appinstitution varchar(1000),
	appname varchar(500),
	approle varchar(100),
	sroname varchar(500),
	fy varchar(10),
	programdescription varchar(1000),
	appfirstname varchar(500),
	applastname varchar(500),
	rrowno int,
	revinstitution varchar(1000),
	revname varchar(500),
	revptype varchar(100)
   );




INSERT INTO @AppInst(rowno, panelname, panelid, appid, appinstitution, appname, approle, applastname, appfirstname, programdescription, fy, sroname) ( 
(SELECT ROW_NUMBER() OVER (PARTITION BY PanelName ORDER BY PanelName, Institution,  LastName,  FirstName) as rowno, PanelName, SessionPanelId,
    LogNumber, Institution, ApplicantName, ApplicantRole, LastName, FirstName, ProgramDescription, FY, SROName  FROM (
SELECT DISTINCT
       dbo.ViewSessionPanel.PanelName as PanelName, dbo.ViewSessionPanel.SessionPanelId, 
	   ViewApplication.LogNumber as LogNumber, ViewApplicationPersonnel.OrganizationName as Institution,
       dbo.ViewApplicationPersonnel.LastName + ', ' + dbo.ViewApplicationPersonnel.FirstName AS ApplicantName,
       ClientApplicationPersonnelType.ApplicationPersonnelType as ApplicantRole, 
	   dbo.ViewApplicationPersonnel.LastName as LastName, dbo.ViewApplicationPersonnel.FirstName as FirstName,
	   clientprogram.ProgramDescription, yrs.FY, 
	   (SELECT TOP 1 SROUserInfo.LastName + ', ' + SROUserInfo.FirstName FROM 
	    dbo.ViewPanelUserAssignment as SROUserAssign join 
        dbo.ClientParticipantType AS SROClientParticipantType ON SROClientParticipantType.ClientParticipantTypeId = SROUserAssign.ClientParticipantTypeId 
           AND SROClientParticipantType.LegacyPartTypeId = 'SRA' INNER JOIN
        dbo.ViewUserInfo AS SROUserInfo ON SROUserInfo.UserID = SROUserAssign.UserId
		where SROUserAssign.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId) as SROName	
FROM   dbo.ClientProgram INNER JOIN
        @Programs as prgs on prgs.ProgramID = clientprogram.ClientProgramId JOIN
	    dbo.ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
        dbo.ViewProgramPanel ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
        dbo.ViewSessionPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId  INNER JOIN
        @Years as yrs on yrs.FY =ViewProgramYear.Year JOIN
        @Panel as pl on pl.PA =0 or pl.PA=ViewSessionPanel.SessionPanelId JOIN              
        dbo.ViewPanelUserAssignment AS PIUserAssign ON PIUserAssign.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
        dbo.ViewUserInfo AS PIUserInfo ON PIUserInfo.UserID = PIUserAssign.UserId join
        dbo.ViewPanelApplication on viewpanelapplication.SessionPanelId =ViewProgramPanel.SessionPanelId join
        dbo.ViewApplicationPersonnel on ViewApplicationPersonnel.ApplicationId=ViewPanelApplication.ApplicationId join
        dbo.viewapplication on viewapplication.ApplicationId =viewpanelapplication.ApplicationId join
        dbo.ClientApplicationPersonnelType on ViewApplicationPersonnel.ClientApplicationPersonnelTypeId = dbo.ClientApplicationPersonnelType.ClientApplicationPersonnelTypeId
                  and ClientProgram.clientid=dbo.ClientApplicationPersonnelType.ClientId and ViewApplicationPersonnel.PrimaryFlag = 1
WHERE ViewApplicationPersonnel.OrganizationName is not null 
  and RTRIM(ViewApplicationPersonnel.OrganizationName) != ''
  and dbo.ViewApplicationPersonnel.LastName is not null) as tbl1))


INSERT INTO @RevInst(rowno, panelid, revinstitution, revname, revptype, revlastname, revfirstname)
SELECT ROW_NUMBER() OVER (PARTITION BY PanelName ORDER BY PanelName, Institution,  LastName,  FirstName) as rowno, panelid,
    Institution, ReviewerName, pt, LastName, FirstName FROM (
SELECT DISTINCT  dbo.ViewSessionPanel.SessionPanelId as panelid, RevUserInfo.Institution, RevUserInfo.LastName + ', ' + RevUserInfo.FirstName AS ReviewerName,
	   RevCPT.ParticipantTypeAbbreviation as pt, RevUserInfo.LastName as LastName, RevUserInfo.FirstName as FirstName, PanelName
FROM   dbo.ClientProgram INNER JOIN
        @Programs as prgs on prgs.ProgramID= clientprogram.ClientProgramId JOIN
	    dbo.ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
        dbo.ViewProgramPanel ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
        dbo.ViewSessionPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId  INNER JOIN
        @Years as yrs on yrs.FY =ViewProgramYear.Year JOIN
        @Panel as pl on pl.PA =0 or pl.PA=ViewSessionPanel.SessionPanelId JOIN              
        dbo.ViewPanelUserAssignment AS RevUserAssign ON RevUserAssign.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
        dbo.ClientParticipantType AS RevCPT ON RevCPT.ClientParticipantTypeId = RevUserAssign.ClientParticipantTypeId and RevCPT.ReviewerFlag = 1 
		 INNER JOIN dbo.ViewUserInfo AS RevUserInfo ON RevUserInfo.UserID = RevUserAssign.UserId 
WHERE RevUserInfo.Institution is not null and rtrim(RevUserInfo.Institution) != ''
   AND RevUserInfo.LastName is not null
) as tbl2


insert into @AppRev(lrowno,lpanelid,rpanelid,PANELNAME,appid, appinstitution, appname, approle, sroname,fy,programdescription,appfirstname,applastname,rrowno,revinstitution,revname,revptype)
SELECT tleft.rowno as lrowno,tleft.panelid,tright.panelid, tleft.panelname, tleft.appid as appid, tleft.appinstitution, tleft.appname, tleft.approle, tleft.sroname,
   tleft.fy, tleft.programdescription, tleft.appfirstname, tleft.applastname, tright.rowno as rrowno, tright.revinstitution, tright.revname, tright.revptype
FROM @AppInst as tleft FULL OUTER JOIN 
@RevInst as tright on tleft.panelid = tright.panelid and tleft.rowno = tright.rowno


insert into @AppRev2(lrowno,lpanelid,rpanelid,countLeftPanel,countRightPanel, PANELNAME,appid, appinstitution, appname, approle, sroname,fy,programdescription,appfirstname,applastname,rrowno,revinstitution,revname,revptype)
SELECT 
case
when panelname is not null then lrowno 
else rrowno end as lrowno, lpanelid,rpanelid, count(lpanelid) leftpanel,count(rpanelid) rightpanel,panelname
,appid, appinstitution, appname, approle, sroname,fy,programdescription,appfirstname,applastname,rrowno,revinstitution,revname,revptype

FROM @AppRev 
group by lrowno, lpanelid,rpanelid, panelname,appid, appinstitution, appname, approle, sroname,fy,programdescription,appfirstname,applastname,rrowno,revinstitution,revname,revptype


UPDATE @AppRev2 
SET lpanelid =  CASE  
                       WHEN lpanelid is NULL THEN rpanelid
                        ELSE lpanelid
                    END,
	rpanelid =  CASE  
                       WHEN rpanelid is NULL THEN lpanelid
                        ELSE rpanelid
                    END

select *
from @AppRev2 order by lpanelid, lrowno 

END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportReviewerPotentialInstitutionalCOI] TO [NetSqlAzMan_Users]
    AS [dbo];
Create PROCEDURE [dbo].[uspReportPanelAssignmentQCNew]
-- Add the parameters for the stored procedure here
@ProgramList varchar(4000),
@FiscalYearList varchar(4000),
@CycleList varchar(4000)

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

-- Insert statements for procedure here

			with 
			Programs(ProgramID) as (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
			Years(FY) as (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
			CycleParams(Cycle) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@CycleList)),		
		
			data_set([App ID],[Award],[PI],[PI Organization Status],[Proposal],[Withdrawn],[SessionPanelID],[Panel],[Reviewer Name],SortOrder,
					 [Type],[COI],ReceiptCycle,ProgramAbbreviation,year,ProgramDescription,ParentApplicationId,ClientAssignmentTypeId)
			AS
			(SELECT   ViewApplication.LogNumber AS [App ID], 
					  ViewClientAwardType.AwardAbbreviation AS Award, 
                      ViewApplicationPersonnelPrimary.LastName + ',' + ' ' + ViewApplicationPersonnelPrimary.FirstName AS PI, 
                      ViewApplicationPersonnelPrimary.OrganizationName AS [PI Organization Status], 
					  ComplianceStatus.ComplianceStatusLabel AS Proposal, 
                      ViewApplication.WithdrawnDate AS Withdrawn, 
					  ViewSessionPanel.SessionPanelID, 
					  ViewSessionPanel.PanelAbbreviation , 
					  LEFT(ViewUserInfo.FirstName, 1) + '. ' + ViewUserInfo.LastName AS [Reviewer Name], 
					  ViewPanelApplicationReviewerAssignment.SortOrder, 
					  ClientAssignmentType.AssignmentAbbreviation AS Type, 
                      LEFT(ViewUserInfo.FirstName, 1) + '. ' + ViewUserInfo.LastName AS COI, 
					  ViewProgramMechanism.ReceiptCycle, 
					  ClientProgram.ProgramAbbreviation, 
                      ViewProgramYear.Year, 
					  ClientProgram.ProgramDescription, 
					  dbo.ViewApplication.ParentApplicationId, 
                      dbo.ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId
FROM         dbo.ApplicationCompliance INNER JOIN
             dbo.ViewApplication INNER JOIN
             dbo.ClientProgram INNER JOIN
             ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
			 Programs on Programs.ProgramID= clientprogram.ClientProgramId join
			 Years on Years.FY =ViewProgramYear.Year join
			 ViewProgramMechanism ON ViewProgramYear.ProgramYearId = ViewProgramMechanism.ProgramYearId ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
			 CycleParams ON CycleParams.Cycle =0 or ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
             dbo.ViewClientAwardType ON ClientProgram.ClientId = ViewClientAwardType.ClientId AND 
             ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId ON 
             ApplicationCompliance.ApplicationId = ViewApplication.ApplicationId INNER JOIN
             dbo.ComplianceStatus ON ApplicationCompliance.ComplianceStatusId = ComplianceStatus.ComplianceStatusId LEFT OUTER JOIN
             dbo.ViewApplicationPersonnelPrimary ON dbo.ViewApplication.ApplicationId = dbo.ViewApplicationPersonnelPrimary.ApplicationId LEFT OUTER JOIN
             dbo.ViewUserInfo INNER JOIN
             dbo.ViewPanelUserAssignment INNER JOIN
             dbo.ViewSessionPanel ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelID ON 
             ViewUserInfo.UserID = ViewPanelUserAssignment.UserId RIGHT OUTER JOIN
             dbo.ViewPanelApplicationReviewerAssignment ON 
             ViewPanelUserAssignment.PanelUserAssignmentId = ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId LEFT OUTER JOIN
             dbo.ClientAssignmentType ON 
             ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId RIGHT OUTER JOIN
             dbo.ViewPanelApplication ON ViewPanelApplicationReviewerAssignment.PanelApplicationId = ViewPanelApplication.PanelApplicationId AND 
             ViewSessionPanel.SessionPanelID = ViewPanelApplication.SessionPanelId ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId)

					
			SELECT  d2.[App ID],d2.Award, [PI],[PI Organization Status],[Proposal],[Withdrawn],A.[SessionPanelID],
					CASE WHEN A.Panel IS NULL 
                      THEN 'No Assigned Panel' ELSE A.Panel END AS Panel,
					ReceiptCycle,[Reviewer Name],SortOrder,
					ProgramAbbreviation,year, ProgramDescription,ParentApplicationId,ClientAssignmentTypeId,
					(select ( STUFF((SELECT (',' + [Type])  
							FROM data_set d1
							WHERE d1.[App ID] = d2.[App ID] and d1.Award = d2.Award and d1.ReceiptCycle = d2.ReceiptCycle and  [type]<> 'COI' 
							order by SortOrder asc
							FOR XML PATH (''))
							, 1, 1, '')  ) )   [Type],
					(select ( STUFF((SELECT (',' + [COI])  
							FROM data_set d3
							WHERE d3.[App ID] = d2.[App ID] and d3.Award = d2.Award and d3.ReceiptCycle = d2.ReceiptCycle and [type] = 'COI' 
							FOR XML PATH (''))
							, 1, 1, '')  ) )   [COI]
						
			from data_set d2
					

			LEFT JOIN
				(SELECT  
					ViewApplication.LogNumber as [App ID], 
					ViewSessionPanel.SessionPanelId,
					ViewSessionPanel.PanelAbbreviation AS Panel
				FROM   	dbo.ViewSessionPanel INNER JOIN            
						dbo.ViewPanelApplication on viewpanelapplication.SessionPanelId = ViewSessionPanel.SessionPanelId LEFT OUTER JOIN
						dbo.viewApplication on viewApplication.ApplicationId = ViewPanelApplication.ApplicationId)  as A 
						on A.[App ID] =  d2.[App ID]
					
			 order by d2.[App ID]

		End



		GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportPanelAssignmentQCNew] TO [NetSqlAzMan_Users]
    AS [dbo];

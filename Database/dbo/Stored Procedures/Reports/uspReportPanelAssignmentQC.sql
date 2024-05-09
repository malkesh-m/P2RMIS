CREATE procedure [dbo].[uspReportPanelAssignmentQC]
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@CycleList varchar(4000),
	@PanelList varchar(4000)
As

Begin
	SET NOCOUNT ON;	
	If @PanelList = '0'
		Begin
			with 
			Programs(ProgramID) as (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
			Years(FY) as (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
			CycleParams(Cycle) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@CycleList)),		
		
			data_set([App ID],[Award],[PI],[PI Organization Status],[Proposal],[Withdrawn],[Panel],[Reviewer Name],SortOrder,[Type],[COI],ReceiptCycle,ProgramAbbreviation,year,ProgramDescription) as
		
			(SELECT  ViewApplication.LogNumber AS [App ID], ViewClientAwardType.AwardAbbreviation AS Award, ViewApplicationPersonnelPrimary.LastName + ',' + ' ' + ViewApplicationPersonnelPrimary.FirstName AS PI, 
			ViewApplicationPersonnelPrimary.OrganizationName AS [PI Organization Status], ComplianceStatus.ComplianceStatusLabel AS Proposal, ViewApplication.WithdrawnDate AS Withdrawn, 
			ViewSessionPanel.PanelAbbreviation AS Panel, LEFT(ViewUserInfo.FirstName, 1) + '. ' + ViewUserInfo.LastName AS [Reviewer Name], ViewPanelApplicationReviewerAssignment.SortOrder, 
			ClientAssignmentType.AssignmentAbbreviation AS Type, LEFT(ViewUserInfo.FirstName, 1) + '. ' + ViewUserInfo.LastName AS COI, ViewProgramMechanism.ReceiptCycle, ClientProgram.ProgramAbbreviation, 
			ViewProgramYear.Year, ClientProgram.ProgramDescription
			FROM ApplicationCompliance INNER JOIN
					ViewApplication INNER JOIN
					ClientProgram INNER JOIN
					ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
					programs on programs.ProgramID= clientprogram.ClientProgramId join
					Years on Years.FY =ViewProgramYear.Year join
					ViewProgramMechanism ON ViewProgramYear.ProgramYearId = ViewProgramMechanism.ProgramYearId ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
					CycleParams ON CycleParams.Cycle =0 or ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
					 
					
					ViewClientAwardType ON ClientProgram.ClientId = ViewClientAwardType.ClientId AND ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId ON 
					ApplicationCompliance.ApplicationId = ViewApplication.ApplicationId INNER JOIN					                        
					ComplianceStatus ON ApplicationCompliance.ComplianceStatusId = ComplianceStatus.ComplianceStatusId LEFT OUTER JOIN
					ViewUserInfo INNER JOIN
					ViewPanelUserAssignment INNER JOIN					
					ViewSessionPanel ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId ON ViewUserInfo.UserID = ViewPanelUserAssignment.UserId RIGHT OUTER JOIN						 
					ViewPanelApplicationReviewerAssignment ON ViewPanelUserAssignment.PanelUserAssignmentId = ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId LEFT OUTER JOIN
					ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId RIGHT OUTER JOIN
					ViewPanelApplication ON ViewPanelApplicationReviewerAssignment.PanelApplicationId = ViewPanelApplication.PanelApplicationId AND 
					ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId LEFT OUTER JOIN
					ViewApplicationPersonnelPrimary ON ViewPanelApplication.ApplicationId = ViewApplicationPersonnelPrimary.ApplicationId ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId)

			SELECT  [App ID],d2.Award, [PI],[PI Organization Status],[Proposal],[Withdrawn],[Panel],ReceiptCycle,[Reviewer Name],SortOrder Reviewer 
					,ProgramAbbreviation,year, ProgramDescription,
					(select ( STUFF((SELECT (',' + [Type])  
					FROM data_set d1
					WHERE d1.[App ID] = d2.[App ID] and d1.Award = d2.Award and d1.ReceiptCycle = d2.ReceiptCycle and  [type]<> 'COI'
					order by SortOrder asc
					FOR XML PATH (''))
					, 1, 1, '')  ) )   Type,

					(select ( STUFF((SELECT (',' + [COI])  
								FROM data_set d3
								WHERE d3.[App ID] = d2.[App ID] and d3.Award = d2.Award and d3.ReceiptCycle = d2.ReceiptCycle and [type] = 'COI'
								FOR XML PATH (''))
								, 1, 1, '')  ) )   [COI]
						
			 
					from data_set d2
					where  [type]<> 'COI'
 
					order by [App ID]
		End

		Else
			begin
				with 
				Programs(ProgramID) as (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
				Years(FY) as (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
				CycleParams(Cycle) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@CycleList)),		
				PanelParams(SessionPanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@PanelList)),

				data_set([App ID],[Award],[PI],[PI Organization Status],[Proposal],[Withdrawn],[Panel],[Reviewer Name],SortOrder,[Type],[COI],ReceiptCycle,ProgramAbbreviation,year,ProgramDescription) as
		
				(SELECT  ViewApplication.LogNumber AS [App ID], ViewClientAwardType.AwardAbbreviation AS Award, ViewApplicationPersonnelPrimary.LastName + ',' + ' ' + ViewApplicationPersonnelPrimary.FirstName AS PI, 
                         ViewApplicationPersonnelPrimary.OrganizationName AS [PI Organization Status], ComplianceStatus.ComplianceStatusLabel AS Proposal, ViewApplication.WithdrawnDate AS Withdrawn, 
                         ViewSessionPanel.PanelAbbreviation AS Panel, LEFT(ViewUserInfo.FirstName, 1) + '. ' + ViewUserInfo.LastName AS [Reviewer Name], ViewPanelApplicationReviewerAssignment.SortOrder, 
                         ClientAssignmentType.AssignmentAbbreviation AS Type, LEFT(ViewUserInfo.FirstName, 1) + '. ' + ViewUserInfo.LastName AS COI, ViewProgramMechanism.ReceiptCycle, ClientProgram.ProgramAbbreviation, 
                         ViewProgramYear.Year, ClientProgram.ProgramDescription
				FROM			 ApplicationCompliance INNER JOIN
                         ViewApplication INNER JOIN
                         ClientProgram INNER JOIN
                         ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
							programs on programs.ProgramID= clientprogram.ClientProgramId join
							Years on Years.FY =ViewProgramYear.Year join
                         ViewProgramMechanism ON ViewProgramYear.ProgramYearId = ViewProgramMechanism.ProgramYearId ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
                         CycleParams ON CycleParams.Cycle =0 or ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN  
						 ViewClientAwardType ON ClientProgram.ClientId = ViewClientAwardType.ClientId AND ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId ON 
                         
						 
						 ApplicationCompliance.ApplicationId = ViewApplication.ApplicationId INNER JOIN
						                         
						 ComplianceStatus ON ApplicationCompliance.ComplianceStatusId = ComplianceStatus.ComplianceStatusId LEFT OUTER JOIN
                         ViewUserInfo INNER JOIN
                         ViewPanelUserAssignment INNER JOIN
                         ViewSessionPanel ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId ON ViewUserInfo.UserID = ViewPanelUserAssignment.UserId RIGHT OUTER JOIN
						 PanelParams on ViewSessionPanel.SessionPanelId is NULL or ViewSessionPanel.SessionPanelId = PanelParams.SessionPanelId join
                         ViewPanelApplicationReviewerAssignment ON ViewPanelUserAssignment.PanelUserAssignmentId = ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId LEFT OUTER JOIN
                         ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId RIGHT OUTER JOIN
                         ViewPanelApplication ON ViewPanelApplicationReviewerAssignment.PanelApplicationId = ViewPanelApplication.PanelApplicationId AND 
                         ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId LEFT OUTER JOIN
                         ViewApplicationPersonnelPrimary ON ViewPanelApplication.ApplicationId = ViewApplicationPersonnelPrimary.ApplicationId ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId)

		
				SELECT  [App ID],d2.Award, [PI],[PI Organization Status],[Proposal],[Withdrawn],[Panel],ReceiptCycle,[Reviewer Name],SortOrder Reviewer 
						,ProgramAbbreviation,year, ProgramDescription,
						(select ( STUFF((SELECT (',' + [Type])  
								  FROM data_set d1
								  WHERE d1.[App ID] = d2.[App ID] and d1.Award = d2.Award and d1.ReceiptCycle = d2.ReceiptCycle and  [type]<> 'COI'
								  order by SortOrder asc
								  FOR XML PATH (''))
								 , 1, 1, '')  ) )   Type,
						(select ( STUFF((SELECT (',' + [COI])  
								  FROM data_set d3
								  WHERE d3.[App ID] = d2.[App ID] and d3.Award = d2.Award and d3.ReceiptCycle = d2.ReceiptCycle and [type] = 'COI'
								  FOR XML PATH (''))
								 , 1, 1, '')  ) )   [COI]						
			 
					 from data_set d2
					where  [type]<> 'COI' 
					order by [App ID]
	 
		End
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportPanelAssignmentQC] TO [NetSqlAzMan_Users]
    AS [dbo];
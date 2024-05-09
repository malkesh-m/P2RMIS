
-- =============================================
-- Author:		Alberto Catuche
-- Create date: 5/2016
-- Description:	Used as source for report Critique Past Due Notice
-- update date: 7/2018
--Update Description: change to optimize querie (PRMIS-24146)
-- =============================================

CREATE PROCEDURE [dbo].[uspReportOrderOfDiscussion] 
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@PanelList varchar(4000)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@ProgramList)), --@ProgramList 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), --@FiscalYearList 
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@PanelList)) --@PanelList 

	SELECT    DENSE_RANK() over (partition by ViewPanelApplication.SessionPanelId order by CASE WHEN Trgd_ds.ReviewStatusId IS NULL THEN ViewPanelApplication.ReviewOrder ElSE '9999' END, viewapplication.lognumber ) as Disp_Order,
			  ViewPanelApplication.ReviewOrder, ViewProgramYear.Year, ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ViewPanelApplication.SessionPanelId, 
			  ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.PanelName, ViewSessionPanel.StartDate, ViewSessionPanel.EndDate, ViewClientAwardType.AwardAbbreviation, 
			  ViewApplication.LogNumber, ViewApplication.ApplicationTitle, ViewApplicationPersonnel.LastName + ', ' + ViewApplicationPersonnel.FirstName AS PI_FullName, 
			  ViewApplicationPersonnel.OrganizationName, qrm_ds.Particip_Count, qrm_ds.Particip_COI_Count, coi_ds.COIName, aptr_ds.assgn_prtcp_role,
			  Trgd_ds.ReviewStatusId, CASE WHEN Trgd_ds.ReviewStatusId IS NULL THEN CAST(ViewPanelApplication.ReviewOrder AS nvarchar) ElSE 'ND' END ReviewStatusIdOrder
	FROM      ViewApplication INNER JOIN
			  ViewPanelApplication ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId INNER JOIN
			  ViewProgramMechanism ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
			  ViewProgramYear ON ViewProgramMechanism.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
			  ClientProgram ON ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
			  ViewSessionPanel ON ViewPanelApplication.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
			  ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId INNER JOIN
			  ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId INNER JOIN
			  ViewClientAwardType ON ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId INNER JOIN
			  ViewApplicationPersonnel ON ViewApplication.ApplicationId = ViewApplicationPersonnel.ApplicationId LEFT OUTER JOIN 

			  /*******************************************************************************/
					(SELECT     ViewPanelApplication.ApplicationId, ViewApplicationReviewStatus.ReviewStatusId, ViewPanelApplication.SessionPanelId
					FROM         ViewApplicationReviewStatus INNER JOIN
										  ViewPanelApplication ON ViewApplicationReviewStatus.PanelApplicationId = ViewPanelApplication.PanelApplicationId AND 
										  ViewApplicationReviewStatus.PanelApplicationId = ViewPanelApplication.PanelApplicationId AND 
										  ViewApplicationReviewStatus.PanelApplicationId = ViewPanelApplication.PanelApplicationId AND 
										  ViewApplicationReviewStatus.PanelApplicationId = ViewPanelApplication.PanelApplicationId
					WHERE     (ViewApplicationReviewStatus.ReviewStatusId = 1)) 
					Trgd_ds ON ViewPanelApplication.SessionPanelId = trgd_ds.SessionPanelId AND ViewPanelApplication.ApplicationId = trgd_ds.ApplicationId LEFT OUTER JOIN

			  /*******************************************************************************/
					(SELECT PT_CNT.SessionPanelId, PT_CNT.ApplicationId, PT_COI_CNT.Particip_COI_Count, PT_CNT.Particip_Count 
					FROM
					(SELECT    dbo.PanelUserAssignment.SessionPanelId, dbo.ViewApplication.ApplicationId, dbo.ViewApplication.LogNumber, Count(dbo.ViewPanelApplication.PanelApplicationId) AS Particip_Count
					FROM      ViewPanelApplicationReviewerExpertise INNER JOIN
							  PanelUserAssignment ON ViewPanelApplicationReviewerExpertise.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
							  ViewPanelApplication ON ViewPanelApplicationReviewerExpertise.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
							  ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
							  ClientExpertiseRating ON ViewPanelApplicationReviewerExpertise.ClientExpertiseRatingId = ClientExpertiseRating.ClientExpertiseRatingId INNER JOIN
							  ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
							  ClientRole ON PanelUserAssignment.ClientRoleId = ClientRole.ClientRoleId
					WHERE     (ClientParticipantType.ReviewerFlag = N'1') --AND (dbo.PanelUserAssignment.SessionPanelId = @PanelList )
					GROUP BY  dbo.PanelUserAssignment.SessionPanelId, dbo.ViewApplication.ApplicationId, dbo.ViewApplication.LogNumber) PT_CNT
					INNER JOIN
					(SELECT    dbo.PanelUserAssignment.SessionPanelId, dbo.ViewApplication.ApplicationId, dbo.ViewApplication.LogNumber, Count(dbo.ViewPanelApplication.ApplicationId) AS Particip_COI_Count
					FROM      dbo.ViewPanelApplicationReviewerAssignment INNER JOIN
							  dbo.ClientAssignmentType ON dbo.ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = dbo.ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
							  dbo.PanelUserAssignment ON dbo.ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = dbo.PanelUserAssignment.PanelUserAssignmentId INNER JOIN
							  dbo.ViewPanelApplication ON dbo.ViewPanelApplicationReviewerAssignment.PanelApplicationId = dbo.ViewPanelApplication.PanelApplicationId INNER JOIN
							  dbo.ClientExpertiseRating ON dbo.ClientAssignmentType.ClientId = dbo.ClientExpertiseRating.ClientId INNER JOIN
							  dbo.ViewApplication ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId
					WHERE     (dbo.ClientAssignmentType.AssignmentTypeId = 8) AND (dbo.ClientExpertiseRating.RatingAbbreviation = 'COI') --AND (dbo.PanelUserAssignment.SessionPanelId = @PanelList)
					GROUP BY  dbo.PanelUserAssignment.SessionPanelId, dbo.ViewApplication.ApplicationId, dbo.ViewApplication.LogNumber) PT_COI_CNT
					ON PT_CNT.SessionPanelId = PT_COI_CNT.SessionPanelId AND PT_CNT.ApplicationId = PT_COI_CNT.ApplicationId) 
			  QRM_DS ON ViewPanelApplication.SessionPanelId = qrm_ds.SessionPanelId AND ViewPanelApplication.ApplicationId = qrm_ds.ApplicationId LEFT OUTER JOIN
			  /*******************************************************************************/
					(SELECT t1.SessionPanelID, t1.ApplicationId, t1.LogNumber,
					  STUFF((SELECT DISTINCT '' + t2.FullName + '!'
							 FROM (SELECT     dbo.PanelUserAssignment.SessionPanelId, dbo.ViewPanelApplication.ApplicationId, dbo.ViewApplication.LogNumber, dbo.ViewUserInfo.FirstName+' '+dbo.ViewUserInfo.LastName AS FullName 
									FROM         dbo.ViewPanelApplicationReviewerAssignment INNER JOIN
												  dbo.ClientAssignmentType ON dbo.ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = dbo.ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
												  dbo.AssignmentType ON dbo.ClientAssignmentType.AssignmentTypeId = dbo.AssignmentType.AssignmentTypeId INNER JOIN
												  dbo.PanelUserAssignment ON dbo.ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = dbo.PanelUserAssignment.PanelUserAssignmentId INNER JOIN
												  dbo.ViewPanelApplication ON dbo.ViewPanelApplicationReviewerAssignment.PanelApplicationId = dbo.ViewPanelApplication.PanelApplicationId INNER JOIN
												  dbo.ViewApplication ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId INNER JOIN
												  dbo.ViewUserInfo ON dbo.PanelUserAssignment.UserId = dbo.ViewUserInfo.UserID INNER JOIN
												  dbo.ClientExpertiseRating ON dbo.ClientAssignmentType.ClientId = dbo.ClientExpertiseRating.ClientId INNER JOIN
												  dbo.ViewSessionPanel ON dbo.PanelUserAssignment.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId AND 
												  dbo.ViewPanelApplication.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId INNER JOIN
												  dbo.ViewProgramMechanism ON dbo.ViewApplication.ProgramMechanismId = dbo.ViewProgramMechanism.ProgramMechanismId INNER JOIN
												  dbo.ViewClientAwardType ON dbo.ViewProgramMechanism.ClientAwardTypeId = dbo.ViewClientAwardType.ClientAwardTypeId
									WHERE     (dbo.ClientAssignmentType.AssignmentTypeId = 8) AND (dbo.ClientExpertiseRating.RatingAbbreviation = 'COI') --AND (dbo.PanelUserAssignment.SessionPanelId = @PanelList)
								  ) t2
							 WHERE t1.SessionPanelID = t2.SessionPanelID and t1.ApplicationId=t2.ApplicationId
								FOR XML PATH(''), TYPE
								).value('(./text())[1]', 'NVARCHAR(MAX)') 
							,1,0,'') COIName
					FROM (SELECT     dbo.PanelUserAssignment.SessionPanelId, dbo.ViewPanelApplication.ApplicationId, dbo.ViewApplication.LogNumber, dbo.ViewUserInfo.FirstName+' '+dbo.ViewUserInfo.LastName AS FullName 
							FROM         dbo.ViewPanelApplicationReviewerAssignment INNER JOIN
										  dbo.ClientAssignmentType ON dbo.ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = dbo.ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
										  dbo.AssignmentType ON dbo.ClientAssignmentType.AssignmentTypeId = dbo.AssignmentType.AssignmentTypeId INNER JOIN
										  dbo.PanelUserAssignment ON dbo.ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = dbo.PanelUserAssignment.PanelUserAssignmentId INNER JOIN
										  dbo.ViewPanelApplication ON dbo.ViewPanelApplicationReviewerAssignment.PanelApplicationId = dbo.ViewPanelApplication.PanelApplicationId INNER JOIN
										  dbo.ViewApplication ON dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId INNER JOIN
										  dbo.ViewUserInfo ON dbo.PanelUserAssignment.UserId = dbo.ViewUserInfo.UserID INNER JOIN
										  dbo.ClientExpertiseRating ON dbo.ClientAssignmentType.ClientId = dbo.ClientExpertiseRating.ClientId INNER JOIN
										  dbo.ViewSessionPanel ON dbo.PanelUserAssignment.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId AND 
										  dbo.ViewPanelApplication.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId INNER JOIN
										  dbo.ViewProgramMechanism ON dbo.ViewApplication.ProgramMechanismId = dbo.ViewProgramMechanism.ProgramMechanismId INNER JOIN
										  dbo.ViewClientAwardType ON dbo.ViewProgramMechanism.ClientAwardTypeId = dbo.ViewClientAwardType.ClientAwardTypeId
							WHERE     (dbo.ClientAssignmentType.AssignmentTypeId = 8) AND (dbo.ClientExpertiseRating.RatingAbbreviation = 'COI') --AND (dbo.PanelUserAssignment.SessionPanelId = @PanelList)
						 ) t1 group by t1.SessionPanelID, t1.ApplicationId, t1.LogNumber) 
			  COI_DS ON ViewPanelApplication.SessionPanelId = coi_ds.SessionPanelId AND ViewPanelApplication.ApplicationId = coi_ds.ApplicationId LEFT OUTER JOIN
			  /*******************************************************************************/
					(SELECT t1.SessionPanelID, t1.ApplicationId, t1.LogNumber,
					  STUFF((SELECT DISTINCT '' + t2.Assignment_Particip_Role + '!'
							 FROM (SELECT     PanelUserAssignment.SessionPanelId, ViewApplication.ApplicationId, ViewApplication.LogNumber, CONVERT(varchar, ViewPanelApplicationReviewerAssignment.SortOrder) 
										  + ' - ' + ViewUserInfo.FirstName + ' ' + ViewUserInfo.LastName + '(' + ClientAssignmentType.AssignmentAbbreviation + ') - ' + ClientParticipantType.ParticipantTypeAbbreviation +
										   '-' + ParticipationMethod.ParticipationMethodLabel + '-' + CASE WHEN dbo.PanelUserAssignment.RestrictedAssignedFlag = 0 THEN 'Full' ELSE 'Adhoc' END + CASE WHEN
										   ClientRole.RoleAbbreviation IS NULL THEN '' ELSE '-' + ClientRole.RoleAbbreviation END AS Assignment_Particip_Role
									FROM         ViewPanelApplicationReviewerAssignment INNER JOIN
														  ViewApplication INNER JOIN
														  ViewPanelApplication ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId ON 
														  ViewPanelApplicationReviewerAssignment.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
														  ClientParticipantType INNER JOIN
														  PanelUserAssignment ON ClientParticipantType.ClientParticipantTypeId = PanelUserAssignment.ClientParticipantTypeId ON 
														  ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
														  ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
														  ViewUserInfo ON PanelUserAssignment.UserId = ViewUserInfo.UserID INNER JOIN
														  ParticipationMethod ON PanelUserAssignment.ParticipationMethodId = ParticipationMethod.ParticipationMethodId LEFT OUTER JOIN
														  ClientRole ON PanelUserAssignment.ClientRoleId = ClientRole.ClientRoleId
									WHERE     (ClientAssignmentType.AssignmentAbbreviation <> 'COI') --AND (PanelUserAssignment.SessionPanelId = @PanelList)
								  ) t2
											 where t1.SessionPanelID = t2.SessionPanelID and t1.ApplicationId=t2.ApplicationId
												FOR XML PATH(''), TYPE
												).value('(./text())[1]', 'NVARCHAR(MAX)') 
											,1,0,'') assgn_prtcp_role
					FROM (SELECT     PanelUserAssignment.SessionPanelId, ViewApplication.ApplicationId, ViewApplication.LogNumber, CONVERT(varchar, ViewPanelApplicationReviewerAssignment.SortOrder) 
										  + ' - ' + ViewUserInfo.FirstName + ' ' + ViewUserInfo.LastName + '(' + ClientAssignmentType.AssignmentAbbreviation + ') - ' + ClientParticipantType.ParticipantTypeAbbreviation +
										   '-' + ParticipationMethod.ParticipationMethodLabel + '-' + CASE WHEN dbo.PanelUserAssignment.RestrictedAssignedFlag = 0 THEN 'Full' ELSE 'Adhoc' END + CASE WHEN
										   ClientRole.RoleAbbreviation IS NULL THEN '' ELSE '-' + ClientRole.RoleAbbreviation END AS Assignment_Particip_Role
							FROM         ViewPanelApplicationReviewerAssignment INNER JOIN
												  ViewApplication INNER JOIN
												  ViewPanelApplication ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId ON 
												  ViewPanelApplicationReviewerAssignment.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
												  ClientParticipantType INNER JOIN
												  PanelUserAssignment ON ClientParticipantType.ClientParticipantTypeId = PanelUserAssignment.ClientParticipantTypeId ON 
												  ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
												  ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
												  ViewUserInfo ON PanelUserAssignment.UserId = ViewUserInfo.UserID INNER JOIN
												  ParticipationMethod ON PanelUserAssignment.ParticipationMethodId = ParticipationMethod.ParticipationMethodId LEFT OUTER JOIN
												  ClientRole ON PanelUserAssignment.ClientRoleId = ClientRole.ClientRoleId
							WHERE     (ClientAssignmentType.AssignmentAbbreviation <> 'COI') --AND (PanelUserAssignment.SessionPanelId = @PanelList)
						  ) t1 group by t1.SessionPanelID, t1.ApplicationId, t1.LogNumber)
			  APTR_DS ON ViewPanelApplication.SessionPanelId = aptr_ds.SessionPanelId AND ViewPanelApplication.ApplicationId = aptr_ds.ApplicationId 
			  /*******************************************************************************/
			  INNER JOIN
			  ProgramParams ON dbo.ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
			  FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
			  PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId

	WHERE     (ViewApplicationPersonnel.PrimaryFlag = 1) --AND (ViewPanelApplication.SessionPanelId = @PanelList)
	ORDER BY 1

END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportOrderOfDiscussion] TO [NetSqlAzMan_Users]
    AS [dbo];



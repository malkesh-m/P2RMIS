CREATE PROCEDURE [dbo].[uspReportW9Address] 
-- Add the parameters for the stored procedure here
@ProgramList varchar(4000),
@FiscalYearList varchar(4000),
@PanelList varchar(4000)

AS

BEGIN
-- SET NOCOUNT ON added to prevent extra result sets fromcom.
-- interfering with SELECT statements.
SET NOCOUNT ON;

-- Insert statements for procedure here
WITH ProgramParams(ClientProgramId)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
FiscalYearParams(FY)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
PanelParams(PanelId)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))

SELECT DISTINCT 
					LASTNAME,
					FirstName,
					VendorName,
					VendorID,
					UserID,
					PartType,
					ParticipationMethodLabel,
					PartLevel,
					RoleName,
					Address1, 
					Address2, 
					Address3, 
					Address4, 
					City, 
					StateName, 					  
					CountryName, 
					Zip,
					PaymentCategory, value, datesigned,DateCompleted,SignedOfflineFlag,
					ConsultantFee,
					W9Status,
					ProgramAbbreviation, 
					PanelAbbreviation, year, ClientMeetingId, CountryAbbreviation, StateAbbreviation, ProgramDescription 
FROM (SELECT DISTINCT 
                         ProgramYear.ProgramYearId, ClientMeeting.MeetingTypeId, ClientProgram.ClientId, ClientProgram.ProgramDescription, ProgramYear.Year, SessionPanel.SessionPanelId, SessionPanel.PanelName, UserInfo.LastName, 
                         UserInfo.FirstName, ViewUserVendor.VendorName, ViewUserVendor.VendorId, UserInfo.UserID, ClientParticipantType.ClientParticipantTypeId, ClientParticipantType.ParticipantTypeName AS PartType, 
                         ParticipationMethod.ParticipationMethodId, ParticipationMethod.ParticipationMethodLabel, ClientRole.RoleName, UserAddress.AddressTypeId, UserAddress.Address1, UserAddress.Address2, UserAddress.Address3, 
                         UserAddress.Address4, UserAddress.City, State.StateAbbreviation, State.StateName, Country.CountryAbbreviation, Country.CountryName, UserAddress.Zip, Acknowledgement.PaymentCategory, 
                         REPLACE(CONVERT(VARCHAR(20), CAST(PanelUserRegistrationDocumentContract.FeeAmount AS MONEY), 1), '.00', '') AS ConsultantFee, ClientMeeting.ClientMeetingId, ClientParticipantType.ClientParticipantTypeId AS sssc, 
                         ParticipationMethod.ParticipationMethodId AS smt, PanelUserAssignment.RestrictedAssignedFlag AS PartLevel, Acknowledgement.DateSigned, Acknowledgement.DateCompleted, Acknowledgement.SignedOfflineFlag, 
                         Acknowledgement.Value, [User].W9Verified, CASE WHEN UserAddress.AddressID IS NULL THEN 'Missing' WHEN UserAddress.AddressID IS NOT NULL AND [User].W9Verified IS NULL 
                         THEN 'Uploaded' WHEN [User].W9Verified = 1 THEN 'Verified Accurate' WHEN [User].W9Verified = 0 THEN 'Verified Inaccurate' END AS W9Status, UserAddress.AddressID, ClientProgram.ProgramAbbreviation, 
                         SessionPanel.PanelAbbreviation		  

FROM            (SELECT        ViewPanelUserRegistrationDocument.DateCompleted, ViewPanelUserRegistrationDocument.DocumentFile, ViewPanelUserRegistrationDocument.SignedOfflineFlag, 
                                                    ViewPanelUserRegistrationDocument.PanelUserRegistrationId, ViewPanelUserRegistrationDocument.DateSigned, ViewPanelUserRegistrationDocumentItem.Value, 
                                                    CASE WHEN ViewPanelUserRegistrationDocumentItem.Value = 'Paid' THEN 'Paid' WHEN ViewPanelUserRegistrationDocumentItem.Value = 'Unpaid' THEN 'Unpaid' WHEN ViewPanelUserRegistrationDocumentItem.Value
                                                     = 'Unpaid w/t' THEN 'Unpaid w/ Travel' END AS PaymentCategory, PanelUserRegistration_1.RegistrationCompletedDate
                          FROM            ViewPanelUserRegistrationDocument INNER JOIN
                                                    ClientRegistrationDocument ON ViewPanelUserRegistrationDocument.ClientRegistrationDocumentId = ClientRegistrationDocument.ClientRegistrationDocumentId INNER JOIN
                                                    ViewPanelUserRegistrationDocumentItem ON ViewPanelUserRegistrationDocument.PanelUserRegistrationDocumentId = ViewPanelUserRegistrationDocumentItem.PanelUserRegistrationDocumentId INNER JOIN
                                                    PanelUserRegistration AS PanelUserRegistration_1 ON PanelUserRegistration_1.PanelUserRegistrationId = ViewPanelUserRegistrationDocument.PanelUserRegistrationId
                          WHERE        (ViewPanelUserRegistrationDocumentItem.RegistrationDocumentItemId = 8)) AS Acknowledgement RIGHT OUTER JOIN
                         ViewPanelUserRegistrationDocument AS ViewPanelUserRegistrationDocument_1 INNER JOIN
                         ViewPanelUserRegistration AS PanelUserRegistration ON ViewPanelUserRegistrationDocument_1.PanelUserRegistrationId = PanelUserRegistration.PanelUserRegistrationId INNER JOIN
                         PanelUserRegistrationDocumentContract ON ViewPanelUserRegistrationDocument_1.PanelUserRegistrationDocumentId = PanelUserRegistrationDocumentContract.PanelUserRegistrationDocumentId AND PanelUserRegistrationDocumentContract.DeletedFlag = 0 RIGHT OUTER JOIN
                         ClientProgram INNER JOIN
                         ViewProgramYear AS ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
                         ViewProgramPanel AS ProgramPanel ON ProgramYear.ProgramYearId = ProgramPanel.ProgramYearId INNER JOIN
                         ViewSessionPanel AS SessionPanel ON ProgramPanel.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
                         ViewPanelUserAssignment AS PanelUserAssignment ON SessionPanel.SessionPanelId = PanelUserAssignment.SessionPanelId INNER JOIN
                         ParticipationMethod ON ParticipationMethod.ParticipationMethodId = PanelUserAssignment.ParticipationMethodId INNER JOIN
                         ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
                         ClientMeeting ON ClientProgram.ClientId = ClientMeeting.ClientId INNER JOIN
                         ViewMeetingSession ON ClientMeeting.ClientMeetingId = ViewMeetingSession.ClientMeetingId AND SessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId LEFT OUTER JOIN
                         ViewUserAddress AS UserAddress LEFT OUTER JOIN
                         State ON UserAddress.StateId = State.StateId LEFT OUTER JOIN
                         Country ON UserAddress.CountryId = Country.CountryId RIGHT OUTER JOIN
                         ViewUserInfo AS UserInfo ON UserAddress.UserInfoID = UserInfo.UserInfoID AND UserAddress.AddressTypeId = 4 ON PanelUserAssignment.UserId = UserInfo.UserID LEFT OUTER JOIN
                         ViewUserVendor ON UserInfo.UserInfoID = ViewUserVendor.UserInfoId AND ViewUserVendor.ActiveFlag = 1 LEFT OUTER JOIN
                         ViewUser AS [User] ON PanelUserAssignment.UserId = [User].UserID LEFT OUTER JOIN
                         ClientRole ON PanelUserAssignment.ClientRoleId = ClientRole.ClientRoleId ON PanelUserRegistration.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId ON 
                         Acknowledgement.PanelUserRegistrationId = PanelUserRegistration.PanelUserRegistrationId LEFT OUTER JOIN
                         ViewUserResume AS UserResume ON UserInfo.UserInfoID = UserResume.UserInfoId
--WHERE      ClientParticipantType.ReviewerFlag = 1
--and  Acknowledgement.DateSigned is not null
	--	and ClientProgram.ClientProgramId = 55
      --  and ProgramYear.Year = 2018
		--	and SessionPanel.SessionPanelId = 2803

				  INNER JOIN
	
                  ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
                  FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN
				  PanelParams ON PanelParams.PanelId = 0 OR SessionPanel.SessionPanelId = PanelParams.PanelId

WHERE      ClientParticipantType.ReviewerFlag =1

) A

END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportW9Address] TO [NetSqlAzMan_Users]
    AS [dbo];
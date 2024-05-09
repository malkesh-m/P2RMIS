-- =============================================
-- Author:		Alberto Catuche
-- Create date: 2/5/2016
-- Description:	Used as source for report Administrative Notes Report
-- =============================================

CREATE PROCEDURE [dbo].[uspReportRoster]
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@PanelList varchar(4000),
	@NumberOfYears int
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
WITH 
	ProgramParams(ClientProgramID) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	MeetingParams(MeetingId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(NULL)), 
	SessionParams(SessionId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(NULL)), 
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))

select UserID,
	   UserInfoID, 
	   MilitaryRank, 
	   Prefix, 
	   Suffix, 
	   BadgeName,
	   FirstName, 
	   LastName,
	   Email, 
	   Institution, 
	   Department, 
	   Title, 
	   ProgramDesc, 
	   MeetingSessionId, 
	   SessionID, 
	   ClientProgramId, 
	   ProgramAbrv, 
	   ProgramYearId, 
	   Year, 
	   ClientAbrv, 
	   ClientID, 
 	   ClientParticipantTypeId, 
	   PartType,  
	   PartTypeName, 
	   case when PartType in ('RTA','SRO') THEN '' ELSE ParticipationMethodLabel END AS  ParticipationMethodLabel,
	   PanelID, 
	   PanelAbrv, 
	   PanelName,
	   MeetingTypeName,
	   SessionDesc, 
	   StartDate,
	   EndDate, 
	   Expertise, 
	   RestrictedAssignedFlag,
	   case when PartType in ('RTA','SRO') THEN '' ELSE PartLevel END AS   PartLevel,
	   ReviewerFlag,
	   SpecialistFlag,
	   ConsumerFlag,
	   RosterOrder,
	   AltContact, 
	   AltPhone, 
	   AltEmail, 
	   PhoneNumber, 
	   PersonPreferredPhone,  
	   Photo, 
	   Role, 
	   ProgramParticipantDescription, 
	   Address ,
	   City, 
	   StateAbrv,
	   ZipCode, 
	   CountryAbrv,
	   VendorID, 
	   VendorName, 
	   VendorAddress,
	   VendorCity, 
       VendorStateAbrv,
	   VendorZipCode, 
	   VendorCountryAbrv,
	   case when PartType in ('RTA','SRO') THEN '' ELSE ParticipationHistory END AS ParticipationHistory
	   

from (

SELECT dbo.ViewUserInfo.UserID,
	dbo.ViewUserInfo.UserInfoID, dbo.MilitaryRank.MilitaryRankAbbreviation AS MilitaryRank, dbo.Prefix.PrefixName AS Prefix, dbo.ViewUserInfo.SuffixText AS Suffix, dbo.ViewUserInfo.BadgeName,
	dbo.ViewUserInfo.FirstName, dbo.ViewUserInfo.LastName, dbo.ViewUserEmail.Email, dbo.ViewUserInfo.Institution, dbo.ViewUserInfo.Department, 
	dbo.ViewUserInfo.Position AS Title, dbo.ClientProgram.ProgramDescription AS ProgramDesc, dbo.ViewMeetingSession.MeetingSessionId, dbo.ViewMeetingSession.SessionAbbreviation AS SessionID, 
	dbo.ViewProgramYear.ClientProgramId, dbo.ClientProgram.ProgramAbbreviation AS ProgramAbrv, dbo.ViewProgramPanel.ProgramYearId, dbo.ViewProgramYear.Year, dbo.Client.ClientAbrv, dbo.Client.ClientID, 
	dbo.ClientParticipantType.ClientParticipantTypeId, dbo.ClientParticipantType.ParticipantTypeAbbreviation AS PartType,  dbo.ClientParticipantType.ParticipantTypeName AS PartTypeName, dbo.ParticipationMethod.ParticipationMethodLabel,ViewProgramPanel.ProgramPanelId AS PanelID, dbo.ViewSessionPanel.PanelAbbreviation AS PanelAbrv, 
	dbo.ViewSessionPanel.PanelName, dbo.MeetingType.MeetingTypeName,dbo.ViewMeetingSession.SessionDescription AS SessionDesc, dbo.ViewMeetingSession.StartDate, dbo.ViewMeetingSession.EndDate, dbo.UserInfo.Expertise, 
	dbo.PanelUserAssignment.RestrictedAssignedFlag,
	case when PanelUserAssignment.RestrictedAssignedFlag = 1 then 'Partial (Ad Hoc)'
	else 'Full'
	end as PartLevel,
	dbo.ClientParticipantType.ReviewerFlag,dbo.ClientRole.SpecialistFlag,dbo.ClientParticipantType.ConsumerFlag,Case when dbo.ClientParticipantType.LegacyPartTypeId='SRA' then 1 
	when (dbo.ClientParticipantType.ChairpersonFlag=1 or dbo.ClientParticipantType.ElevatedChairpersonFlag=1) then 2 
	when dbo.ClientParticipantType.ReviewerFlag=1 and dbo.ClientParticipantType.ConsumerFlag=0 and dbo.PanelUserAssignment.RestrictedAssignedFlag=0 and dbo.ClientRole.SpecialistFlag IS Null or dbo.ClientRole.SpecialistFlag=0  then 3
	when dbo.ClientParticipantType.ReviewerFlag=1 and dbo.ClientParticipantType.ConsumerFlag=0 and dbo.PanelUserAssignment.RestrictedAssignedFlag=0 and dbo.ClientRole.SpecialistFlag=1 then 4
	when dbo.ClientParticipantType.ReviewerFlag=1 and dbo.ClientParticipantType.ConsumerFlag=0 and dbo.PanelUserAssignment.RestrictedAssignedFlag=1 then 5
	when dbo.ClientParticipantType.ReviewerFlag=1 and dbo.ClientParticipantType.ConsumerFlag=1 then 6
	when dbo.ClientParticipantType.LegacyPartTypeId='RTA' then 7
	
	 else 0 End AS RosterOrder, 
	AlternateContact.FirstName + ' ' + AlternateContact.LastName AS AltContact, AlternateContactPhone.PhoneNumber AS AltPhone, 
	AlternateContact.EmailAddress AS AltEmail,  AlternateContactPhone.PhoneNumber, 
	PrimaryPhone.PhoneNumber AS PersonPreferredPhone,  
	PersonPhoto.Photo, dbo.ClientRole.RoleAbbreviation AS Role, dbo.ClientParticipantType.ParticipantTypeName AS ProgramParticipantDescription, PersonPrefAddress.Address , PersonPrefAddress.City, PersonPrefAddress.StateAbrv, PersonPrefAddress.ZipCode, PersonPrefAddress.CountryAbrv,
	ViewUserVendor.VendorID, ViewUserVendor.VendorName, VendorAddress.VendorAddress, VendorAddress.City AS VendorCity, 
                      VendorAddress.StateAbrv AS VendorStateAbrv, VendorAddress.ZipCode AS VendorZipCode, VendorAddress.CountryAbrv AS VendorCountryAbrv,

		stuff( (SELECT '!'+CAST(dbo.ClientProgram.ProgramAbbreviation AS CHAR(12)) 
			+ CAST(dbo.ViewProgramYear.Year AS CHAR(7))
			+CAST(dbo.ClientParticipantType.ParticipantTypeAbbreviation AS CHAR(9))
			+dbo.ViewSessionPanel.PanelAbbreviation
			FROM         dbo.ViewPanelUserAssignment INNER JOIN
						  dbo.ClientParticipantType ON dbo.ViewPanelUserAssignment.ClientParticipantTypeId = dbo.ClientParticipantType.ClientParticipantTypeId INNER JOIN
						  dbo.ViewSessionPanel INNER JOIN
						  dbo.ClientProgram INNER JOIN
						  dbo.ViewProgramYear ON dbo.ClientProgram.ClientProgramId = dbo.ViewProgramYear.ClientProgramId 
					--LEFT join clients on programs selected to get all client participation for selected programs
				 JOIN (SELECT ClientProgramID from
							 clientprogram where clientid in (select ClientId from ClientProgram where ClientProgram.ClientProgramId IN (SELECT ClientProgramID FROM ProgramParams))) A
							on A.ClientProgramID = ViewProgramYear.ClientProgramId
						 INNER JOIN dbo.ViewProgramPanel ON dbo.ViewProgramYear.ProgramYearId = dbo.ViewProgramPanel.ProgramYearId ON 
						  dbo.ViewSessionPanel.SessionPanelId = dbo.ViewProgramPanel.SessionPanelId ON 
						  dbo.ViewPanelUserAssignment.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId

		   WHERE ViewPanelUserAssignment.UserId = ViewUserInfo.UserID AND 
		   ViewProgramYear.Year <= (Select Max (FiscalYearParams.FY) from FiscalYearParams)and ViewProgramYear.Year >=  ((Select Max  (FiscalYearParams.FY) from FiscalYearParams)-@NumberOfYears) AND ViewProgramPanel.ProgramPanelId NOT IN (Select PanelId From PanelParams)
	--	   and ClientProgram.ClientProgramId = @ProgramList
		   ORDER BY ViewProgramYear.Year DESC, ClientProgram.ProgramAbbreviation, ClientParticipantType.ParticipantTypeAbbreviation
		   FOR XML PATH(''), TYPE).value('.', 'varchar(max)')
		,1,1,'')
	AS ParticipationHistory
FROM 
	dbo.Client INNER JOIN
	dbo.PanelUserAssignment INNER JOIN
	dbo.ClientParticipantType ON dbo.PanelUserAssignment.ClientParticipantTypeId = dbo.ClientParticipantType.ClientParticipantTypeId INNER JOIN
	dbo.ViewSessionPanel ON dbo.PanelUserAssignment.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId INNER JOIN
	dbo.ViewProgramYear INNER JOIN
	dbo.ClientProgram ON dbo.ViewProgramYear.ClientProgramId = dbo.ClientProgram.ClientProgramId INNER JOIN
	dbo.ViewProgramPanel ON dbo.ViewProgramYear.ProgramYearId = dbo.ViewProgramPanel.ProgramYearId ON 
	dbo.ViewSessionPanel.SessionPanelId = dbo.ViewProgramPanel.SessionPanelId ON dbo.Client.ClientID = dbo.ClientParticipantType.ClientId AND 
	dbo.Client.ClientID = dbo.ClientProgram.ClientId INNER JOIN
	dbo.ViewClientMeeting ON dbo.Client.ClientID = dbo.ViewClientMeeting.ClientId INNER JOIN
	dbo.MeetingType ON dbo.ViewClientMeeting.MeetingTypeId=dbo.MeetingType.MeetingTypeID INNER JOIN
	dbo.ViewMeetingSession ON dbo.ViewClientMeeting.ClientMeetingId = dbo.ViewMeetingSession.ClientMeetingId AND 
	dbo.ViewSessionPanel.MeetingSessionId = dbo.ViewMeetingSession.MeetingSessionId LEFT OUTER JOIN
	dbo.ClientRole ON dbo.Client.ClientID = dbo.ClientRole.ClientId AND dbo.PanelUserAssignment.ClientRoleId = dbo.ClientRole.ClientRoleId LEFT OUTER JOIN
    dbo.UserInfo INNER JOIN
    dbo.ViewUserInfo ON dbo.UserInfo.UserInfoID = dbo.ViewUserInfo.UserInfoID LEFT OUTER JOIN
					  ViewUserVendor ON ViewUserInfo.UserInfoId = ViewUserVendor.UserInfoId AND ViewUserVendor.ActiveFlag = 1 LEFT OUTER JOIN
    dbo.Prefix ON dbo.ViewUserInfo.PrefixId = dbo.Prefix.PrefixId LEFT OUTER JOIN
   dbo.ViewUserEmail ON dbo.ViewUserInfo.UserInfoID = dbo.ViewUserEmail.UserInfoID LEFT OUTER JOIN
    dbo.MilitaryRank ON dbo.ViewUserInfo.MilitaryRankId = dbo.MilitaryRank.MilitaryRankId ON 
     dbo.PanelUserAssignment.UserId = dbo.ViewUserInfo.UserID LEFT OUTER JOIN
	dbo.ParticipationMethod ON dbo.PanelUserAssignment.ParticipationMethodId = dbo.ParticipationMethod.ParticipationMethodId LEFT OUTER JOIN
	(SELECT DISTINCT ViewUserAlternateContact_1.UserAlternateContactId, ViewUserAlternateContact_1.UserInfoId, 
	ViewUserAlternateContact_1.FirstName, ViewUserAlternateContact_1.LastName, ViewUserAlternateContact_1.EmailAddress
	FROM dbo.ViewUserAlternateContact AS ViewUserAlternateContact_1 INNER JOIN dbo.ViewUserInfo ON 
	ViewUserAlternateContact_1.UserInfoId = dbo.ViewUserInfo.UserInfoId AND ViewUserAlternateContact_1.PrimaryFlag = 1) AS AlternateContact
	ON dbo.ViewUserInfo.UserInfoID = AlternateContact.UserInfoId LEFT OUTER JOIN
	dbo.[User] ON dbo.ViewUserInfo.UserID = dbo.[User].UserID LEFT OUTER JOIN
	dbo.PersonPhoto ON dbo.[User].PersonID = PersonPhoto.PersonId INNER JOIN

	ProgramParams ON ProgramParams.ClientProgramID = 0 OR ClientProgram.ClientProgramID = ProgramParams.ClientProgramID INNER JOIN
	FiscalYearParams ON FiscalYearParams.FY = 0 OR ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
	MeetingParams ON MeetingParams.MeetingId = '' OR ViewMeetingSession.MeetingSessionId = MeetingParams.MeetingId INNER JOIN
	SessionParams ON SessionParams.SessionId = '' OR ViewSessionPanel.SessionPanelId = SessionParams.SessionId INNER JOIN
	PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId LEFT OUTER JOIN

	(SELECT DISTINCT ViewUserAlternateContactPhone_1.UserAlternateContactId, ViewUserAlternateContactPhone_1.PhoneNumber
FROM         dbo.ViewUserAlternateContactPhone AS ViewUserAlternateContactPhone_1 INNER JOIN
                      dbo.ViewUserAlternateContact ON ViewUserAlternateContactPhone_1.UserAlternateContactId = dbo.ViewUserAlternateContact.UserAlternateContactId AND 
                      ViewUserAlternateContactPhone_1.PrimaryFlag = dbo.ViewUserAlternateContact.PrimaryFlag
WHERE     (ViewUserAlternateContactPhone_1.PrimaryFlag = 1)) AS AlternateContactPhone ON AlternateContact.UserAlternateContactId = AlternateContactPhone.UserAlternateContactId LEFT OUTER JOIN
	(SELECT  Stuff( Coalesce(' ' + NULLIF(Address1, ''), '') 
	+ Coalesce(', ' + NULLIF(Address2, ''), '') 
	+ Coalesce(', ' + NULLIF(Address3, ''), '') 
	+ Coalesce(', ' + NULLIF(Address4, ''), '') 
	 
	 , 1, 1, '') AS [Address],
	
	 City, StateAbbreviation AS StateAbrv, Zip AS ZipCode, CountryAbbreviation AS CountryAbrv, UserInfoID
	  FROM          dbo.ViewUserAddress INNER JOIN 
	                dbo.State ON dbo.ViewUserAddress.StateId = dbo.State.StateId INNER JOIN 
					dbo.Country ON dbo.ViewUserAddress.CountryId = dbo.Country.CountryId
	  WHERE      (PrimaryFlag = 1)) AS PersonPrefAddress ON ViewUserInfo.UserInfoId = PersonPrefAddress.UserInfoID LEFT OUTER JOIN

	  (SELECT     Stuff( Coalesce(' ' + NULLIF(Address1, ''), '') 
	+ Coalesce(', ' + NULLIF(Address2, ''), '') 
	+ Coalesce(', ' + NULLIF(Address3, ''), '') 
	+ Coalesce(', ' + NULLIF(Address4, ''), '') 
	 
	 , 1, 1, '') AS [VendorAddress] ,
                                                   dbo.ViewUserAddress.City, dbo.State.StateAbbreviation AS StateAbrv, dbo.ViewUserAddress.Zip AS ZipCode, 
                                                   dbo.Country.CountryAbbreviation AS CountryAbrv, dbo.ViewUserAddress.UserInfoID
                            FROM          dbo.ViewUserAddress INNER JOIN
                                                   dbo.State ON dbo.ViewUserAddress.StateId = dbo.State.StateId INNER JOIN
                                                   dbo.Country ON dbo.ViewUserAddress.CountryId = dbo.Country.CountryId INNER JOIN
                                                   dbo.ViewUserInfo AS ViewUserInfo ON dbo.ViewUserAddress.UserInfoID = ViewUserInfo.UserInfoID
                            WHERE      (dbo.ViewUserAddress.AddressTypeId = 4)) AS VendorAddress ON ViewUserInfo.UserInfoId = VendorAddress.UserInfoID LEFT OUTER JOIN


	(SELECT     ViewUserPhone.UserInfoID, ViewUserPhone.phone AS PhoneNumber, dbo.PhoneType.PhoneType, dbo.ViewUserPhone.PrimaryFlag
FROM         dbo.ViewUserPhone LEFT OUTER JOIN
                      dbo.PhoneType ON ViewUserPhone.PhoneTypeId = dbo.PhoneType.PhoneTypeId
	  WHERE       (dbo.ViewUserPhone.PrimaryFlag = 1)) AS PrimaryPhone ON ViewUserInfo.UserInfoId = PrimaryPhone.UserInfoID 
	
	

WHERE 
	(dbo.ViewUserEmail.PrimaryFlag = 1) AND
	(dbo.PanelUserAssignment.DeletedFlag = 0) AND
	(dbo.[User].DeletedFlag = 0)

) A

ORDER BY  ProgramAbrv, Year, PanelAbrv,RosterOrder, LastName

END



GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportRoster] TO [NetSqlAzMan_Users]
    AS [dbo];
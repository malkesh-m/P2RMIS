-- =============================================
-- Author: Joe Gao
-- Created date: 4/9/2018
-- Description: Storeprocedure to Create Master Travel Report
-- Modified by: Ngan on 1/15/2019 parameters changes

-- exec [uspReportTravel] '2013', '1', '351', null, null
-- exec [uspReportTravel] '2013', '1', '359', null, null
-- exec [uspReportTravel] '2013', '1', '359', '827', null
-- ===========================================
CREATE PROCEDURE [dbo].[uspReportTravel] 
-- Add the parameters for the stored procedure here
@FiscalYearList varchar(4000),
@MeetingTypeList varchar(4000),
@MeetingList varchar(4000),
@Session varchar(4000) = NULL,
@ProgramList varchar(4000) = NULL

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

	WITH 
	
		FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
		MeetingTypeParams(MeetingTypeId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@MeetingTypeList)),
		MeetingParams(ClientMeetingId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@MeetingList)),
		SessionParam(MeetingSessionId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@Session)),
		ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList))


	SELECT distinct 
		    ViewMeetingSession.ClientMeetingId, 
			ViewMeetingSession.MeetingSessionId, 
			ViewMeetingSession.SessionAbbreviation, 
			ViewMeetingSession.SessionDescription,
			ViewClientMeeting.MeetingAbbreviation, 
			ViewClientMeeting.MeetingDescription, 
			Convert(varchar(10),ViewClientMeeting.StartDate, 101) AS [Meeting Start],
			Convert(varchar(10),ViewClientMeeting.EndDate, 101) AS [Meeting End],
			ViewPanelUserAssignment.PanelUserAssignmentId As [PartID],
			ViewPanelUserAssignment.RestrictedAssignedFlag As [PartLevel],
			ViewPanelUserAssignment.ParticipationMethodId As [PartMethod],
			ViewUserInfo.LastName AS [Last Name], 
			ViewUserInfo.FirstName AS [First Name], 
			ClientParticipantType.ParticipantTypeAbbreviation AS [Part Type],
			ViewUserEmail.Email AS Email,
			PanelRegistrationItem.Value AS [SRA Paid],
			ClientProgram.ProgramAbbreviation AS Program,
			ViewProgramYear.[Year] AS FY,
			ViewSessionPanel.PanelAbbreviation AS Panel,
			ViewMeetingSession.SessionDescription AS [Session],
			Convert(varchar(10), ViewMeetingSession.StartDate, 101) AS [Session Start],
			Convert(varchar(10), ViewMeetingSession.EndDate, 101) AS [Session End],
			TravelMode.TravelModeAbbreviation AS Mode,
			ViewMeetingRegistrationTravel.ReservationCode AS Reservation,
			convert(varchar(10), ViewClientMeeting.StartDate, 101) AS [Meeting Attendance Start],
			convert(varchar(10), ViewClientMeeting.EndDate, 101) AS [Meeting Attendance End],
			Convert(varchar(10), ViewMeetingRegistrationHotel.HotelCheckInDate, 101) AS [Check In],
			Convert(varchar(10), ViewMeetingRegistrationHotel.HotelCheckOutDate, 101) AS [Check Out],
			datediff(day, convert(varchar(10), ViewMeetingRegistrationHotel.HotelCheckInDate, 101), 
			convert(varchar(10), ViewMeetingRegistrationHotel.HotelCheckOutDate, 101)) As [# of nights],	
			IIF(ViewMeetingRegistrationHotel.HotelNotRequiredFlag = 1, 'Yes', 'No') AS [Hotel not Required],
			IIF(ViewMeetingRegistrationHotel.HotelDoubleOccupancy = 1, 'Yes', 'No') AS [Double Occupancy],
			ViewMeetingRegistrationHotel.HotelAndFoodRequestComments AS [Special Accomodation Request],
			Hotel.HotelName AS Hotel,			
			ViewMeetingRegistrationTravel.Fare,
			ViewMeetingRegistrationTravel.AgentFee AS [Agent Fee #1],
			ViewMeetingRegistrationTravel.AgentFee2 AS [Agent Fee #2],
			ViewMeetingRegistrationTravel.ChangeFee AS [Change Fee],
			IIF(ViewMeetingRegistrationTravel.Ground = 1, 'Yes', 'No') AS Ground,
			ViewMeetingRegistrationTravel.TravelRequestComments AS [Special Travel Request],
			ViewMeetingRegistrationComment.InternalComments AS [Internal Comments],
			ViewMeetingRegistrationTravel.NteAmount AS [NTE Amount],
			ViewMeetingRegistrationTravel.GsaRate AS [GSA rate],	
			IIF(ViewMeetingRegistrationTravel.NoGsa = 1, 'Yes', 'No') AS [No GSA],	
			ViewMeetingRegistrationTravel.ClientApprovedAmount AS [Client Approved Amount],
			Convert(varchar(10), ViewMeetingRegistrationTravel.CancellationDate, 101) AS [Canceled Date],
			ViewMeetingRegistrationTravelFlight.DepartureCity AS [Dept Aircode],		
			Convert(varchar(10), Cast(ViewMeetingRegistrationTravelFlight.DepartureDate as date), 101) AS [Dep Date],	
			Convert(varchar(5), Cast(ViewMeetingRegistrationTravelFlight.DepartureDate as time), 108) AS [Dep Time],
			ViewMeetingRegistrationTravelFlight.ArrivalCity AS [Arr City],
			Convert(varchar(10), Cast(ViewMeetingRegistrationTravelFlight.ArrivalDate as date), 101) AS [Arr Date],	
			Convert(varchar(5), Cast(ViewMeetingRegistrationTravelFlight.ArrivalDate as time), 108) as [Arr Time],
			ViewMeetingRegistrationTravelFlight.CarrierName AS [Carrier],
			ViewMeetingRegistrationTravelFlight.FlightNumber AS [Flight],
			ltrim(rtrim(substring(Airport.Description, 0, CHARINDEX(',',Airport.Description))))  as [Dep City],
			ltrim(rtrim(substring(Airport.Description, CHARINDEX(',',Airport.Description) + 1, len(Airport.Description)))) as [Dep State],
			IIF(PanelRegistrationItem.Value='Paid', 'Yes','No') as [On Master]

	
	FROM	ViewPanelUserAssignment 
		INNER JOIN ClientParticipantType			  ON ClientParticipantType.ClientParticipantTypeId = ViewPanelUserAssignment.ClientParticipantTypeId
		INNER JOIN ViewSessionPanel           		  ON ViewSessionPanel.SessionPanelId = ViewPanelUserAssignment.SessionPanelId
		INNER JOIN ViewMeetingSession          		  ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId
		INNER JOIN ViewClientMeeting          		  ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId
		INNER JOIN MeetingType						  ON MeetingType.MeetingTypeId = ViewClientMeeting.MeetingTypeId
		INNER JOIN viewProgramPanel					  ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId
		INNER JOIN viewProgramYear					  ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId
		INNER JOIN ClientProgram					  ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId
		INNER JOIN FiscalYearParams					  ON ViewProgramYear.[Year] = FiscalYearParams.FY
		INNER JOIN MeetingTypeParams				  ON MeetingType.MeetingTypeId = MeetingTypeParams.MeetingTypeId
		INNER JOIN MeetingParams					  ON ViewMeetingSession.ClientMeetingId = MeetingParams.ClientMeetingId 
		INNER JOIN SessionParam						  ON SessionParam.MeetingSessionId = 0 OR ViewMeetingSession.MeetingSessionId = SessionParam.MeetingSessionId
		INNER JOIN ProgramParams					  ON ProgramParams.ClientProgramId = 0 OR ViewProgramYear.ClientProgramId = ProgramParams.ClientProgramId
		INNER JOIN ViewUserInfo						  ON ViewUserInfo.UserID = viewPanelUserAssignment.UserId
		LEFT JOIN ViewUserEmail					      ON ViewUserEmail.UserInfoID = ViewUserInfo.UserInfoID 
														AND ViewUserEmail.PrimaryFlag = 1
		LEFT JOIN ViewUserAddress					  ON ViewUserAddress.UserInfoID = ViewUserInfo.UserInfoID
														AND ViewUserAddress.PrimaryFlag = 1
		LEFT JOIN
			(SELECT ViewPanelUserRegistration.PanelUserAssignmentId, ViewPanelUserRegistrationDocumentItem.[Value]
			FROM ViewPanelUserRegistration   		  
			INNER JOIN ViewPanelUserRegistrationDocument	  ON ViewPanelUserRegistration.PanelUserRegistrationId = ViewPanelUserRegistrationDocument.PanelUserRegistrationId
			INNER JOIN ViewPanelUserRegistrationDocumentItem ON ViewPanelUserRegistrationDocument.PanelUserRegistrationDocumentId = ViewPanelUserRegistrationDocumentItem.PanelUserRegistrationDocumentId
			WHERE ViewPanelUserRegistrationDocumentItem.RegistrationDocumentItemId = 8) 
				PanelRegistrationItem				  ON ViewPanelUserAssignment.PanelUserAssignmentId = PanelRegistrationItem.PanelUserAssignmentId
		LEFT JOIN ViewMeetingRegistration 			  ON ViewPanelUserAssignment.PanelUserAssignmentId = ViewMeetingRegistration.PanelUserAssignmentId
		LEFT JOIN ViewMeetingRegistrationHotel		  ON ViewMeetingRegistration.MeetingRegistrationId = ViewMeetingRegistrationHotel.MeetingRegistrationId
		and ViewMeetingRegistrationHotel.CancellationFlag = 0
		LEFT JOIN Hotel								  ON ViewMeetingRegistrationHotel.HotelId = Hotel.HotelId
		LEFT JOIN ViewMeetingRegistrationAttendance   ON ViewMeetingRegistration.MeetingRegistrationId = ViewMeetingRegistrationAttendance.MeetingRegistrationId
		LEFT JOIN ViewMeetingRegistrationTravel 	  ON ViewMeetingRegistration.MeetingRegistrationId = ViewMeetingRegistrationTravel.MeetingRegistrationId
		LEFT JOIN ViewMeetingRegistrationComment 	  ON ViewMeetingRegistration.MeetingRegistrationId = ViewMeetingRegistrationComment.MeetingRegistrationId
		LEFT JOIN TravelMode						  ON ViewMeetingRegistrationTravel.TravelModeId = TravelMode.TravelModeId
		LEFT JOIN ViewMeetingRegistrationTravelFlight ON ViewMeetingRegistrationTravel.MeetingRegistrationTravelId = ViewMeetingRegistrationTravelFlight.MeetingRegistrationTravelId
		left join Airport on ViewMeetingRegistrationTravelFlight.DepartureCity = Airport.Code
		Where ViewPanelUserAssignment.ParticipationMethodId<>2
	UNION ALL
	SELECT DISTINCT	ViewMeetingSession.ClientMeetingId, 
			ViewMeetingSession.MeetingSessionId, 
			ViewMeetingSession.SessionAbbreviation, 
			ViewMeetingSession.SessionDescription,
			ViewClientMeeting.MeetingAbbreviation, 
			ViewClientMeeting.MeetingDescription, 
			Convert(varchar(10),ViewClientMeeting.StartDate, 101) AS [Meeting Start],
			Convert(varchar(10),ViewClientMeeting.EndDate, 101) AS [Meeting End],
			NULL AS [PartID],
			NULL AS [PartLevel],
			NULL AS [PartMethod],
			ViewUserInfo.LastName AS [Last Name], 
			ViewUserInfo.FirstName AS [First Name], 
			NULL AS [Part Type],
			ViewUserEmail.Email AS Email,
			NULL AS [SRA Paid],
			ClientProgram.ProgramAbbreviation AS Program,
			ViewProgramYear.[Year] AS FY,
			NULL AS Panel,
			ViewMeetingSession.SessionDescription AS [Session],
			Convert(varchar(10), ViewMeetingSession.StartDate, 101) AS [Session Start],
			Convert(varchar(10), ViewMeetingSession.EndDate, 101) AS [Session End],
			TravelMode.TravelModeAbbreviation AS Mode,
			ViewMeetingRegistrationTravel.ReservationCode AS Reservation,
			convert(varchar(10), ViewClientMeeting.StartDate, 101) AS [Meeting Attendance Start],
			convert(varchar(10), ViewClientMeeting.EndDate, 101) AS [Meeting Attendance End],
			Convert(varchar(10), ViewMeetingRegistrationHotel.HotelCheckInDate, 101) AS [Check In],
			Convert(varchar(10), ViewMeetingRegistrationHotel.HotelCheckOutDate, 101) AS [Check Out],	
			datediff(day, convert(varchar(10), ViewMeetingRegistrationHotel.HotelCheckInDate, 101), 
			convert(varchar(10), ViewMeetingRegistrationHotel.HotelCheckOutDate, 101)) As [# of nights],	
			IIF(ViewMeetingRegistrationHotel.HotelNotRequiredFlag = 1, 'Yes', 'No') AS [Hotel not Required],
			IIF(ViewMeetingRegistrationHotel.HotelDoubleOccupancy = 1, 'Yes', 'No') AS [Double Occupancy],
			ViewMeetingRegistrationHotel.HotelAndFoodRequestComments AS [Special Accomodation Request],
			Hotel.HotelName AS Hotel,	
			ViewMeetingRegistrationTravel.Fare,
			ViewMeetingRegistrationTravel.AgentFee AS [Agent Fee #1],
			ViewMeetingRegistrationTravel.AgentFee2 AS [Agent Fee #2],
			ViewMeetingRegistrationTravel.ChangeFee AS [Change Fee],
			IIF(ViewMeetingRegistrationTravel.Ground = 1, 'Yes', 'No') AS Ground,
			ViewMeetingRegistrationTravel.TravelRequestComments AS [Special Travel Request],
			ViewMeetingRegistrationComment.InternalComments AS [Internal Comments],
			ViewMeetingRegistrationTravel.NteAmount AS [NTE Amount],
			ViewMeetingRegistrationTravel.GsaRate AS [GSA rate],	
			IIF(ViewMeetingRegistrationTravel.NoGsa = 1, 'Yes', 'No') AS [No GSA],	
			ViewMeetingRegistrationTravel.ClientApprovedAmount AS [Client Approved Amount],
			Convert(varchar(10), ViewMeetingRegistrationTravel.CancellationDate, 101) AS [Canceled Date],
			ViewMeetingRegistrationTravelFlight.DepartureCity AS [Dept Aircode],		
			Convert(varchar(10), Cast(ViewMeetingRegistrationTravelFlight.DepartureDate as date), 101) AS [Dep Date],	
			Convert(varchar(5), Cast(ViewMeetingRegistrationTravelFlight.DepartureDate as time), 108) AS [Dep Time],
			ViewMeetingRegistrationTravelFlight.ArrivalCity AS [Arr City],
			Convert(varchar(10), Cast(ViewMeetingRegistrationTravelFlight.ArrivalDate as date), 101) AS [Arr Date],	
			Convert(varchar(5), Cast(ViewMeetingRegistrationTravelFlight.ArrivalDate as time), 108) as [Arr Time],
			ViewMeetingRegistrationTravelFlight.CarrierName AS [Carrier],
			ViewMeetingRegistrationTravelFlight.FlightNumber AS [Flight],
			ltrim(rtrim(substring(Airport.Description, 0, CHARINDEX(',',Airport.Description))))  as [Dep City],
			ltrim(rtrim(substring(Airport.Description, CHARINDEX(',',Airport.Description) + 1, len(Airport.Description)))) as [Dep State],
			NULL as [On Master]
			

	FROM	ViewSessionUserAssignment 
		INNER JOIN ViewSessionPanel           		  ON ViewSessionPanel.MeetingSessionId = ViewSessionUserAssignment.MeetingSessionId
		INNER JOIN ViewMeetingSession          		  ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId
		INNER JOIN ViewClientMeeting          		  ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId
		INNER JOIN MeetingType						  ON MeetingType.MeetingTypeId = ViewClientMeeting.MeetingTypeId
		INNER JOIN viewProgramPanel					  ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId
		INNER JOIN viewProgramYear					  ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId
		INNER JOIN ClientProgram					  ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId
		INNER JOIN FiscalYearParams					  ON ViewProgramYear.[Year] = FiscalYearParams.FY
		INNER JOIN MeetingTypeParams				  ON MeetingType.MeetingTypeId = MeetingTypeParams.MeetingTypeId
		INNER JOIN MeetingParams					  ON ViewMeetingSession.ClientMeetingId = MeetingParams.ClientMeetingId 
		INNER JOIN SessionParam						  ON SessionParam.MeetingSessionId = 0 OR ViewMeetingSession.MeetingSessionId = SessionParam.MeetingSessionId
		INNER JOIN ProgramParams					  ON ProgramParams.ClientProgramId = 0 OR ViewProgramYear.ClientProgramId = ProgramParams.ClientProgramId
		INNER JOIN ViewUserInfo						  ON ViewUserInfo.UserID = viewSessionUserAssignment.UserId
		LEFT JOIN ViewUserEmail					      ON ViewUserEmail.UserInfoID = ViewUserInfo.UserInfoID 
														AND ViewUserEmail.PrimaryFlag = 1
		LEFT JOIN ViewUserAddress					  ON ViewUserAddress.UserInfoID = ViewUserInfo.UserInfoID
														AND ViewUserAddress.PrimaryFlag = 1
		LEFT JOIN ViewMeetingRegistration 			  ON ViewSessionUserAssignment.SessionUserAssignmentId = ViewMeetingRegistration.SessionUserAssignmentId
		LEFT JOIN ViewMeetingRegistrationHotel		  ON ViewMeetingRegistration.MeetingRegistrationId = ViewMeetingRegistrationHotel.MeetingRegistrationId
		and ViewMeetingRegistrationHotel.CancellationFlag = 0
		LEFT JOIN Hotel								  ON ViewMeetingRegistrationHotel.HotelId = Hotel.HotelId
		LEFT JOIN ViewMeetingRegistrationAttendance   ON ViewMeetingRegistration.MeetingRegistrationId = ViewMeetingRegistrationAttendance.MeetingRegistrationId
		LEFT JOIN ViewMeetingRegistrationTravel 	  ON ViewMeetingRegistration.MeetingRegistrationId = ViewMeetingRegistrationTravel.MeetingRegistrationId
		LEFT JOIN ViewMeetingRegistrationComment 	  ON ViewMeetingRegistration.MeetingRegistrationId = ViewMeetingRegistrationComment.MeetingRegistrationId
		LEFT JOIN TravelMode						  ON ViewMeetingRegistrationTravel.TravelModeId = TravelMode.TravelModeId
		LEFT JOIN ViewMeetingRegistrationTravelFlight ON ViewMeetingRegistrationTravel.MeetingRegistrationTravelId = ViewMeetingRegistrationTravelFlight.MeetingRegistrationTravelId
		left join Airport on ViewMeetingRegistrationTravelFlight.DepartureCity = Airport.Code

END
 GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportTravel] TO [NetSqlAzMan_Users]
    AS [dbo];                   
                  
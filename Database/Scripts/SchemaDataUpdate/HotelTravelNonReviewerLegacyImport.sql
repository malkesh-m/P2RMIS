INSERT INTO [dbo].[SessionUserAssignment]
           ([UserId]
           ,[MeetingSessionId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT DISTINCT ViewUser.UserId, ViewMeetingSession.MeetingSessionId, VUN.UserID, mreg.Last_Update_Date
FROM [$(P2RMIS)].dbo.MTG_Registration mreg INNER JOIN
[$(P2RMIS)].dbo.PRG_Participants part ON mreg.Prg_Part_ID = part.Prg_Part_ID INNER JOIN
ViewClientMeeting ON mreg.Meeting_ID = ViewclientMeeting.LegacyMeetingId INNER JOIN
ViewMeetingSession ON ViewClientMeeting.ClientMeetingId = ViewMeetingSession.ClientMeetingId INNER JOIN
ViewUser ON part.Person_ID = ViewUser.PersonID LEFT JOIN
ViewLegacyUserNameToUserId VUN ON mreg.Last_Updated_By = VUN.UserName
WHERE part.Panel_ID IS NULL;

INSERT INTO [dbo].[MeetingRegistration]
           ([PanelUserAssignmentId]
           ,[SessionUserAssignmentId]
           ,[LegacyMrId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT NULL, ViewSessionUserAssignment.SessionUserAssignmentId, mreg.MR_ID, VUN.UserID, mreg.Last_Update_Date
FROM [$(P2RMIS)].dbo.MTG_Registration mreg INNER JOIN
[$(P2RMIS)].dbo.PRG_Participants part ON mreg.Prg_Part_ID = part.Prg_Part_ID INNER JOIN
ViewClientMeeting ON mreg.Meeting_ID = ViewclientMeeting.LegacyMeetingId INNER JOIN
ViewMeetingSession ON ViewClientMeeting.ClientMeetingId = ViewMeetingSession.ClientMeetingId INNER JOIN
ViewUser ON part.Person_ID = ViewUser.PersonID INNER JOIN
ViewSessionUserAssignment ON ViewMeetingSession.MeetingSessionId = ViewSessionUserAssignment.MeetingSessionId AND ViewUser.UserID = ViewSessionUserAssignment.UserId LEFT JOIN
ViewLegacyUserNameToUserId VUN ON mreg.Last_Updated_By = VUN.UserName
WHERE part.Panel_ID IS NULL;

INSERT INTO [dbo].[MeetingRegistrationHotel]
           ([MeetingRegistrationId]
           ,[HotelRequiredFlag]
           ,[HotelId]
           ,[HotelCheckInDate]
           ,[HotelCheckOutDate]
           ,[HotelDoubleOccupancy]
           --,[HotelAndFoodRequestComments] See below script for update
           ,[ExtraNightsRequestComments]
           ,[LegacyHousingId]
           ,[CancellationFlag]
           ,[CancellationDate]
           --,[ParticipantModifiedDate] See below script for update
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ViewMeetingRegistration.MeetingRegistrationId, CASE WHEN mhous.NoHotel = 0 THEN 1 ELSE 0 END, Hotel.HotelId, mhous.CheckIn, mhous.CheckOut,
 ISNULL(mhous.Double_Occupancy, 0), mhous.Additional_Nights, mhous.Housing_ID, CancellationFlag = CASE WHEN mhous.Canceled IS NOT NULL THEN 1 ELSE 0 END, CancellationDate = mhous.Canceled,
 VUN.UserID, mhous.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.MTG_Housing mhous INNER JOIN
ViewMeetingRegistration ON mhous.MR_ID = ViewMeetingRegistration.LegacyMrId INNER JOIN
ViewSessionUserAssignment ON ViewMeetingRegistration.SessionUserAssignmentId = ViewSessionUserAssignment.SessionUserAssignmentId LEFT JOIN
Hotel ON mhous.Hotel_ID = Hotel.LegacyHotelId LEFT JOIN
ViewLegacyUserNameToUserId VUN ON mhous.Last_Updated_By = VUN.UserName

--The following are updates for consistency to the original script, this will be run anyway when deployed to additional environemnts
UPDATE MeetingRegistrationHotel
SET HotelAndFoodRequestComments = House.Special_Requests + CASE WHEN LEN(LTRIM(ISNULL(House.Special_Requests, ''))) > 0 THEN CHAR(10) + CHAR(13) + CHAR(10) + CHAR(13) ELSE '' END + House.Additional_Nights
FROM MeetingRegistrationHotel
INNER JOIN [$(P2RMIS)].dbo.MTG_Housing House ON MeetingRegistrationHotel.LegacyHousingId = House.Housing_ID
WHERE MeetingRegistrationHotel.HotelAndFoodRequestComments IS NULL AND MeetingRegistrationHotel.DeletedFlag = 0 AND (LEN(LTRIM(ISNULL(House.Special_Requests, ''))) > 0 OR LEN(LTRIM(ISNULL(House.Additional_Nights, ''))) > 0);

UPDATE MeetingRegistrationHotel
SET ParticipantModifiedDate = House.Part_Last_Updated
FROM MeetingRegistrationHotel
INNER JOIN [$(P2RMIS)].dbo.MTG_Housing House ON MeetingRegistrationHotel.LegacyHousingId = House.Housing_ID
WHERE MeetingRegistrationHotel.ParticipantModifiedDate IS NULL AND MeetingRegistrationHotel.DeletedFlag = 0;

INSERT INTO [dbo].[MeetingRegistrationTravel]
           ([MeetingRegistrationId]
           ,[TravelModeId]
           ,[TravelRequestComments]
           ,[LegacyTravelId]
           ,[CancellationFlag]
           ,[CancellationDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[ReservationCode]
           ,[Fare]
           ,[AgentFee]
           ,[AgentFee2]
           ,[ChangeFee]
           ,[Ground]
           ,[GsaRate]
           ,[NoGsa]
           ,[ClientApprovedAmount]
           ,[NteAmount])
SELECT ViewMeetingRegistration.MeetingRegistrationId, TravelMode.TravelModeId, travel.Comments, travel.Travel_ID, CASE WHEN travel.Canceled IS NOT NULL THEN 1 ELSE 0 END, CancellationDate = travel.Canceled,
VUN.UserID, travel.LAST_UPDATE_DATE, travel.Reservation, travel.Fare, travel.agent_Fee_1, travel.Agent_Fee_2, travel.Change_Fee_2, ISNULL(travel.Ground, 0), travel.GSA_Rate, CASE WHEN LTRIM(RTRIM(Travel.No_GSA)) = 'Y' THEN 1 ELSE 0 END, travel.Client_Approved_Amount, travel.NTE_Amount
FROM [$(P2RMIS)].dbo.MTG_Travel travel INNER JOIN
ViewMeetingRegistration ON travel.MR_ID = ViewMeetingRegistration.LegacyMrId INNER JOIN
ViewSessionUserAssignment ON ViewMeetingRegistration.SessionUserAssignmentId = ViewSessionUserAssignment.SessionUserAssignmentId LEFT JOIN
TravelMode ON travel.Travel_Mode = TravelMode.LegacyTravelModeAbbreviation LEFT JOIN
ViewLegacyUserNameToUserId VUN ON travel.Last_Updated_By = VUN.UserName;

INSERT INTO [MeetingRegistrationTravelFlight]
           ([MeetingRegistrationTravelId]
           ,[CarrierName]
           ,[FlightNumber]
           ,[DepartureCity]
           ,[DepartureDate]
           ,[ArrivalCity]
           ,[ArrivalDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ViewMeetingRegistrationTravel.MeetingRegistrationTravelId, TravelLegs.Carrier, TravelLegs.Flight_Number, TravelLegs.Depart_City, TravelLegs.Depart_Time,
TravelLegs.Arrival_City, TravelLegs.Arrival_Time, VUN.UserID, TravelLegs.LAST_UPDATE_DATE
FROM ViewMeetingRegistrationTravel
INNER JOIN ViewMeetingRegistration ON ViewMeetingRegistrationTravel.MeetingRegistrationId = ViewMeetingRegistration.MeetingRegistrationId
INNER JOIN [$(P2RMIS)].dbo.MTG_Travel Travel ON ViewMeetingRegistrationTravel.LegacyTravelId = Travel.Travel_ID
INNER JOIN [$(P2RMIS)].dbo.MTG_Travel_Legs TravelLegs ON Travel.Travel_ID = TravelLegs.Travel_ID
LEFT OUTER JOIN ViewLegacyUserNameToUserId VUN ON TravelLegs.LAST_UPDATED_BY = VUN.UserName
WHERE ViewMeetingRegistration.SessionUserAssignmentId IS NOT NULL;

INSERT INTO [dbo].[MeetingRegistrationComment]
           ([MeetingRegistrationId]
           ,[InternalComments])
SELECT ViewMeetingRegistrationHotel.MeetingRegistrationId, House.Internal_Comments
FROM ViewMeetingRegistrationHotel
INNER JOIN ViewMeetingRegistration ON ViewMeetingRegistrationHotel.MeetingRegistrationId = ViewMeetingRegistration.MeetingRegistrationId
INNER JOIN [$(P2RMIS)].dbo.MTG_Housing House ON ViewMeetingRegistrationHotel.LegacyHousingId = House.Housing_ID
WHERE LEN(LTRIM(ISNULL(House.Internal_Comments, ''))) > 0 and ViewMeetingRegistration.SessionUserAssignmentId IS NOT NULL;

INSERT INTO [dbo].[MeetingRegistrationAttendance]
           ([MeetingRegistrationId]
           ,[AttendanceStartDate]
           ,[AttendanceEndDate]
           ,[MealRequestComments]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT MeetingRegistration.MeetingRegistrationId, MIN(MTG_Attendance.Schd_Attend_Day), MAX(MTG_Attendance.Schd_Attend_Day), MTG_Housing.Special_Requests,
		(Select TOP(1) VUN.UserId FROM ViewLegacyUserNameToUserId VUN WHERE VUN.UserName = MAX(MTG_Attendance.LAST_UPDATED_BY)), MAX(MTG_Attendance.LAST_UPDATE_DATE)
FROM [$(P2RMIS)].dbo.MTG_Attendance MTG_Attendance INNER JOIN
	MeetingRegistration ON MTG_Attendance.MR_ID = MeetingRegistration.LegacyMrId LEFT OUTER JOIN
	[$(P2RMIS)].dbo.MTG_Housing MTG_Housing ON MTG_Attendance.MR_ID = MTG_Housing.MR_ID
WHERE MeetingRegistration.DeletedFlag = 0 AND MeetingRegistration.SessionUserAssignmentId IS NOT NULL
GROUP BY MeetingRegistration.MeetingRegistrationId, MTG_Housing.Special_Requests
--Migrating the rest of hotel and travel data from 1.0

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

UPDATE MeetingRegistrationTravel
SET ReservationCode = Travel.Reservation, Fare = Travel.Fare, AgentFee = Travel.agent_Fee_1, AgentFee2 = Travel.Agent_Fee_2, ChangeFee = Travel.Change_Fee_2, Ground = ISNULL(Travel.Ground, 0), GsaRate = Travel.GSA_Rate, NoGsa = CASE WHEN LTRIM(RTRIM(Travel.No_GSA)) = 'Y' THEN 1 ELSE 0 END , ClientApprovedAmount = Travel.Client_Approved_Amount
FROM MeetingRegistrationTravel
INNER JOIN [$(P2RMIS)].dbo.MTG_Travel Travel ON MeetingRegistrationTravel.LegacyTravelId = Travel.Travel_ID
WHERE MeetingRegistrationTravel.DeletedFlag = 0;

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
INNER JOIN [$(P2RMIS)].dbo.MTG_Travel Travel ON ViewMeetingRegistrationTravel.LegacyTravelId = Travel.Travel_ID
INNER JOIN [$(P2RMIS)].dbo.MTG_Travel_Legs TravelLegs ON Travel.Travel_ID = TravelLegs.Travel_ID
LEFT OUTER JOIN ViewLegacyUserNameToUserId VUN ON TravelLegs.LAST_UPDATED_BY = VUN.UserName;

INSERT INTO [dbo].[MeetingRegistrationComment]
           ([MeetingRegistrationId]
           ,[InternalComments])
SELECT ViewMeetingRegistrationHotel.MeetingRegistrationId, House.Internal_Comments
FROM ViewMeetingRegistrationHotel
INNER JOIN [$(P2RMIS)].dbo.MTG_Housing House ON ViewMeetingRegistrationHotel.LegacyHousingId = House.Housing_ID
WHERE LEN(LTRIM(ISNULL(House.Internal_Comments, ''))) > 0;




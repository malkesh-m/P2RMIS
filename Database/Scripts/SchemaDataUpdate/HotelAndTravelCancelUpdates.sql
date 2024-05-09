--Set cancelled flag for hotel
UPDATE MeetingRegistrationHotel
SET CancellationFlag = CASE WHEN MTG_Housing.Canceled IS NOT NULL THEN 1 ELSE 0 END, CancellationDate = MTG_Housing.Canceled
FROM [$(P2RMIS)].dbo.MTG_Housing MTG_Housing 
INNER JOIN MeetingRegistrationHotel ON MTG_Housing.Housing_ID = MeetingRegistrationHotel.LegacyHousingId
--Set cancelled flag and NTE amount for travel
UPDATE MeetingRegistrationTravel
SET CancellationFlag = CASE WHEN MTG_Travel.Canceled IS NOT NULL THEN 1 ELSE 0 END, CancellationDate = MTG_Travel.Canceled, NteAmount = MTG_Travel.NTE_Amount
FROM [$(P2RMIS)].dbo.MTG_Travel MTG_Travel
INNER JOIN MeetingRegistrationTravel ON MTG_Travel.Travel_ID = MeetingRegistrationTravel.LegacyTravelId
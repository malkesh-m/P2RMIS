﻿CREATE VIEW [dbo].[ViewMeetingRegistrationTravel]
	AS SELECT [MeetingRegistrationTravelId]
      ,[MeetingRegistrationId]
      ,[TravelModeId]
      ,[TravelRequestComments]
      ,[LegacyTravelId]
	  ,[NteAmount]
	  ,[CancellationFlag]
	  ,[CancellationDate]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
	  ,[ReservationCode]
	  ,[Fare]
	  ,[AgentFee]
	  ,[AgentFee2]
	  ,[ChangeFee]
	  ,[Ground]
	  ,[GsaRate]
	  ,[NoGsa]
	  ,[ClientApprovedAmount]
  FROM [dbo].[MeetingRegistrationTravel]
  WHERE DeletedFlag = 0


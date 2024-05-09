INSERT INTO [dbo].[ClientMeeting] (
-- auto generated
	--[ClientMeetingId]
      [LegacyMeetingId]
      ,[ClientId]
      ,[MeetingAbbreviation]
      ,[MeetingDescription]
      ,[StartDate]
      ,[EndDate]
      ,[MeetingLocation]
      ,[MeetingTypeId]
     -- ,[CreatedBy]
     -- ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate])

      /*
      Meetings without any assigned panels not included
      */
     
      SELECT DISTINCT
      mm.[Meeting_ID] LegacyMtgID
      --,pl.[client] ClientFromProgramLU
      ,c.[clientid] ClientID
      ,mm.[Meeting_ID] MtgAbbrev
      ,mm.[Meeting_Desc] MeetingDesc
      ,mm.[Start_Date] StartDate
      ,mm.[End_Date] EndDate
      ,mm.[Meeting_Location] MtgLoc
      ,ISNULL(mt.[MeetingTypeId], 1) MtgTypeID
      ,v.[userid] ModBy
      ,mm.[LAST_UPDATE_DATE] ModDate

  FROM [$(P2RMIS)].[dbo].[MTG_Master] mm
	inner join [$(P2RMIS)].[dbo].[MTG_Session] mp on mm.[Meeting_ID] = mp.[Meeting_ID]
	inner join [$(P2RMIS)].[dbo].[PAN_Master] pan on mp.Session_ID = pan.Session_ID
	inner join [$(P2RMIS)].[dbo].[PRG_Program_LU] pl on pan.[program] = pl.[program]
	inner join [dbo].[Client] c on pl.[client] = c.[clientAbrv] 
	left join [dbo].[ViewLegacyUserNameToUserId] v on mm.[LAST_UPDATED_BY] = v.[username]
    left join [dbo].[MeetingType] mt on mm.[review_type] = mt.[LegacyMeetingTypeId]
  WHERE NOT EXISTS (Select 'X' From ClientMeeting WHERE ClientMeeting.LegacyMeetingId = mm.Meeting_ID AND ClientMeeting.ClientId = c.ClientID)
  

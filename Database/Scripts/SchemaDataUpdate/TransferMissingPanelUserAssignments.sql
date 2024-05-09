INSERT INTO [dbo].[PanelUserAssignment]
( 
      [SessionPanelId]
      ,[UserId]
      ,[ClientParticipantTypeId]
      ,[ClientRoleId]
      ,[LegacyParticipantId]
	  ,[RestrictedAssignedFlag]
	  ,[ParticipationMethodId]
      ,[ModifiedBy]
      ,[ModifiedDate]
 )
SELECT
    sp.[sessionPanelId] SessionPanelID
    ,u.[userid] UserId
    ,cpt.[ClientParticipantTypeId] ClientParticipantTypeId
    ,cr.[ClientRoleID] ClientRoleID
    ,p.[Prg_Part_ID] LegacyParticipantTypeId
	,PartMapping.RestrictedAssignedFlag
	,PartMapping.NewParticipantMethod
    ,v.[userid] ModifiedBy
    ,p.[LAST_UPDATE_DATE] ModifiedDate
  FROM [$(P2RMIS)].[dbo].[PRG_Participants] p
  inner join [dbo].[ViewSessionPanel] sp on p.[panel_id] = sp.[LegacyPanelId]
  inner join [$(P2RMIS)].[dbo].[PRG_Program_LU] prg on p.Program = prg.Program
  inner join [dbo].[Client] c on prg.Client = c.ClientAbrv 
  inner join ViewMeetingSession ms on sp.MeetingSessionId = ms.MeetingSessionId
  inner join ViewClientMeeting cm on ms.ClientMeetingId = cm.ClientMeetingId
  left join [dbo].[ClientRole] cr on p.[part_role_type] = cr.[RoleAbbreviation] and ISNULL(cm.[ClientId], 19) = cr.[ClientId]
  cross apply [dbo].udfLegacyToNewParticipantTypeMap(p.Part_Type, ISNULL(CR.SpecialistFlag, 0), CM.MeetingTypeId, CM.ClientId) PartMapping 
  INNER JOIN [dbo].[ClientParticipantType] cpt ON  PartMapping.NewParticipantTypeAbbreviation = cpt.ParticipantTypeAbbreviation AND
				CM.ClientId = cpt.ClientId
  inner join [dbo].[viewuser] u on p.[person_id] = u.[personid]
  left join [dbo].[ViewLegacyUserNameToUserId] v on p.[LAST_UPDATED_BY] = v.[username]
  WHERE p.Panel_ID IS NOT NULL AND sp.SessionPanelID IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewPanelUserAssignment WHERE LegacyParticipantId = p.Prg_Part_ID)
AND p.Prg_Part_ID IN (110235,110206,110187,110183,101893,110196,110188,110197,106671,103648,102606,100384,100379,98271,91559,111266,110201,110203,110209,101594,101482,110193,110186,101509,110210,110177,110179,110180);
  
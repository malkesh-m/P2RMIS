INSERT INTO [dbo].[ProgramUserAssignment]
( --[ProgramUserAssignmentId]
      [ProgramYearId]
      ,[UserId]
      ,[ClientParticipantTypeId]
      ,[LegacyParticipantId]
      --,[CreatedBy]
      --,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
 )
  SELECT 
      py.[ProgramYearId] ProgramYearId
      ,u.[userid] UserId
      --,p.[Person_ID]
      --,p.[Program] 
      --,p.[FY]
      --,p.[Part_Type] 
      --,cpt.[LegacyPartTypeId]
      ,cpt.[ClientParticipantTypeId] ClientParticipantTypeId
      ,p.[Prg_Part_ID] LegacyParticipantID
      --,p.[Part_Role_Type]
      --,p.[Panel_ID]
      --,p.[EC_ID]
      --,p.[RC_ID]
      --,p.[Comments]
      ,v.[userid] ModifiedBy
      ,p.[LAST_UPDATE_DATE] ModifiedDate
      --,p.[LAST_UPDATED_BY]
      --,p.[BC_ID]
  FROM [$(P2RMIS)].[dbo].[PRG_Participants] p
  left join [dbo].[ClientProgram] cp on p.[program] = cp.[LegacyProgramId] 
  left join [dbo].[ClientParticipantType] cpt on  p.[part_type] = cpt.[LegacyPartTypeId] AND cp.ClientId = cpt.ClientId
  left join [dbo].[ProgramYear] py on p.[fy] = py.[year] and cp.[ClientProgramId] = py.[ClientProgramId]
 left join [dbo].[user] u on p.[person_id] = u.[personid]
  left join [dbo].[ViewLegacyUserNameToUserId] v on p.[LAST_UPDATED_BY] = v.[username]
  --Remove participants with programs that do not exist
  where p.panel_id is null AND py.ProgramYearId IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewProgramUserAssignment WHERE ProgramYearId = py.ProgramYearId AND LegacyParticipantId = p.Prg_Part_ID) 
  order by py.[ProgramYearId]
  
 

INSERT INTO [dbo].[ClientParticipantType]
	([ClientId]
      ,[LegacyPartTypeId]
      ,[ParticipantTypeAbbreviation]
      ,[ParticipantTypeName]
      ,[ParticipantScope]
	  ,[ReviewerFlag]
      ,[ModifiedBy]
      ,[ModifiedDate])

SELECT distinct c.[clientid] ClientID
	-- change Constella to SRA for legacy part type ID
     ,pptl.[Part_Type] AS LegacyPartTypeID 
     -- change Constella to SRA for part type abbreviation
      ,(CASE WHEN pptl.[Part_Type]  = 'Constella' THEN 'SRA' WHEN pptl.[Part_Type]  = 'SRA' THEN 'SRO' ELSE pptl.[Part_Type] END) PartTypeAbbrev
      ,pptl.[Part_Desc] ParticipantTypeName2
      -- keep Panel as Panel, change Meeting to Program, keep Program as Program, no other data existed, so no else was added
      ,(CASE WHEN pptl.[Part_Category] = 'Panel' THEN 'Panel' WHEN pptl.[Part_Category] = 'Meeting' THEN 'Program' WHEN pptl.[Part_Category] = 'Program' THEN 'Program' END) ParticipantScope
	  ,(CASE WHEN pptl.Part_Type IN ('SR', 'CH', 'CR', 'CHT', 'OR', 'OCR', 'AH', 'OCH', 'TC', 'CRT') THEN 1 ELSE 0 END)
      ,v.[userid]
      ,pptl.[LAST_UPDATE_DATE]
  FROM [$(P2RMIS)].[dbo].[PRG_Part_Type_LU] pptl
  left join [$(P2RMIS)].[dbo].[PRG_Participants] pp on pptl.[part_type] = pp.[part_type]
  left join [$(P2RMIS)].[dbo].[PRG_Program_LU] ppl on pp.[program] = ppl.[program]
  left join [dbo].[Client] C on ppl.[Client] = c.[CLIENTAbrv]
  left join [dbo].[ViewLegacyUserNameToUserId] v on pptl.[LAST_UPDATED_BY] = v.[username]
 -- don't add participant types when a client is not associated with that participant type
  where c.[clientid] is not null 
  -- we don't want to re-run this insert if it has already been run
  and (select count(*) from [dbo].[ClientParticipantType])=0
  order by legacyparttypeid

  --Update reviewer flag since we added that late
  UPDATE ClientParticipantType SET ChairpersonFlag = 1 WHERE ParticipantTypeAbbreviation = 'CH' OR ParticipantTypeAbbreviation = 'OCH' OR ParticipantTypeAbbreviation = 'CHT'
  --Update consumer review flag since we added that late
  UPDATE ClientParticipantType SET ConsumerFlag = 1 WHERE ParticipantTypeAbbreviation IN ('CR','CRT', 'OCR')
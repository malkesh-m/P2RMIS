
 INSERT INTO [dbo].[ClientProgram] 
      ([LegacyProgramId]
      ,[ClientId]
      ,[ProgramAbbreviation]
      ,[ProgramDescription]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate])
 
  SELECT ppl.[PROGRAM] LegacyProgramID
		,c.[clientid] ClientID
		,ppl.[PROGRAM] ProgramAbbrev
      ,ppl.[DESCRIPTION]
      --,ppl.[CLIENT]
	  ,v.[userid] ModifiedBy
      ,ppl.[LAST_UPDATE_DATE] ModifiedDate
      ,v.[userid] ModifiedBy
      ,ppl.[LAST_UPDATE_DATE] ModifiedDate
      --,ppl.[LAST_UPDATED_BY]
      --,ppl.[BS_Class]
      --,ppl.[W9_Info]
	FROM [$(P2RMIS)].[dbo].[PRG_Program_LU] ppl
	left join [dbo].[Client] C on ppl.[Client] = c.[CLIENTAbrv]
	left join [dbo].[ViewLegacyUserNameToUserId] v on ppl.[LAST_UPDATED_BY] = v.[username]
	
	-- do not add rows that are already present
	where ppl.[PROGRAM] not in 
	(select [LegacyProgramId]
	from [dbo].[ClientProgram])

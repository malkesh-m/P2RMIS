
-- The unique constraint must be dropped before this script will work.
-- ALTER TABLE [dbo].[ClientAwardType] DROP CONSTRAINT UN_ClientAwardType_ClientId_AwardAbbreviation

  insert into [dbo].[ClientAwardType]
		([ClientId]
		,[LegacyAwardTypeId]
		,[AwardAbbreviation]
		,[AwardDescription]
		,[ModifiedDate]
		,[ModifiedBy])		
		
	SELECT DISTINCT c.[clientid]
	,pat.[AWARD_TYPE]
	,pat.[SHORT_DESC]
    ,pat.[DESCRIPTION]
	,pat.[LAST_UPDATE_DATE]
    ,v.[userid]
     --,pat.[AWARD_CATEGORY]
     --,pat.[CENTER]
     --,pat.[REV_TEMP_LOC]
     --,pat.[sort_order]
   FROM [$(P2RMIS)].[dbo].[PRO_Award_Type] PAT
	left join [$(P2RMIS)].[dbo].[PRO_Award_Type_Member] PATM ON PAT.[AWARD_TYPE] = PATM.[AWARD_TYPE]
	left join [$(P2RMIS)].[dbo].[PRG_Program_PA] PP ON patm.[pa_id] = pp.[pa_id]
	left join [$(P2RMIS)].[dbo].[PRG_Program] P on pp.[prg_id] = p.[prg_id]
	left join [$(P2RMIS)].[dbo].[PRG_Program_LU] PL ON  p.[program] = pl.[program]
	left join [dbo].[Client] C on pl.[Client] = c.[CLIENTAbrv]
	left join [dbo].[ViewLegacyUserNameToUserId] v on pat.[LAST_UPDATED_BY] = v.[username]
	
	-- do not add any clients that are not associated with an award type
	where c.[clientid] is not null 
	-- do not add any award data that is already in the new table
	and 
	NOT EXISTS (Select 'X' From ClientAwardType WHERE ClientAwardType.LegacyAwardTypeId = pat.AWARD_TYPE AND ClientAwardType.ClientId = c.ClientID)
	order by pat.[AWARD_TYPE]
	
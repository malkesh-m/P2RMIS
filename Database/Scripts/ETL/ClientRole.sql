
 insert into [dbo].[ClientRole]
      ([ClientId]
      ,[LegacyRoleId] 
      ,[RoleAbbreviation] 
      ,[RoleName] 
      ,[ModifiedBy] 
      ,[ModifiedDate]
      ,[ActiveFlag]
	  ,[SpecialistFlag]) 
 
  SELECT c.[clientid] ClientID 
	,ppt.[Role_ID] LegacyRoleID  
      ,ppt.[Part_Role_Type] RoleAbbrev 
      ,ppt.[Part_Role_Desc] RoleName  
      ,v.[userid]
      ,ppt.[Last_Update_Date]
      --,ppt.[active]
      -- if active is null, set active to 1
      ,(CASE WHEN ppt.active is NULL THEN 1 ELSE ppt.active END) newactive
	  ,(CASE WHEN ppt.Part_Role_Type IN ('BS', 'CR', 'NCR', 'MCR') THEN 0 ELSE 1 END)

  FROM [$(P2RMIS)].[dbo].[PRG_Part_Role_Type] ppt
  left join [dbo].[Client] C on ppt.[Client] = c.[CLIENTAbrv]
	left join [dbo].[ViewLegacyUserNameToUserId] v on ppt.[LAST_UPDATED_BY] = v.[username]
	
	-- if data exists, we do not want to add it again
	where ppt.[Role_ID] not in
	(select [LegacyRoleId]
	from [dbo].[ClientRole]) and ppt.Role_ID <> 38
	order by ppt.[Role_ID]
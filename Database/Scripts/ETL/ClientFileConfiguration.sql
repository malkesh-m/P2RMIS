INSERT INTO [dbo].[ClientFileConfiguration]
           ([ClientId]
           ,[FileSuffix]
           ,[DisplayLabel]
           ,[AbstractFlag]
		   ,[ModifiedBy]
		   ,[ModifiedDate])
SELECT Client.ClientId, PRG_Client_File_Type.File_Extension, PRG_Client_File_Type.Label, CASE WHEN PRG_Client_File_Type.Label LIKE '%abstract%' THEN 1 ELSE 0 END,
VUN.UserId, PRG_Client_File_Type.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRG_Client_File_Type PRG_Client_File_Type 
LEFT OUTER JOIN ViewLegacyUserNameToUserId VUN ON PRG_Client_File_Type.LAST_UPDATED_BY = VUN.UserName
INNER JOIN Client ON PRG_Client_File_Type.Client = Client.ClientAbrv
WHERE NOT EXISTS (SELECT 'X' FROM ClientFileConfiguration WHERE ClientId = Client.ClientID AND FileSuffix = PRG_Client_File_Type.File_Extension AND DisplayLabel = PRG_Client_File_Type.Label);
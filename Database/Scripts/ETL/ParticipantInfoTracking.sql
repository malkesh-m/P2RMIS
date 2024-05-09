INSERT INTO [dbo].[ParticipantInfoTracking]
           ([PrgPartId]
           ,[DocumentName]
           ,[DocumentText]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT Coi.PRG_Part_ID, CASE WHEN Coi.Doc_ID = 1 THEN 'Acknowledgment' ELSE 'Bias and Conflict of Interest' END, Coi.Doc_Text, VUN.UserID, Coi.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRG_Part_COI_NDA Coi
LEFT JOIN ViewLegacyUserNameToUserId VUN ON Coi.LAST_UPDATED_BY = VUN.UserName
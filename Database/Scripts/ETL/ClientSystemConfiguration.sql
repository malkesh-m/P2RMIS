INSERT INTO [dbo].[ClientConfiguration]
           ([ClientId]
           ,[SystemConfigurationId]
           ,[ConfigurationValue]) 
SELECT [Client].ClientID, 1, CASE WHEN Client.ClientID IN (19, 23) THEN 1 ELSE 0 END
FROM [Client]
WHERE NOT EXISTS (Select 'X' FROM ClientConfiguration WHERE ClientId = [Client].ClientID)
MERGE INTO [CGMS_Data_Transfer].[dbo].[AwardType] AS Target
USING (SELECT [ClientAwardTypeId]
      ,[AwardAbbreviation]
      ,[AwardDescription]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[ViewClientAwardType]
  WHERE ClientId = 9
) AS Source 
ON (Target.[AwardTypeId] = Source.[ClientAwardTypeId])
WHEN MATCHED THEN
 UPDATE SET [AwardTypeId] = Source.[ClientAwardTypeId]
      ,[AwardAbbreviation] = Source.[AwardAbbreviation]
      ,[AwardDescription] = Source.[AwardDescription]
      ,[CreatedBy] = Source.[CreatedBy]
      ,[CreatedDate] = Source.[CreatedDate]
      ,[ModifiedBy] = Source.[ModifiedBy]
      ,[ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT ([AwardTypeId]
           ,[AwardAbbreviation]
           ,[AwardDescription]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
     VALUES
           (Source.[ClientAwardTypeId]
           ,Source.[AwardAbbreviation]
           ,Source.[AwardDescription]
           ,Source.[CreatedBy]
           ,Source.[CreatedDate]
           ,Source.[ModifiedBy]
           ,Source.[ModifiedDate])
WHEN NOT MATCHED BY SOURCE THEN
DELETE;
GO
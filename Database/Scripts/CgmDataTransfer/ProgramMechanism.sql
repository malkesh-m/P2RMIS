MERGE INTO [CGMS_Data_Transfer].[dbo].[ProgramMechanism] AS Target
USING (SELECT [ViewProgramMechanism].[ProgramMechanismId]
      ,[ViewProgramMechanism].[ProgramYearId]
      ,[ViewProgramMechanism].[ClientAwardTypeId]
      ,[ViewProgramMechanism].[ReceiptCycle]
      ,[ViewProgramMechanism].[ReceiptDeadline]
      ,[ViewProgramMechanism].[CreatedBy]
      ,[ViewProgramMechanism].[CreatedDate]
      ,[ViewProgramMechanism].[ModifiedBy]
      ,[ViewProgramMechanism].[ModifiedDate]
  FROM [dbo].[ViewProgramMechanism] INNER JOIN
  [dbo].[ViewClientAwardType] ON [ViewProgramMechanism].[ClientAwardTypeId] = [ViewClientAwardType].[ClientAwardTypeId]
  WHERE ClientId = 9
) AS Source 
ON (Target.[ProgramMechanismId] = Source.[ProgramMechanismId])
WHEN MATCHED THEN
 UPDATE SET [ProgramMechanismId] = Source.[ProgramMechanismId]
      ,[ProgramYearId] = Source.[ProgramYearId]
      ,[AwardTypeId] = Source.[ClientAwardTypeId]
      ,[ReceiptCycle] = Source.[ReceiptCycle]
      ,[CreatedBy] = Source.[CreatedBy]
      ,[CreatedDate] = Source.[CreatedDate]
      ,[ModifiedBy] = Source.[ModifiedBy]
      ,[ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT ([ProgramMechanismId]
           ,[ProgramYearId]
           ,[AwardTypeId]
           ,[ReceiptCycle]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
     VALUES
           (Source.[ProgramMechanismId]
           ,Source.[ProgramYearId]
           ,Source.[ClientAwardTypeId]
           ,Source.[ReceiptCycle]
           ,Source.[CreatedBy]
           ,Source.[CreatedDate]
           ,Source.[ModifiedBy]
           ,Source.[ModifiedDate])
WHEN NOT MATCHED BY SOURCE THEN
DELETE;
GO
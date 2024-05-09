MERGE INTO [CGMS_Data_Transfer].[dbo].[ProgramYear] AS Target
USING (SELECT [ViewProgramYear].[ProgramYearId]
      ,[ViewProgramYear].[ClientProgramId]
      ,[ViewProgramYear].[Year]
      ,[ViewProgramYear].[DateClosed]
      ,[ViewProgramYear].[CreatedDate]
      ,[ViewProgramYear].[CreatedBy]
      ,[ViewProgramYear].[ModifiedDate]
      ,[ViewProgramYear].[ModifiedBy]
  FROM [dbo].[ViewProgramYear]
  INNER JOIN [ClientProgram] ON [ViewProgramYear].[ClientProgramId] = [ClientProgram].ClientProgramId
  WHERE [ClientProgram].[ClientId] = 9
) AS Source 
ON (Target.[ProgramYearId] = Source.[ProgramYearId])
WHEN MATCHED THEN
 UPDATE SET [ProgramYearId] = Source.[ProgramYearId]
      ,[ProgramId] = Source.[ClientProgramId]
      ,[Year] = Source.[Year]
      ,[DateClosed] = Source.[DateClosed]
      ,[CreatedDate] = Source.[CreatedDate]
      ,[CreatedBy] = Source.[CreatedBy]
      ,[ModifiedDate] = Source.[ModifiedDate]
      ,[ModifiedBy] = Source.[ModifiedBy]
WHEN NOT MATCHED BY TARGET THEN
 INSERT ([ProgramYearId]
           ,[ProgramId]
           ,[Year]
           ,[DateClosed]
           ,[CreatedDate]
           ,[CreatedBy]
           ,[ModifiedDate]
           ,[ModifiedBy])
     VALUES
           (Source.[ProgramYearId]
           ,Source.[ClientProgramId]
           ,Source.[Year]
           ,Source.[DateClosed]
           ,Source.[CreatedDate]
           ,Source.[CreatedBy]
           ,Source.[ModifiedDate]
           ,Source.[ModifiedBy])
WHEN NOT MATCHED BY SOURCE THEN
DELETE;
GO
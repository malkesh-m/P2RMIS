MERGE INTO [CGMS_Data_Transfer].[dbo].[Program] AS Target
USING (SELECT [ClientProgramId]
      ,[ProgramAbbreviation]
      ,[ProgramDescription]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[ClientProgram]
  WHERE [ClientId] = 9
) AS Source 
ON (Target.[ProgramId] = Source.[ClientProgramId])
WHEN MATCHED THEN
 UPDATE SET
  [ProgramId] = Source.ClientProgramId
      ,[ProgramAbbreviation] = Source.ProgramAbbreviation
      ,[ProgramDescription] = Source.ProgramDescription
      ,[CreatedBy] = Source.CreatedBy
      ,[CreatedDate] = Source.CreatedDate
      ,[ModifiedBy] = Source.ModifiedBy
      ,[ModifiedDate] = Source.ModifiedDate
WHEN NOT MATCHED BY TARGET THEN
 INSERT ([ProgramId]
           ,[ProgramAbbreviation]
           ,[ProgramDescription]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
     VALUES
           (Source.ClientProgramId
           ,Source.ProgramAbbreviation
           ,Source.ProgramDescription
           ,Source.CreatedBy
           ,Source.CreatedDate
           ,Source.ModifiedBy
           ,Source.ModifiedDate)
WHEN NOT MATCHED BY SOURCE THEN
DELETE;
GO
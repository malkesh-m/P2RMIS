MERGE INTO [CGMS_Data_Transfer].[dbo].[AssignmentType] AS Target
USING (SELECT [ClientAssignmentTypeId]
      ,[AssignmentAbbreviation]
      ,[AssignmentLabel]
  FROM [dbo].[ClientAssignmentType]
  WHERE [ClientId] = 9
) AS Source ([ClientAssignmentTypeId],[AssignmentAbbreviation],[AssignmentLabel])
ON (Target.[AssignmentTypeId] = Source.[ClientAssignmentTypeId])
WHEN MATCHED THEN
 UPDATE SET
  [AssignmentAbbreviation] = Source.[AssignmentAbbreviation], 
  [AssignmentLabel] = Source.[AssignmentLabel]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([AssignmentTypeId],[AssignmentAbbreviation],[AssignmentLabel])
 VALUES(Source.[ClientAssignmentTypeId],Source.[AssignmentAbbreviation],Source.[AssignmentLabel])
WHEN NOT MATCHED BY SOURCE THEN
DELETE;
GO
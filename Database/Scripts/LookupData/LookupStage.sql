SET IDENTITY_INSERT [LookupStage] ON
 
MERGE INTO [LookupStage] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (1,'Pending')
 ,(2,'Confirmed')
 ,(3,'Active')
 ,(4,'Invited')
 ,(5,'Locked')
 ,(6,'Deactivated-eligible')
 ,(7,'Reset-Pending Confirmation')
 ,(8,'Invitation Expired')
 ,(9,'Password Expired')
 ,(10,'Deavtivated-ineligible')
 ,(11,'Inactive-eligible')
 ,(12,'Inactive-ineligible')

) AS Source ([StageID],[StageName])
ON (Target.[StageID] = Source.[StageID])
WHEN MATCHED AND (Target.[StageName] <> Source.[StageName]) THEN
 UPDATE SET
 [StageName] = Source.[StageName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([StageID],[StageName])
 VALUES(Source.[StageID],Source.[StageName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [LookupStage]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[LookupStage] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET IDENTITY_INSERT [LookupStage] OFF
GO
SET IDENTITY_INSERT [LookupPrefix] ON
 
MERGE INTO [LookupPrefix] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  (1,'')
 ,(2,'Dr.')
 ,(3,'Miss')
 ,(4,'Mr.')
 ,(5,'Mrs.')
 ,(6,'Ms.')
 ,(7,'Prof.')
 ,(8,'Col')
 ,(9,'COL')
 ,(10,'LCDR')
 ,(11,'CAPT')
 ,(12,'LTC')
 ,(13,'CDR')

) AS Source ([PrefixID],[PrefixName])
ON (Target.[PrefixID] = Source.[PrefixID])
WHEN MATCHED AND (Target.[PrefixName] <> Source.[PrefixName]) THEN
 UPDATE SET
 [PrefixName] = Source.[PrefixName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([PrefixID],[PrefixName])
 VALUES(Source.[PrefixID],Source.[PrefixName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [LookupPrefix]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[LookupPrefix] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET IDENTITY_INSERT [LookupPrefix] OFF
GO
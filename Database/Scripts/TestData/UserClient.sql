SET IDENTITY_INSERT [UserClient] ON
 
MERGE INTO [UserClient] AS Target
USING (VALUES

------------------------------------------------------------------------------------------------
  (2,4,19)
 ,(5,8,19)
 ,(7,10,19)

) AS Source ([UserClientID],[UserID],[ClientID])
ON (Target.[UserClientID] = Source.[UserClientID])
WHEN MATCHED AND (Target.[UserID] <> Source.[UserID] OR Target.[ClientID] <> Source.[ClientID]) THEN
 UPDATE SET
 [UserID] = Source.[UserID], 
[ClientID] = Source.[ClientID]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([UserClientID],[UserID],[ClientID])
 VALUES(Source.[UserClientID],Source.[UserID],Source.[ClientID])
;
 

DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [UserClient]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[UserClient] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END

 
SET IDENTITY_INSERT [UserClient] OFF
GO
MERGE INTO [MeetingType] AS Target
USING (VALUES
  (1,'OS','OS','Onsite Meeting',1)
 ,(2,'TC','TC','Teleconference',3)
 ,(3,'OL','OL','On-Line Discussion',2)
 ,(4,NULL,'VC','Videoconference',4)
) AS Source ([MeetingTypeId],[LegacyMeetingTypeId],[MeetingTypeAbbreviation],[MeetingTypeName],[SortOrder])
ON (Target.[MeetingTypeId] = Source.[MeetingTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[LegacyMeetingTypeId], Target.[LegacyMeetingTypeId]) IS NOT NULL OR NULLIF(Target.[LegacyMeetingTypeId], Source.[LegacyMeetingTypeId]) IS NOT NULL OR 
	NULLIF(Source.[MeetingTypeAbbreviation], Target.[MeetingTypeAbbreviation]) IS NOT NULL OR NULLIF(Target.[MeetingTypeAbbreviation], Source.[MeetingTypeAbbreviation]) IS NOT NULL OR 
	NULLIF(Source.[MeetingTypeName], Target.[MeetingTypeName]) IS NOT NULL OR NULLIF(Target.[MeetingTypeName], Source.[MeetingTypeName]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [LegacyMeetingTypeId] = Source.[LegacyMeetingTypeId], 
  [MeetingTypeAbbreviation] = Source.[MeetingTypeAbbreviation], 
  [MeetingTypeName] = Source.[MeetingTypeName], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([MeetingTypeId],[LegacyMeetingTypeId],[MeetingTypeAbbreviation],[MeetingTypeName],[SortOrder])
 VALUES(Source.[MeetingTypeId],Source.[LegacyMeetingTypeId],Source.[MeetingTypeAbbreviation],Source.[MeetingTypeName],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [MeetingType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[MeetingType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

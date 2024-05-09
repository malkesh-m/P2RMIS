SET IDENTITY_INSERT [TrainingCategory] ON

MERGE INTO [TrainingCategory] AS Target
USING (VALUES
  (1,'Broad Agency Announcement',1,'BA')
 ,(2,'Critique Samples',2,'CS')
 ,(3,'Handbooks',3,'HB')
 ,(4,'Instructions for Applicants',4,'IA')
 ,(5,'Overviews',5,'OV')
 ,(6,'Other',6,'OT')
 ,(7,'Program Announcements',7,'PA')
 ,(8,'Request for Applications',8,'RA')
 ,(9,'Tutorials',9,'EL')
 ,(10,'User Guides',10,'UG')
 ,(11,'Review Criteria',11,'RC')
) AS Source ([TrainingCategoryId],[CategoryName],[SortOrder],[LegacyCatTypeId])
ON (Target.[TrainingCategoryId] = Source.[TrainingCategoryId])
WHEN MATCHED AND (
	NULLIF(Source.[CategoryName], Target.[CategoryName]) IS NOT NULL OR NULLIF(Target.[CategoryName], Source.[CategoryName]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL OR 
	NULLIF(Source.[LegacyCatTypeId], Target.[LegacyCatTypeId]) IS NOT NULL OR NULLIF(Target.[LegacyCatTypeId], Source.[LegacyCatTypeId]) IS NOT NULL) THEN
 UPDATE SET
  [CategoryName] = Source.[CategoryName], 
  [SortOrder] = Source.[SortOrder], 
  [LegacyCatTypeId] = Source.[LegacyCatTypeId]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([TrainingCategoryId],[CategoryName],[SortOrder],[LegacyCatTypeId])
 VALUES(Source.[TrainingCategoryId],Source.[CategoryName],Source.[SortOrder],Source.[LegacyCatTypeId])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [TrainingCategory]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[TrainingCategory] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [TrainingCategory] OFF
GO
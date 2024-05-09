SET IDENTITY_INSERT [UserInfoChangeType] ON

MERGE INTO [UserInfoChangeType] AS Target
USING (VALUES
  (1,'Last Name',1,NULL,NULL)
 ,(2,'First Name',2,NULL,NULL)
 ,(3,'Suffix',3,NULL,NULL)
 ,(4,'Email',4,NULL,NULL)
 ,(5,'Org Affiliation-Type',5,NULL,NULL)
 ,(6,'Org Affiliation-Name',6,NULL,NULL)
 ,(7,'Org Affiliation-Dept',7,NULL,NULL)
 ,(8,'Org Affiliation-Position',8,NULL,NULL)
 ,(9,'Org Address-State',12,NULL,NULL)
 ,(10,'CV',10,NULL,NULL)
 ,(11,'Degrees',9,NULL,NULL)
 ,(12,'Expertise',11,NULL,NULL)
 ,(13,'Personal Address-State',13,NULL,NULL)
 ,(15,'PrimaryFlag',14,NULL,NULL)
 ,(16,'W9Verified',15,NULL,NULL)
 ,(17,'Major',16,NULL,NULL)
 ,(18,'Degrees N/A',17,NULL,NULL)
) AS Source ([UserInfoChangeTypeId],[Label],[SortOrder],[TableName],[FieldName])
ON (Target.[UserInfoChangeTypeId] = Source.[UserInfoChangeTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[Label], Target.[Label]) IS NOT NULL OR NULLIF(Target.[Label], Source.[Label]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL OR 
	NULLIF(Source.[TableName], Target.[TableName]) IS NOT NULL OR NULLIF(Target.[TableName], Source.[TableName]) IS NOT NULL OR 
	NULLIF(Source.[FieldName], Target.[FieldName]) IS NOT NULL OR NULLIF(Target.[FieldName], Source.[FieldName]) IS NOT NULL) THEN
 UPDATE SET
  [Label] = Source.[Label], 
  [SortOrder] = Source.[SortOrder], 
  [TableName] = Source.[TableName], 
  [FieldName] = Source.[FieldName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([UserInfoChangeTypeId],[Label],[SortOrder],[TableName],[FieldName])
 VALUES(Source.[UserInfoChangeTypeId],Source.[Label],Source.[SortOrder],Source.[TableName],Source.[FieldName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [UserInfoChangeType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[UserInfoChangeType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [UserInfoChangeType] OFF
GO
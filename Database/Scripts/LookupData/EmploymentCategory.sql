SET IDENTITY_INSERT [EmploymentCategory] ON

MERGE INTO [EmploymentCategory] AS Target
USING (VALUES
  (1,'Paid')
 ,(2,'Unpaid')
 ,(3,'Unpaid w/t')
) AS Source ([EmploymentCategoryId],[Name])
ON (Target.[EmploymentCategoryId] = Source.[EmploymentCategoryId])
WHEN MATCHED AND 
	NULLIF(Source.[Name], Target.[Name]) IS NOT NULL OR NULLIF(Target.[Name], Source.[Name]) IS NOT NULL THEN
 UPDATE SET
  [Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([EmploymentCategoryId],[Name])
 VALUES(Source.[EmploymentCategoryId],Source.[Name])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [EmploymentCategory]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[EmploymentCategory] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [EmploymentCategory] OFF
GO
SET IDENTITY_INSERT [MechanismRelationshipType] ON

MERGE INTO [MechanismRelationshipType] AS Target
USING (VALUES
  (1,'Pre-Application')
) AS Source ([MechanismRelationshipTypeId],[RelationshipType])
ON (Target.[MechanismRelationshipTypeId] = Source.[MechanismRelationshipTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[RelationshipType], Target.[RelationshipType]) IS NOT NULL OR NULLIF(Target.[RelationshipType], Source.[RelationshipType]) IS NOT NULL) THEN
 UPDATE SET
  [RelationshipType] = Source.[RelationshipType]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([MechanismRelationshipTypeId],[RelationshipType])
 VALUES(Source.[MechanismRelationshipTypeId],Source.[RelationshipType])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [MechanismRelationshipType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[MechanismRelationshipType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [MechanismRelationshipType] OFF
GO

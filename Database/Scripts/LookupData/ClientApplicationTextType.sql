SET IDENTITY_INSERT [ClientApplicationTextType] ON

MERGE INTO [ClientApplicationTextType] AS Target
USING (VALUES
  (1,10,'Public Abstract')
 ,(2,10,'Technical Abstract')
 ,(3,21,'Technical Abstract')
 ,(4,2,'Public Abstract')
 ,(5,19,'Statement of Work')
 ,(6,19,'Public Abstract')
 ,(7,19,'Technical Abstract')
 ,(8,19,'Impact Statement')
 ,(9,9,'Significance')
 ,(10,9,'Layperson''s Summary')
 ,(11,19,'Scientific Abstract')
 ,(12,9,'Technical Abstract')
 ,(13,17,'Technical Abstract')
 ,(14,17,'Public Abstract')
 ,(15,11,'Scientific Abstract')
 ,(16,11,'Public Abstract')
 ,(17,11,'Research Progress Plan and Outcomes')
 ,(18,11,'Impact Statement')
 ,(19,9,'Abstract and Significance')
 ,(20,9,'Summary')
 ,(21,21,'Public Abstract')
 ,(22,23,'Statement of Work')
 ,(23,23,'Public Abstract')
 ,(24,23,'Technical Abstract')
 ,(25,23,'Impact Statement')
 ,(26,23,'Scientific Abstract')
) AS Source ([ClientApplicationTextTypeId],[ClientId],[TextType])
ON (Target.[ClientApplicationTextTypeId] = Source.[ClientApplicationTextTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientId], Target.[ClientId]) IS NOT NULL OR NULLIF(Target.[ClientId], Source.[ClientId]) IS NOT NULL OR 
	NULLIF(Source.[TextType], Target.[TextType]) IS NOT NULL OR NULLIF(Target.[TextType], Source.[TextType]) IS NOT NULL) THEN
 UPDATE SET
  [ClientId] = Source.[ClientId], 
  [TextType] = Source.[TextType]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientApplicationTextTypeId],[ClientId],[TextType])
 VALUES(Source.[ClientApplicationTextTypeId],Source.[ClientId],Source.[TextType])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientApplicationTextType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientApplicationTextType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientApplicationTextType] OFF
GO
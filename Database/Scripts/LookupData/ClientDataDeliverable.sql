SET IDENTITY_INSERT [ClientDataDeliverable] ON

MERGE INTO [ClientDataDeliverable] AS Target
USING (VALUES
  (3,19,'Scoring','Deliverable of average scores by application.','GetScoringDeliverable','xml',1,3)
 ,(4,19,'Budget','Deliverable of budget comments and recommended revisions.','GetBudgetDeliverable','xml',1,4)
 ,(5,19,'Criteria','Deliverable of mechanism criteria.','GetCriteriaDeliverable','xml',1,2)
 ,(6,19,'Panel','Deliverable of panel of applications.','GetPanelDeliverable','xml',1,1)
) AS Source ([ClientDataDeliverableId],[ClientId],[Label],[Description],[ApiMethod],[FileFormat],[QcRequiredFlag],[SortOrder])
ON (Target.[ClientDataDeliverableId] = Source.[ClientDataDeliverableId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientId], Target.[ClientId]) IS NOT NULL OR NULLIF(Target.[ClientId], Source.[ClientId]) IS NOT NULL OR 
	NULLIF(Source.[Label], Target.[Label]) IS NOT NULL OR NULLIF(Target.[Label], Source.[Label]) IS NOT NULL OR 
	NULLIF(Source.[Description], Target.[Description]) IS NOT NULL OR NULLIF(Target.[Description], Source.[Description]) IS NOT NULL OR 
	NULLIF(Source.[ApiMethod], Target.[ApiMethod]) IS NOT NULL OR NULLIF(Target.[ApiMethod], Source.[ApiMethod]) IS NOT NULL OR 
	NULLIF(Source.[FileFormat], Target.[FileFormat]) IS NOT NULL OR NULLIF(Target.[FileFormat], Source.[FileFormat]) IS NOT NULL OR 
	NULLIF(Source.[QcRequiredFlag], Target.[QcRequiredFlag]) IS NOT NULL OR NULLIF(Target.[QcRequiredFlag], Source.[QcRequiredFlag]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [ClientId] = Source.[ClientId], 
 [Label] = Source.[Label], 
 [Description] = Source.[Description], 
 [ApiMethod] = Source.[ApiMethod], 
 [FileFormat] = Source.[FileFormat], 
 [QcRequiredFlag] = Source.[QcRequiredFlag],
 [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientDataDeliverableId],[ClientId],[Label],[Description],[ApiMethod],[FileFormat],[QcRequiredFlag],[SortOrder])
 VALUES(Source.[ClientDataDeliverableId],Source.[ClientId],Source.[Label],Source.[Description],Source.[ApiMethod],Source.[FileFormat],Source.[QcRequiredFlag],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN
 DELETE;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientDataDeliverable]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientDataDeliverable] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientDataDeliverable] OFF
GO
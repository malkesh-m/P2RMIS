SET IDENTITY_INSERT [ClientAssignmentType] ON

MERGE INTO [ClientAssignmentType] AS Target
USING (VALUES
  (1,19,5,'SR','Scientist Reviewer')
 ,(2,19,6,'CR','Consumer Reviewer')
 ,(3,19,8,'COI','Conflict of Interest')
 ,(4,19,7,'RR','Reader')
 ,(5,1,5,'SR','Scientist Reviewer')
 ,(6,2,5,'SR','Scientist Reviewer')
 ,(7,3,5,'SR','Scientist Reviewer')
 ,(8,4,5,'SR','Scientist Reviewer')
 ,(9,5,5,'SR','Scientist Reviewer')
 ,(10,6,5,'SR','Scientist Reviewer')
 ,(11,7,5,'SR','Scientist Reviewer')
 ,(12,8,5,'SR','Scientist Reviewer')
 ,(13,9,5,'SR','Scientist Reviewer')
 ,(14,10,5,'SR','Scientist Reviewer')
 ,(15,11,5,'SR','Scientist Reviewer')
 ,(16,12,5,'SR','Scientist Reviewer')
 ,(17,13,5,'SR','Scientist Reviewer')
 ,(18,14,5,'SR','Scientist Reviewer')
 ,(19,15,5,'SR','Scientist Reviewer')
 ,(20,16,5,'SR','Scientist Reviewer')
 ,(21,17,5,'SR','Scientist Reviewer')
 ,(22,18,5,'SR','Scientist Reviewer')
 ,(23,20,5,'SR','Scientist Reviewer')
 ,(24,21,5,'SR','Scientist Reviewer')
 ,(25,22,5,'SR','Scientist Reviewer')
 ,(26,1,6,'CR','Consumer Reviewer')
 ,(27,2,6,'CR','Consumer Reviewer')
 ,(28,3,6,'CR','Consumer Reviewer')
 ,(29,4,6,'CR','Consumer Reviewer')
 ,(30,5,6,'CR','Consumer Reviewer')
 ,(31,6,6,'CR','Consumer Reviewer')
 ,(32,7,6,'CR','Consumer Reviewer')
 ,(33,8,6,'CR','Consumer Reviewer')
 ,(34,9,6,'AR','Advocate Reviewer')
 ,(35,10,6,'CR','Consumer Reviewer')
 ,(36,11,6,'CR','Consumer Reviewer')
 ,(37,12,6,'CR','Consumer Reviewer')
 ,(38,13,6,'CR','Consumer Reviewer')
 ,(39,14,6,'CR','Consumer Reviewer')
 ,(40,15,6,'CR','Consumer Reviewer')
 ,(41,16,6,'CR','Consumer Reviewer')
 ,(42,17,6,'CR','Consumer Reviewer')
 ,(43,18,6,'CR','Consumer Reviewer')
 ,(44,20,6,'CR','Consumer Reviewer')
 ,(45,21,6,'CR','Consumer Reviewer')
 ,(46,22,6,'CR','Consumer Reviewer')
 ,(47,1,8,'COI','Conflict of Interest')
 ,(48,2,8,'COI','Conflict of Interest')
 ,(49,3,8,'COI','Conflict of Interest')
 ,(50,4,8,'COI','Conflict of Interest')
 ,(51,5,8,'COI','Conflict of Interest')
 ,(52,6,8,'COI','Conflict of Interest')
 ,(53,7,8,'COI','Conflict of Interest')
 ,(54,8,8,'COI','Conflict of Interest')
 ,(55,9,8,'COI','Conflict of Interest')
 ,(56,10,8,'COI','Conflict of Interest')
 ,(57,11,8,'COI','Conflict of Interest')
 ,(58,12,8,'COI','Conflict of Interest')
 ,(59,13,8,'COI','Conflict of Interest')
 ,(60,14,8,'COI','Conflict of Interest')
 ,(61,15,8,'COI','Conflict of Interest')
 ,(62,16,8,'COI','Conflict of Interest')
 ,(63,17,8,'COI','Conflict of Interest')
 ,(64,18,8,'COI','Conflict of Interest')
 ,(65,20,8,'COI','Conflict of Interest')
 ,(66,21,8,'COI','Conflict of Interest')
 ,(67,22,8,'COI','Conflict of Interest')
 ,(68,1,7,'RR','Reader')
 ,(69,2,7,'RR','Reader')
 ,(70,3,7,'RR','Reader')
 ,(71,4,7,'RR','Reader')
 ,(72,5,7,'RR','Reader')
 ,(73,6,7,'RR','Reader')
 ,(74,7,7,'RR','Reader')
 ,(75,8,7,'RR','Reader')
 ,(76,9,7,'RR','Reader')
 ,(77,10,7,'RR','Reader')
 ,(78,11,7,'RR','Reader')
 ,(79,12,7,'RR','Reader')
 ,(80,13,7,'RR','Reader')
 ,(81,14,7,'RR','Reader')
 ,(82,15,7,'RR','Reader')
 ,(83,16,7,'RR','Reader')
 ,(84,17,7,'RR','Reader')
 ,(85,18,7,'RR','Reader')
 ,(86,20,7,'RR','Reader')
 ,(87,21,7,'RR','Reader')
 ,(88,22,7,'RR','Reader')
 ,(89,23,5,'SR','Scientist Reviewer')
 ,(90,23,6,'CR','Consumer Reviewer')
 ,(91,23,8,'COI','Conflict of Interest')
 ,(92,23,7,'RR','Reader')
 ,(93,19,9,'SPR','Specialist Reviewer')
 ,(94,23,9,'SPR','Specialist Reviewer')
) AS Source ([ClientAssignmentTypeId],[ClientId],[AssignmentTypeId],[AssignmentAbbreviation],[AssignmentLabel])
ON (Target.[ClientAssignmentTypeId] = Source.[ClientAssignmentTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientId], Target.[ClientId]) IS NOT NULL OR NULLIF(Target.[ClientId], Source.[ClientId]) IS NOT NULL OR 
	NULLIF(Source.[AssignmentTypeId], Target.[AssignmentTypeId]) IS NOT NULL OR NULLIF(Target.[AssignmentTypeId], Source.[AssignmentTypeId]) IS NOT NULL OR 
	NULLIF(Source.[AssignmentAbbreviation], Target.[AssignmentAbbreviation]) IS NOT NULL OR NULLIF(Target.[AssignmentAbbreviation], Source.[AssignmentAbbreviation]) IS NOT NULL OR 
	NULLIF(Source.[AssignmentLabel], Target.[AssignmentLabel]) IS NOT NULL OR NULLIF(Target.[AssignmentLabel], Source.[AssignmentLabel]) IS NOT NULL) THEN
 UPDATE SET
  [ClientId] = Source.[ClientId], 
  [AssignmentTypeId] = Source.[AssignmentTypeId], 
  [AssignmentAbbreviation] = Source.[AssignmentAbbreviation], 
  [AssignmentLabel] = Source.[AssignmentLabel]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientAssignmentTypeId],[ClientId],[AssignmentTypeId],[AssignmentAbbreviation],[AssignmentLabel])
 VALUES(Source.[ClientAssignmentTypeId],Source.[ClientId],Source.[AssignmentTypeId],Source.[AssignmentAbbreviation],Source.[AssignmentLabel])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientAssignmentType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientAssignmentType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientAssignmentType] OFF
GO
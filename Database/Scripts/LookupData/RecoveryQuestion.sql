﻿SET IDENTITY_INSERT [RecoveryQuestion] ON

MERGE INTO [RecoveryQuestion] AS Target
USING (VALUES
  (1,'What is your childhood nickname?')
 ,(2,'In what city did you meet your spouse/significant other?')
 ,(3,'What is the name of your favorite childhood friend?')
 ,(4,'What street did you live on in the third grade?')
 ,(5,'What is your oldest sibling birthday month and year (e.g. January 1900)?')
 ,(6,'What is the middle name of your youngest child?')
 ,(7,'What is your oldest siblings middle name?')
 ,(8,'What school did you attend for sixth grade?')
 ,(9,'What was your childhood phone number including area code (e.g. 000-000-0000)?')
 ,(10,'In what city does your nearest sibling living?')
 ,(11,'What is your youngest brother''s birthday month and year (e.g. January 1900)?')
 ,(12,'What is your maternal grandmother''s maiden name?')
 ,(13,'In what city or town was your first job?')
 ,(14,'What is name of the place where your wedding reception was held?')
 ,(15,'What is the name of college you applied to and did not attend?')
 ,(16,'What was the name of your first pet?')
 ,(17,'What is your mother''s maiden name?')
 ,(18,'What was the make of your first car?')
) AS Source ([RecoveryQuestionId],[QuestionText])
ON (Target.[RecoveryQuestionId] = Source.[RecoveryQuestionId])
WHEN MATCHED AND (
	NULLIF(Source.[QuestionText], Target.[QuestionText]) IS NOT NULL OR NULLIF(Target.[QuestionText], Source.[QuestionText]) IS NOT NULL) THEN
 UPDATE SET
  [QuestionText] = Source.[QuestionText]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([RecoveryQuestionId],[QuestionText])
 VALUES(Source.[RecoveryQuestionId],Source.[QuestionText])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [RecoveryQuestion]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[RecoveryQuestion] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [RecoveryQuestion] OFF
GO
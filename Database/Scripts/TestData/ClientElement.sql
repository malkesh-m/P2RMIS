SET IDENTITY_INSERT [ClientElement] ON

MERGE INTO [ClientElement] AS Target
USING (VALUES
  (1,19,1,'OE','Overall Eval','Overall Evaluation',10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(2,19,2,'OVRVW','Overview','Overview',10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(5,19,1,'IM','Impact','Impact',10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(7,19,3,'IMDN','Impact - DN','Impact - Discussion Notes',10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(8,19,1,'RSF','RSF','Research Strategy and Feasibility',10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(9,19,3,'RSFDN','RSF - DN','Research Strategy and Feasibility- Discussion Notes',10,'2014-07-25T00:00:00',10,'2014-07-25T00:00:00')
 ,(11,19,1,'Impl Plan','Impl Plan','Implementation Plan',10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(15,19,3,'IMPPLANDN','Impl Plan - DN','Implementation Plan - Discussion Notes',10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(16,19,1,'PL','Personnel','Personnel',10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(17,19,3,'PLDN','Personnel - DN','Personnel - Discussion Notes',10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(18,19,1,'EN','Envnmnt','Environment',10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(20,19,3,'ENDN','EN - DN','Environment - Discussion Notes',10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(21,19,1,'BT','Budget','Budget',10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(22,19,3,'BTDN','Budget - DN','Budget - Discussion Notes',10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(23,19,1,'APP','App Pres','Application Presentation',10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
 ,(25,19,3,'APPDN','App Pres - DN','Application Presentation - Discussion Notes',10,'2014-08-01T00:00:00',10,'2014-08-01T00:00:00')
) AS Source ([ClientElementId],[ClientId],[ElementTypeId],[ElementIdentifier],[ElementAbbreviation],[ElementDescription],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
ON (Target.[ClientElementId] = Source.[ClientElementId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientId], Target.[ClientId]) IS NOT NULL OR NULLIF(Target.[ClientId], Source.[ClientId]) IS NOT NULL OR 
	NULLIF(Source.[ElementTypeId], Target.[ElementTypeId]) IS NOT NULL OR NULLIF(Target.[ElementTypeId], Source.[ElementTypeId]) IS NOT NULL OR 
	NULLIF(Source.[ElementIdentifier], Target.[ElementIdentifier]) IS NOT NULL OR NULLIF(Target.[ElementIdentifier], Source.[ElementIdentifier]) IS NOT NULL OR 
	NULLIF(Source.[ElementAbbreviation], Target.[ElementAbbreviation]) IS NOT NULL OR NULLIF(Target.[ElementAbbreviation], Source.[ElementAbbreviation]) IS NOT NULL OR 
	NULLIF(Source.[ElementDescription], Target.[ElementDescription]) IS NOT NULL OR NULLIF(Target.[ElementDescription], Source.[ElementDescription]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL) THEN
 UPDATE SET
  [ClientId] = Source.[ClientId], 
  [ElementTypeId] = Source.[ElementTypeId], 
  [ElementIdentifier] = Source.[ElementIdentifier], 
  [ElementAbbreviation] = Source.[ElementAbbreviation], 
  [ElementDescription] = Source.[ElementDescription], 
  [CreatedBy] = Source.[CreatedBy], 
  [CreatedDate] = Source.[CreatedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientElementId],[ClientId],[ElementTypeId],[ElementIdentifier],[ElementAbbreviation],[ElementDescription],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
 VALUES(Source.[ClientElementId],Source.[ClientId],Source.[ElementTypeId],Source.[ElementIdentifier],Source.[ElementAbbreviation],Source.[ElementDescription],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate])
;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientElement]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientElement] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientElement] OFF
GO
SET IDENTITY_INSERT [ClientRegistrationDocumentItem] ON

MERGE INTO [ClientRegistrationDocumentItem] AS Target
USING (VALUES
  (1,1,8,1,'Please indicate your eligibility to receive a consultant fee.')
 ,(2,1,9,0,NULL)
 ,(3,6,8,1,'Please indicate your eligibility to receive a consultant fee.')
 ,(4,6,9,0,NULL)
 ,(5,4,1,0,NULL)
 ,(6,4,2,0,NULL)
 ,(7,4,3,0,'Please scroll down to the bottom of the page and indicate if you have additional disclosures to report.')
 ,(8,4,4,0,NULL)
 ,(9,7,1,0,NULL)
 ,(10,7,2,0,NULL)
 ,(11,7,3,1,'Please scroll down to the bottom of the page and indicate if you have additional disclosures to report.')
 ,(12,7,4,0,NULL)
 ,(14,5,10,0,NULL)
 ,(15,8,10,0,NULL)
 ,(16,9,8,1,'Please indicate your eligibility to receive a consultant fee.')
 ,(17,9,9,0,NULL)
 ,(18,10,1,0,NULL)
 ,(19,10,2,0,NULL)
 ,(20,10,3,1,'Please scroll down to the bottom of the page and indicate if you have additional disclosures to report.')
 ,(21,10,4,0,NULL)
 ,(22,11,10,0,NULL)
 ,(23,12,8,1,'Please indicate your eligibility to receive a consultant fee.')
 ,(24,12,9,0,NULL)
 ,(25,13,1,0,NULL)
 ,(26,13,2,0,NULL)
 ,(27,13,3,1,'Please scroll down to the bottom of the page and indicate if you have additional disclosures to report.')
 ,(28,13,4,0,NULL)
 ,(29,14,10,0,NULL)
) AS Source ([ClientRegistrationDocumentItemId],[ClientRegistrationDocumentId],[RegistrationDocumentItemId],[RequiredFlag],[RequiredMessage])
ON (Target.[ClientRegistrationDocumentItemId] = Source.[ClientRegistrationDocumentItemId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientRegistrationDocumentId], Target.[ClientRegistrationDocumentId]) IS NOT NULL OR NULLIF(Target.[ClientRegistrationDocumentId], Source.[ClientRegistrationDocumentId]) IS NOT NULL OR 
	NULLIF(Source.[RegistrationDocumentItemId], Target.[RegistrationDocumentItemId]) IS NOT NULL OR NULLIF(Target.[RegistrationDocumentItemId], Source.[RegistrationDocumentItemId]) IS NOT NULL OR 
	NULLIF(Source.[RequiredFlag], Target.[RequiredFlag]) IS NOT NULL OR NULLIF(Target.[RequiredFlag], Source.[RequiredFlag]) IS NOT NULL OR 
	NULLIF(Source.[RequiredMessage], Target.[RequiredMessage]) IS NOT NULL OR NULLIF(Target.[RequiredMessage], Source.[RequiredMessage]) IS NOT NULL) THEN
 UPDATE SET
  [ClientRegistrationDocumentId] = Source.[ClientRegistrationDocumentId], 
  [RegistrationDocumentItemId] = Source.[RegistrationDocumentItemId], 
  [RequiredFlag] = Source.[RequiredFlag], 
  [RequiredMessage] = Source.[RequiredMessage]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientRegistrationDocumentItemId],[ClientRegistrationDocumentId],[RegistrationDocumentItemId],[RequiredFlag],[RequiredMessage])
 VALUES(Source.[ClientRegistrationDocumentItemId],Source.[ClientRegistrationDocumentId],Source.[RegistrationDocumentItemId],Source.[RequiredFlag],Source.[RequiredMessage])
;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientRegistrationDocumentItem]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientRegistrationDocumentItem] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientRegistrationDocumentItem] OFF
GO
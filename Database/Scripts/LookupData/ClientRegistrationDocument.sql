SET IDENTITY_INSERT [ClientRegistrationDocument] ON

MERGE INTO [ClientRegistrationDocument] AS Target
USING (VALUES
  (1,1,1,'Acknowledge/NDA','AckNDA',1,'AcknowledgementCprit',1,0,'2015-09-17T00:00:00','I hereby confirm that I have reviewed and agree to the Acknowledge/NDA herein. By entering my password below, I understand that this creates a binding agreement with SRA International, Inc., a GDIT Company',NULL)
 ,(4,1,2,'Bias/COI','BiasCOI',2,'BiasCoiCprit',0,0,'2015-09-17T00:00:00','I hereby confirm that I have reviewed and reported all my BIAS/COI herein. By entering my password below, I understand that this creates a binding agreement with SRA International, Inc., a GDIT Company, that I will report any and all conflicts of interest that I have with respect to applications submitted to my assigned committee for review.',NULL)
 ,(5,1,3,'Contractual Agreement','Contract',3,'ContractCprit',0,4,'2015-09-17T00:00:00','I hereby confirm that I have reviewed and agree to the terms and conditions outlined in the contractual agreement herein.  By entering my password below, I understand that this creates a binding agreement with SRA International, Inc., a GDIT Company','CPRIT_Contract')
 ,(6,2,1,'Eligibility','AckNDA',1,'Acknowledgement',1,2,'2015-09-17T00:00:00','I hereby confirm that I have reviewed and agree to the Eligibility herein. By entering my password below, I understand that this creates a binding agreement with SRA International, Inc., a GDIT Company',NULL)
 ,(7,2,2,'Confidentiality/COI','BiasCOI',2,'BiasCoi',1,1,'2015-09-17T00:00:00','I hereby confirm that I have reviewed and reported all my Confidentiality/COI herein. By entering my password below, I understand that this creates a binding agreement with SRA International, Inc., a GDIT Company, that I will report any and all conflicts of interest that I have with respect to applications submitted to my assigned committee for review.',NULL)
 ,(8,2,3,'Contractual Agreement','Contract',3,'Contract',0,3,'2015-09-17T00:00:00','I hereby confirm that I have reviewed and agree to the terms and conditions outlined in the contractual agreement herein.  By entering my password below, I understand that this creates a binding agreement with SRA International, Inc., a GDIT Company','CDMRP_Contract')
 ,(9,3,1,'Eligibility','AckNDA',1,'Acknowledgement',1,2,'2015-09-17T00:00:00','I hereby confirm that I have reviewed and agree to the Eligibility herein. By entering my password below, I understand that this creates a binding agreement with SRA International, Inc., a GDIT Company',NULL)
 ,(10,3,2,'Confidentiality/COI','BiasCOI',2,'BiasCoi',1,1,'2015-09-17T00:00:00','I hereby confirm that I have reviewed and reported all my Confidentiality/COI herein. By entering my password below, I understand that this creates a binding agreement with SRA International, Inc., a GDIT Company, that I will report any and all conflicts of interest that I have with respect to applications submitted to my assigned committee for review.',NULL)
 ,(11,3,3,'Contractual Agreement','Contract',3,'Contract',0,3,'2015-09-17T00:00:00','I hereby confirm that I have reviewed and agree to the terms and conditions outlined in the contractual agreement herein.  By entering my password below, I understand that this creates a binding agreement with SRA International, Inc., a GDIT Company','CDMRP_Contract')
 ,(12,4,1,'Eligibility','AckNDA',1,'Acknowledgement',1,2,'2015-09-17T00:00:00','I hereby confirm that I have reviewed and agree to the Eligibility herein. By entering my password below, I understand that this creates a binding agreement with SRA International, Inc., a GDIT Company',NULL)
 ,(13,4,2,'Confidentiality/COI','BiasCOI',2,'BiasCoi',1,1,'2015-09-17T00:00:00','I hereby confirm that I have reviewed and reported all my Confidentiality/COI herein. By entering my password below, I understand that this creates a binding agreement with SRA International, Inc., a GDIT Company, that I will report any and all conflicts of interest that I have with respect to applications submitted to my assigned committee for review.',NULL)
 ,(14,4,3,'Contractual Agreement','Contract',3,'Contract',0,3,'2015-09-17T00:00:00','I hereby confirm that I have reviewed and agree to the terms and conditions outlined in the contractual agreement herein.  By entering my password below, I understand that this creates a binding agreement with SRA International, Inc., a GDIT Company','CDMRP_Contract')
 ) AS Source ([ClientRegistrationDocumentId],[ClientRegistrationId],[RegistrationDocumentTypeId],[DocumentName],[DocumentAbbreviation],[SortOrder],[DocumentRoute],[RequiredFlag],[DocumentVersion],[DocumentUpdateDate],[ConfirmationText],[ReportFileName])
ON (Target.[ClientRegistrationDocumentId] = Source.[ClientRegistrationDocumentId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientRegistrationId], Target.[ClientRegistrationId]) IS NOT NULL OR NULLIF(Target.[ClientRegistrationId], Source.[ClientRegistrationId]) IS NOT NULL OR 
	NULLIF(Source.[RegistrationDocumentTypeId], Target.[RegistrationDocumentTypeId]) IS NOT NULL OR NULLIF(Target.[RegistrationDocumentTypeId], Source.[RegistrationDocumentTypeId]) IS NOT NULL OR 
	NULLIF(Source.[DocumentName], Target.[DocumentName]) IS NOT NULL OR NULLIF(Target.[DocumentName], Source.[DocumentName]) IS NOT NULL OR 
	NULLIF(Source.[DocumentAbbreviation], Target.[DocumentAbbreviation]) IS NOT NULL OR NULLIF(Target.[DocumentAbbreviation], Source.[DocumentAbbreviation]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL OR 
	NULLIF(Source.[DocumentRoute], Target.[DocumentRoute]) IS NOT NULL OR NULLIF(Target.[DocumentRoute], Source.[DocumentRoute]) IS NOT NULL OR 
	NULLIF(Source.[RequiredFlag], Target.[RequiredFlag]) IS NOT NULL OR NULLIF(Target.[RequiredFlag], Source.[RequiredFlag]) IS NOT NULL OR 
	NULLIF(Source.[DocumentVersion], Target.[DocumentVersion]) IS NOT NULL OR NULLIF(Target.[DocumentVersion], Source.[DocumentVersion]) IS NOT NULL OR 
	NULLIF(Source.[DocumentUpdateDate], Target.[DocumentUpdateDate]) IS NOT NULL OR NULLIF(Target.[DocumentUpdateDate], Source.[DocumentUpdateDate]) IS NOT NULL OR 
	NULLIF(Source.[ConfirmationText], Target.[ConfirmationText]) IS NOT NULL OR NULLIF(Target.[ConfirmationText], Source.[ConfirmationText]) IS NOT NULL OR 
	NULLIF(Source.[ReportFileName], Target.[ReportFileName]) IS NOT NULL OR NULLIF(Target.[ReportFileName], Source.[ReportFileName]) IS NOT NULL) THEN
 UPDATE SET
  [ClientRegistrationId] = Source.[ClientRegistrationId], 
  [RegistrationDocumentTypeId] = Source.[RegistrationDocumentTypeId], 
  [DocumentName] = Source.[DocumentName], 
  [DocumentAbbreviation] = Source.[DocumentAbbreviation], 
  [SortOrder] = Source.[SortOrder], 
  [DocumentRoute] = Source.[DocumentRoute], 
  [RequiredFlag] = Source.[RequiredFlag], 
  [DocumentVersion] = Source.[DocumentVersion], 
  [DocumentUpdateDate] = Source.[DocumentUpdateDate], 
  [ConfirmationText] = Source.[ConfirmationText], 
  [ReportFileName] = Source.[ReportFileName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientRegistrationDocumentId],[ClientRegistrationId],[RegistrationDocumentTypeId],[DocumentName],[DocumentAbbreviation],[SortOrder],[DocumentRoute],[RequiredFlag],[DocumentVersion],[DocumentUpdateDate],[ConfirmationText],[ReportFileName])
 VALUES(Source.[ClientRegistrationDocumentId],Source.[ClientRegistrationId],Source.[RegistrationDocumentTypeId],Source.[DocumentName],Source.[DocumentAbbreviation],Source.[SortOrder],Source.[DocumentRoute],Source.[RequiredFlag],Source.[DocumentVersion],Source.[DocumentUpdateDate],Source.[ConfirmationText],Source.[ReportFileName])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientRegistrationDocument]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientRegistrationDocument] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientRegistrationDocument] OFF
GO
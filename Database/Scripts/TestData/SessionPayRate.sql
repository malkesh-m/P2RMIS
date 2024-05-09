SET IDENTITY_INSERT [SessionPayRate] ON

MERGE INTO [SessionPayRate] AS Target
USING (VALUES
  (1,783,109,1,'Paid','Consultant Fee of $1,250.00, plus authorized expenses',250.0000,'2015-07-22T00:00:00','2015-09-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0,NULL,NULL)
 ,(2,783,22,1,'Paid','Consultant Fee of $1,250.00, plus authorized expenses',250.0000,'2015-07-22T00:00:00','2015-09-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0,NULL,NULL)
 ,(3,783,49,1,'Paid','Consultant Fee of $1,250.00, plus authorized expenses',250.0000,'2015-07-22T00:00:00','2015-09-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0,NULL,NULL)
 ,(4,783,6,1,'Paid','Consultant Fee of $50.00, plus authorized expenses',750.0000,'2015-07-22T00:00:00','2015-09-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0,NULL,NULL)
 ,(5,783,109,2,'Unaid','None',0.0000,'2015-07-22T00:00:00','2015-09-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0,NULL,NULL)
 ,(6,783,22,2,'Unaid','None',0.0000,'2015-07-22T00:00:00','2015-09-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0,NULL,NULL)
 ,(7,783,49,2,'Unaid','None',0.0000,'2015-07-22T00:00:00','2015-09-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0,NULL,NULL)
 ,(8,783,6,2,'Unaid','None',0.0000,'2015-07-22T00:00:00','2015-09-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0,NULL,NULL)
) AS Source ([SessionPayRateId],[MeetingSessionId],[ClientParticipantTypeId],[EmploymentCategoryId],[HonorariumAccepted],[ConsultantFeeText],[ConsultantFee],[PeriodStartDate],[PeriodEndDate],[ManagerList],[DescriptionOfWork],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate],[DeletedFlag],[DeletedBy],[DeletedDate])
ON (Target.[SessionPayRateId] = Source.[SessionPayRateId])
WHEN MATCHED AND (
	NULLIF(Source.[MeetingSessionId], Target.[MeetingSessionId]) IS NOT NULL OR NULLIF(Target.[MeetingSessionId], Source.[MeetingSessionId]) IS NOT NULL OR 
	NULLIF(Source.[ClientParticipantTypeId], Target.[ClientParticipantTypeId]) IS NOT NULL OR NULLIF(Target.[ClientParticipantTypeId], Source.[ClientParticipantTypeId]) IS NOT NULL OR 
	NULLIF(Source.[EmploymentCategoryId], Target.[EmploymentCategoryId]) IS NOT NULL OR NULLIF(Target.[EmploymentCategoryId], Source.[EmploymentCategoryId]) IS NOT NULL OR 
	NULLIF(Source.[HonorariumAccepted], Target.[HonorariumAccepted]) IS NOT NULL OR NULLIF(Target.[HonorariumAccepted], Source.[HonorariumAccepted]) IS NOT NULL OR 
	NULLIF(Source.[ConsultantFeeText], Target.[ConsultantFeeText]) IS NOT NULL OR NULLIF(Target.[ConsultantFeeText], Source.[ConsultantFeeText]) IS NOT NULL OR 
	NULLIF(Source.[ConsultantFee], Target.[ConsultantFee]) IS NOT NULL OR NULLIF(Target.[ConsultantFee], Source.[ConsultantFee]) IS NOT NULL OR 
	NULLIF(Source.[PeriodStartDate], Target.[PeriodStartDate]) IS NOT NULL OR NULLIF(Target.[PeriodStartDate], Source.[PeriodStartDate]) IS NOT NULL OR 
	NULLIF(Source.[PeriodEndDate], Target.[PeriodEndDate]) IS NOT NULL OR NULLIF(Target.[PeriodEndDate], Source.[PeriodEndDate]) IS NOT NULL OR 
	NULLIF(Source.[ManagerList], Target.[ManagerList]) IS NOT NULL OR NULLIF(Target.[ManagerList], Source.[ManagerList]) IS NOT NULL OR 
	NULLIF(Source.[DescriptionOfWork], Target.[DescriptionOfWork]) IS NOT NULL OR NULLIF(Target.[DescriptionOfWork], Source.[DescriptionOfWork]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL OR 
	NULLIF(Source.[DeletedFlag], Target.[DeletedFlag]) IS NOT NULL OR NULLIF(Target.[DeletedFlag], Source.[DeletedFlag]) IS NOT NULL OR 
	NULLIF(Source.[DeletedBy], Target.[DeletedBy]) IS NOT NULL OR NULLIF(Target.[DeletedBy], Source.[DeletedBy]) IS NOT NULL OR 
	NULLIF(Source.[DeletedDate], Target.[DeletedDate]) IS NOT NULL OR NULLIF(Target.[DeletedDate], Source.[DeletedDate]) IS NOT NULL) THEN
 UPDATE SET
  [MeetingSessionId] = Source.[MeetingSessionId], 
  [ClientParticipantTypeId] = Source.[ClientParticipantTypeId], 
  [EmploymentCategoryId] = source.[EmploymentCategoryId],
  [HonorariumAccepted] = Source.[HonorariumAccepted], 
  [ConsultantFeeText] = Source.[ConsultantFeeText], 
  [ConsultantFee] = Source.[ConsultantFee], 
  [PeriodStartDate] = Source.[PeriodStartDate], 
  [PeriodEndDate] = Source.[PeriodEndDate], 
  [ManagerList] = Source.[ManagerList], 
  [DescriptionOfWork] = Source.[DescriptionOfWork], 
  [CreatedBy] = Source.[CreatedBy], 
  [CreatedDate] = Source.[CreatedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [ModifiedDate] = Source.[ModifiedDate], 
  [DeletedFlag] = Source.[DeletedFlag], 
  [DeletedBy] = Source.[DeletedBy], 
  [DeletedDate] = Source.[DeletedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([SessionPayRateId],[MeetingSessionId],[ClientParticipantTypeId],[EmploymentCategoryId],[HonorariumAccepted],[ConsultantFeeText],[ConsultantFee],[PeriodStartDate],[PeriodEndDate],[ManagerList],[DescriptionOfWork],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate],[DeletedFlag],[DeletedBy],[DeletedDate])
 VALUES(Source.[SessionPayRateId],Source.[MeetingSessionId],Source.[ClientParticipantTypeId],source.[EmploymentCategoryId],Source.[HonorariumAccepted],Source.[ConsultantFeeText],Source.[ConsultantFee],Source.[PeriodStartDate],Source.[PeriodEndDate],Source.[ManagerList],Source.[DescriptionOfWork],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate],Source.[DeletedFlag],Source.[DeletedBy],Source.[DeletedDate])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [SessionPayRate]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[SessionPayRate] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [SessionPayRate] OFF
GO
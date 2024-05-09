SET IDENTITY_INSERT [ClientRole] ON

MERGE INTO [ClientRole] AS Target
USING (VALUES
  ('Advocate Reviewer','Advocate')
 ,('Basic Scientist', 'Basic Scientist')
 ,('Bioethicist','Bioethicist')
 ,('Biostatistician','Biostatistician')
 ,('Business/Finance/Management Specialist','Business/Finance/Mgmt')
 ,('Clinical Investigator','Clinical Investigator')
 ,('Clinical Trials Manager','Clinical Trials Manager')
 ,('Clinical Trials Specialist','Clinical Trials Specialist')
 ,('Clinician','Clinician')
 ,('Consortium Development Specialist','Consortium Developmt')
 ,('Consumer','Consumer')
 ,('Consumer Reviewer-Experienced','CR-Experienced')
 ,('Consumer Reviewer-Mentor','CR-Mentor')
 ,('Consumer Reviewer-Novice','CR-Novice')
 ,('Data Management Specialist','Data Mgmt')
 ,('Educator/Academician','Educator/Academician')
 ,('FDA/ICH Specialist','FDA/ICH Specialist')
 ,('Laboratory Investigator','Laboratory Investigator')
 ,('Military Relevance Specialist','Military Relevance')
 ,('Person invited to participate in the NPAC meetings','NPAC Invitee')
 ,('Product Development Specialist','Product Developmt')
 ,('Public Health/Education Researcher','Public Health/Educ.')
 ,('QA/QC Specialist','QA/QC Specialist')
 ,('Radiation Oncologist/Radiologist','Radiation Oncologist')
 ,('Radiologist','Radiologist')
 ,('Regulatory Compliance Specialist','Regulatory Compliance')
 ,('Scientist','Scientist')
 ,('Statistician','Statistician')
 ,('Surgeon','Surgeon')
 ,('Technology Transfer Specialist','Tech Transfer')
 ,('Translational Investigator','Translational Investigator')
) AS Source ([RoleName],[RoleAbbreviation])
ON (Target.[RoleName] = Source.[RoleName])
WHEN MATCHED AND (
	NULLIF(Source.[RoleAbbreviation], Target.[RoleAbbreviation]) IS NOT NULL ) THEN
 UPDATE SET
  Target.[RoleAbbreviation] = Source.[RoleAbbreviation]
;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientRole]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientRole] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientRole] OFF
GO

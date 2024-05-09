﻿SET IDENTITY_INSERT [ClientApplicationPersonnelType] ON

MERGE INTO [ClientApplicationPersonnelType] AS Target
USING (VALUES
  (78,9,'Alternate Submitter',NULL,1)
 ,(79,9,'Applicant',NULL,1)
 ,(80,9,'Authorized Representative',NULL,1)
 ,(81,9,'COI',NULL,1)
 ,(82,9,'COI - Conflict of Interest',NULL,1)
 ,(83,9,'co-investigator',NULL,1)
 ,(84,9,'collaborator',NULL,1)
 ,(85,9,'consultant (paid or unpaid)',NULL,1)
 ,(86,9,'Contract Representative 3',NULL,1)
 ,(87,9,'co-PI',NULL,1)
 ,(88,9,'Education/Recruitment',NULL,1)
 ,(89,9,'Evaluation',NULL,1)
 ,(90,9,'graduate student',NULL,1)
 ,(91,9,'mentor',NULL,1)
 ,(92,9,'mentor (secondary)',NULL,1)
 ,(93,9,'mentor (tertiary)',NULL,1)
 ,(94,9,'other',NULL,1)
 ,(95,9,'other role on project',NULL,1)
 ,(96,9,'Partnering PI',NULL,1)
 ,(97,9,'Project Lead',NULL,1)
 ,(98,9,'Provision of Clinical Services',NULL,1)
 ,(99,9,'Senior Management',NULL,1)
 ,(100,9,'Staff',NULL,1)
 ,(101,9,'Trainee (pre- or postdoctoral)',NULL,1)
 ,(102,10,'COI',NULL,1)
 ,(103,10,'co-investigator',NULL,1)
 ,(104,10,'collaborator',NULL,1)
 ,(105,10,'other',NULL,1)
 ,(106,11,'COI',NULL,1)
 ,(107,11,'co-investigator',NULL,1)
 ,(108,11,'collaborator',NULL,1)
 ,(109,11,'consultant (paid or unpaid)',NULL,1)
 ,(110,11,'co-PI',NULL,1)
 ,(111,11,'graduate student',NULL,1)
 ,(112,11,'mentor',NULL,1)
 ,(113,11,'mentor (secondary)',NULL,1)
 ,(114,11,'mentor (tertiary)',NULL,1)
 ,(115,11,'other',NULL,1)
 ,(116,11,'Partnering PI',NULL,1)
 ,(117,17,'Applicant',NULL,1)
 ,(118,17,'co-investigator',NULL,1)
 ,(119,17,'collaborator',NULL,1)
 ,(120,17,'consultant (paid or unpaid)',NULL,1)
 ,(121,17,'co-PI',NULL,1)
 ,(122,17,'mentor',NULL,1)
 ,(123,17,'other',NULL,1)
 ,(124,17,'post-doctoral fellow',NULL,1)
 ,(125,17,'Staff',NULL,1)
 ,(126,19,'Alternate Submitter',NULL,1)
 ,(127,19,'Applicant',NULL,1)
 ,(128,19,'Authorized Representative',NULL,1)
 ,(129,19,'COI',NULL,1)
 ,(130,19,'COI - Conflict of Interest',NULL,1)
 ,(131,19,'co-investigator',NULL,1)
 ,(132,19,'collaborator',NULL,1)
 ,(133,19,'consultant (paid or unpaid)',NULL,1)
 ,(134,19,'Contract Representative 1',NULL,1)
 ,(135,19,'Contract Representative 2',NULL,1)
 ,(136,19,'Contract Representative 3',NULL,1)
 ,(137,19,'co-PI',NULL,1)
 ,(138,19,'graduate student',NULL,1)
 ,(139,19,'mentor',NULL,1)
 ,(140,19,'mentor (secondary)',NULL,1)
 ,(141,19,'mentor (tertiary)',NULL,1)
 ,(142,19,'other',NULL,1)
 ,(143,19,'other role on project',NULL,1)
 ,(144,19,'Partnering PI',NULL,1)
 ,(145,19,'post-doctoral fellow',NULL,1)
 ,(146,20,'Applicant',NULL,1)
 ,(147,20,'co-investigator',NULL,1)
 ,(148,20,'consultant (paid or unpaid)',NULL,1)
 ,(149,20,'co-PI',NULL,1)
 ,(150,20,'other',NULL,1)
 ,(168,1,'Principal Investigator','PI',0)
 ,(169,2,'Principal Investigator','PI',0)
 ,(170,3,'Principal Investigator','PI',0)
 ,(171,4,'Principal Investigator','PI',0)
 ,(172,5,'Principal Investigator','PI',0)
 ,(173,6,'Principal Investigator','PI',0)
 ,(174,7,'Principal Investigator','PI',0)
 ,(175,8,'Principal Investigator','PI',0)
 ,(176,12,'Principal Investigator','PI',0)
 ,(177,13,'Principal Investigator','PI',0)
 ,(178,14,'Principal Investigator','PI',0)
 ,(179,15,'Principal Investigator','PI',0)
 ,(180,16,'Principal Investigator','PI',0)
 ,(181,18,'Principal Investigator','PI',0)
 ,(182,21,'Principal Investigator','PI',0)
 ,(183,22,'Principal Investigator','PI',0)
 ,(184,19,'Admin-1','Admin-1',0)
 ,(185,20,'Admin-1','Admin-1',0)
 ,(186,9,'Admin-1','Admin-1',0)
 ,(187,19,'Admin-2','Admin-2',0)
 ,(188,20,'Admin-2','Admin-2',0)
 ,(189,9,'Admin-2','Admin-2',0)
 ,(190,19,'Admin-3','Admin-3',0)
 ,(191,20,'Admin-3','Admin-3',0)
 ,(192,9,'Admin-3','Admin-3',0)
 ,(193,19,'Mentor','Mentor',0)
 ,(199,10,'Principal Investigator','PI',0)
 ,(200,11,'Principal Investigator','PI',0)
 ,(203,19,'Principal Investigator','PI',0)
 ,(202,20,'Principal Investigator','PI',0)
 ,(204,9,'Principal Investigator','PI',0)
 ,(205,17,'Principal Investigator','PI',0)
  ,(206,10,'Principal Investigator 2','PI2',0)
 ,(207,11,'Principal Investigator 2','PI2',0)
 ,(208,19,'Principal Investigator 2','PI2',0)
 ,(209,20,'Principal Investigator 2','PI2',0)
 ,(210,9,'Principal Investigator 2','PI2',0)
 ,(211,17,'Principal Investigator 2','PI2',0)
  ,(218,23,'Alternate Submitter',NULL,1)
 ,(219,23,'Applicant',NULL,1)
 ,(220,23,'Authorized Representative',NULL,1)
 ,(221,23,'COI',NULL,1)
 ,(222,23,'COI - Conflict of Interest',NULL,1)
 ,(223,23,'co-investigator',NULL,1)
 ,(224,23,'collaborator',NULL,1)
 ,(225,23,'consultant (paid or unpaid)',NULL,1)
 ,(226,23,'Contract Representative 1',NULL,1)
 ,(227,23,'Contract Representative 2',NULL,1)
 ,(228,23,'Contract Representative 3',NULL,1)
 ,(229,23,'co-PI',NULL,1)
 ,(230,23,'graduate student',NULL,1)
 ,(231,23,'mentor',NULL,1)
 ,(232,23,'mentor (secondary)',NULL,1)
 ,(233,23,'mentor (tertiary)',NULL,1)
 ,(234,23,'other',NULL,1)
 ,(235,23,'other role on project',NULL,1)
 ,(236,23,'Partnering PI',NULL,1)
 ,(237,23,'post-doctoral fellow',NULL,1)
 ,(238,23,'Admin-1','Admin-1',0)
 ,(239,23,'Admin-2','Admin-2',0)
 ,(240,23,'Admin-3','Admin-3',0)
 ,(241,23,'Mentor','Mentor',0)
 ,(242,23,'Principal Investigator','PI',0)
 ,(243,23,'Principal Investigator 2','PI2',0)

) AS Source ([ClientApplicationPersonnelTypeId],[ClientId],[ApplicationPersonnelType],[ApplicationPersonnelTypeAbbreviation],[CoiFlag])
ON (Target.[ClientApplicationPersonnelTypeId] = Source.[ClientApplicationPersonnelTypeId])
WHEN MATCHED AND (
	NULLIF(Source.[ClientId], Target.[ClientId]) IS NOT NULL OR NULLIF(Target.[ClientId], Source.[ClientId]) IS NOT NULL OR 
	NULLIF(Source.[ApplicationPersonnelType], Target.[ApplicationPersonnelType]) IS NOT NULL OR NULLIF(Target.[ApplicationPersonnelType], Source.[ApplicationPersonnelType]) IS NOT NULL OR 
	NULLIF(Source.[ApplicationPersonnelTypeAbbreviation], Target.[ApplicationPersonnelTypeAbbreviation]) IS NOT NULL OR NULLIF(Target.[ApplicationPersonnelTypeAbbreviation], Source.[ApplicationPersonnelTypeAbbreviation]) IS NOT NULL OR 
	NULLIF(Source.[CoiFlag], Target.[CoiFlag]) IS NOT NULL OR NULLIF(Target.[CoiFlag], Source.[CoiFlag]) IS NOT NULL) THEN
 UPDATE SET
  [ClientId] = Source.[ClientId], 
  [ApplicationPersonnelType] = Source.[ApplicationPersonnelType], 
  [ApplicationPersonnelTypeAbbreviation] = Source.[ApplicationPersonnelTypeAbbreviation], 
  [CoiFlag] = Source.[CoiFlag]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ClientApplicationPersonnelTypeId],[ClientId],[ApplicationPersonnelType],[ApplicationPersonnelTypeAbbreviation],[CoiFlag])
 VALUES(Source.[ClientApplicationPersonnelTypeId],Source.[ClientId],Source.[ApplicationPersonnelType],Source.[ApplicationPersonnelTypeAbbreviation],Source.[CoiFlag])
;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [ClientApplicationPersonnelType]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[ClientApplicationPersonnelType] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [ClientApplicationPersonnelType] OFF
GO
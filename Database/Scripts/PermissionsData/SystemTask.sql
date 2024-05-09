SET IDENTITY_INSERT [SystemTask] ON

MERGE INTO [SystemTask] AS Target
USING (VALUES
  (1,'Manage Users','')
 ,(2,'Manage Personal Information','')
 ,(3,'Manage Security Information','')
 ,(4,'Technical Support','')
 ,(5,'Manage Registration Documents','')
 ,(6,'Manage Summary Statements','')
 ,(7,'Process Summary Statements','')
 ,(8,'Manage Assigned Panels','')
 ,(9,'Manage Program Panels','')
 ,(10,'View Templates','')
 ,(11,'Review Summary Statements','Perform a limited review process on valid summary statements.')
 ,(12,'View Online Scoring','')
 ,(13,'Manage Online Scoring - SRO','')
 ,(14,'Manage Online Scoring - Admin','Allows users to perform administrative functions within the manage application scoring module.')
 ,(15,'Review Applications','')
 ,(16,'Edit Critiques','')
 ,(17,'View Library','Allows users to view allowed documents in the library.')
 ,(18,'Manage Reviewer Recruitment','Allows users to perform administrative functions within the reviewer recruitment module.')
 ,(19,'Check into any phase','Allows users to submit a summary statement into any phase')
 ,(20,'Restricted Manage User Accounts','Allows SRO''s to manage uses accounts of reviewers or potential reviewers')
 ,(21,'Manage Online Scoring - Client Staff','')
 ,(22,'Manage Online Scoring - SRA Staff','')
 ,(23,'Filter Draft Summary Statements','Displays only summary statements from assigned panels')
 ,(25,'Manage Participant Information','Allows users to modify participant information')
 ,(26,'Perform Summary Statement Writing','Adding and improving content to a summary statement')
 ,(27,'Perform Summary Statement Editing','Modifying and clarifying summary statement content')
 ,(28,'Quality Check Summary Statements','Review/manage summary statement quality')
 ,(30,'View Reports','Allows users to run all reports')
 ,(31,'Limited Manage Participant Information','Allows SRO to modify participant information')
 ,(32,'Submit Task Request','Submit task requests for P2RMIS enhancements or deliverables')
 ,(33,'Manage Setup','')
 ,(34,'View Client Reports','Allows user to view a specified set of reports')
 ,(35,'Process Panel','Allows user to add, modify, or delete panels')
 ,(36,'Manage Online Scoring - Elevated Chair','Allows user to view critique (read only) and general note if they are assigned as an elevated chair.')
 ,(38,'Import Data','Allows user to transfer data into system.')
 ,(39,'Manage Library','Allows users to view all documents in the library regardless of participation type.')
 ,(40,'Manage Document','Allows user to manage documents')
 ,(41,'Meeting Management','Allows users to access Meeting Management')
 ,(42,'Manage Panel Application','Add, modify, or delete panel applications')
 ,(43,'Manage Application Reviewer','Add, modify, or delete panel reviewer assignments')
 ,(44,'Manage Referral Mapping','Upload, or delete referral mapping')
 ,(45,'View Reviewers','View reviewer information')
 ,(46,'Manage Fee Schedule','Upload, or delete fee schedules')
 ,(47,'Manage Applications','Get Applications')
 ,(48,'Manage Application Withdraw','Withdraw or update withdraw')
 ,(49,'Manage Personnel','Add, modify, or remove personnel from program/meeting')
 ,(50,'Manage W9 Addresses','Upload W9 Addresses')
 ,(51,'Manage Travel Flights','Manage Travel Flights')
 ,(52,'View CR Reports','Allow PM, SRM, CRA to run CR REPORTS') 
 ,(53,'Generate Data','Allows user to transfer data out of system.')
 ,(54,'Consumer Management','Allows users to access Consumer Management.')
 ,(55,'Manage Assigned Panels - Chair','Provides limited access to panel management functions in order to chair a panel')
 ,(56,'Manage Library Any Program','Allows users to view all documents in the library without the need to be assigned to a corresponding panel.')
 ,(57,'Modify Contract Documents','Allows a user to modify a panel reviewers contract/pma.')
 ,(200,'Manage Security Policy','Allows a user to define and modify user access and network security policy.')
 ,(201,'View Security Information','Allows a user to view user access and network security policy information.')
) AS Source ([SystemTaskId],[TaskName],[TaskDescription])
ON (Target.[SystemTaskId] = Source.[SystemTaskId])
WHEN MATCHED AND (
	NULLIF(Source.[TaskName], Target.[TaskName]) IS NOT NULL OR NULLIF(Target.[TaskName], Source.[TaskName]) IS NOT NULL OR 
	NULLIF(Source.[TaskDescription], Target.[TaskDescription]) IS NOT NULL OR NULLIF(Target.[TaskDescription], Source.[TaskDescription]) IS NOT NULL) THEN
 UPDATE SET
  [TaskName] = Source.[TaskName], 
 [TaskDescription] = Source.[TaskDescription]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([SystemTaskId],[TaskName],[TaskDescription])
 VALUES(Source.[SystemTaskId],Source.[TaskName],Source.[TaskDescription])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [SystemTask]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[SystemTask] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [SystemTask] OFF
GO
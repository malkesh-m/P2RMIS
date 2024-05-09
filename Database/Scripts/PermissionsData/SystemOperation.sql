SET IDENTITY_INSERT [SystemOperation] ON

MERGE INTO [SystemOperation] AS Target
USING (VALUES
  (1,'Create New User','User controller -> Create new user: DEPRECATED REMOVE ASAP')
 ,(2,'Edit Existing User','User controller -> Edit user: DEPRECATED REMOVE ASAP')
 ,(3,'User Administration','User controller -> User administration: DEPRECATED REMOVE ASAP')
 ,(4,'Set Personal Information','User controller -> Personal Information: DEPRECATED REMOVE ASAP')
 ,(5,'Set Security Information','User controller -> Security settings: DEPRECATED REMOVE ASAP')
 ,(6,'View Application Scoring Summary','ApplicationDetailsController: DEPRECATED REMOVE ASAP')
 ,(7,'Search Open Programs','Search Controller -> Deprecated remove ASAP')
 ,(8,'Search Assigned Programs','Search Controller - Not used, remove ASAP')
 ,(9,'Access General Note','Allows user to access general notes')
 ,(10,'Access Discussion Note','Allows user to access discussion notes')
 ,(11,'Access Admin Note','Allows user to access admin notes')
 ,(12,'View All Clients','')
 ,(13,'View and Modify Registration Documents','ProgramRegistrationStatusController - Allows user to view and modify registration documents for a panel.')
 ,(14,'View Reports','Everything in ReportController, ReportViewer.')
 ,(15,'Manage Summary Statement','SummaryStatementController - Allows user to access functionality within the Manage Summary Statements module.')
 ,(16,'Process Summary Statement','SummaryStatementController - Allows user to access functionality within the Process Summary Statements module.')
 ,(17,'Manage Panel','PanelManagementController - Allows general access to panel management')
 ,(18,'Modify Panel Reopen Dates','Panel Management Controller - Allows user to set re-open dates for panel.')
 ,(19,'View Template','TemplatesController - Provides access to internal html templates (webmaster only).')
 ,(20,'Manage User Accounts','UserProfileManagementController - The users with this permission can manage accounts of other users.')
 ,(21,'Review Summary Statement','SummaryStatementController - Allows user to review summary statements marked for review.')
 ,(22,'Accept Summary Statement Track Changes','SummaryStatementController - The users with this permission can accept or reject track changes of Summary Statements.')
 ,(23,'Score Application','MyWorkspaceController - Provides access to score applications or provide critiques (does not include access to MyWorkspace navigational tabs See-Access Application Scoring).')
 ,(24,'Access My Workspace','MyWorkspaceController - Provides top level access to the my workspace module.')
 ,(25,'View Online Scoring Assigned Panels','ManageApplicationScoringController - Restricts users to being able to access only panels they have been assigned to in Manage Application Scoring.')
 ,(26,'View Online Scoring All Panels','ManageApplicationScoringController - Allows users to being able to access all open panels in Manage Application Scoring.')
 ,(27,'View Scoreboard','ManageApplicationScoringController - Allows user to view the scoreboard.')
 ,(28,'Edit Score Status','ManageApplicationScoringController - Allows user to edit the score status of an application (e.g. Triaged, Disapproved)')
 ,(29,'Edit Score Card','ManageApplicationScoringController - Allows user to edit a final score card.')
 ,(30,'Edit Assignment Type','ManageApplicationScoringController - Allows user to edit a reviewer''s assignment type to an application (e.g. mark COI or abstain).')
 ,(31,'Access Library','Standard access to the library section')
 ,(32,'Manage Work List','Reviewer Recruitment - Allows user to view and manage the worklist.')
 ,(33,'Check into any phase','SummaryStatementController - Allows user to submit summary statement into any phase')
 ,(34,'Restricted Manage User Accounts','Restricts SRO''s access to reviewers and potential reviewers profiles that are on the SRP''s panels')
 ,(35,'View critique','View a reviewers critique without making modifications')
 ,(36,'View Program Reports','View reports at the program scope.')
 ,(37,'View Panel Reports','View reports for an assigned panel.')
 ,(38,'View Scientific Admin Reports','View reports related to scientific administration of a panel.')
 ,(39,'Display Assigned Panels','Displays only summary statements from assigned panels')
 ,(41,'Modify participant information post panel assignment','Modify ParticipantType, ParticipantMethod, Participant Level after reviewer has been assigned to panel')
 ,(42,'Check out SS in writing phase','Allows user to check out summary statements from the writing phase.')
 ,(43,'Check out SS in editing phase','Allows user to check out summary statements in editing phases')
 ,(44,'Check out SS in assurance phase','Allows user to check out summary statements in phases intended to assure quality')
 ,(46,'Modify limited participant information post panel assignment','Modify ParticipantType, ParticipantMethod, Participant Level after reviewer has been assigned to pane for SROl')
 ,(47,'Submit Task','TaskTrackingController - Allows user to submit tasks to P2RMIS IT')
 ,(48,'Manage Setup','Allow general access to system setup')
 ,(49,'View Client Reports','Allows user to view a restricted set of reports')
 ,(51,'Access Unassigned Reviewer Note','Allows user to access unassigned reviewer notes')
 ,(52,'Process Panel','Allows user to add, modify, or delete panels') --Reviewers page
 ,(53,'Process Staffs','Allows user to add, modify, or delete staffs') --Reviewers page
 ,(54,'Import Application Data','Allows user to import application data to the system.')
 ,(55,'Generate Deliverables','Allows user to generate deliverable data for export to external systems.')
 ,(56,'Access Full Library','Allows users to view allowed documents in the library.')
 ,(57,'Manage Document','Allows use to manage documents.')
 ,(58,'Meeting Management', 'Allows user to access Meeting Management')
 ,(59,'Manage Panel Application','Add, modify, or delete panel applications')
 ,(60,'Manage Application Reviewer','Add, modify, or delete panel reviewer assignments')--Reviewers page
 ,(61,'Manage Referral Mapping','Upload, or delete referral mapping')
 ,(62,'View Reviewers','View reviewer information')
 ,(63,'Manage Fee Schedule','Upload, or delete fee schedules')
 ,(64,'Manage Applications','Get Applications')
 ,(65,'Manage Application Withdraw','Withdraw or update withdraw')
 ,(67,'Manage W9 Addresses','Upload W9 Addresses')
 ,(68,'Manage Travel Flights','Manage Travel Flights')
 ,(69,'View CR Reports','Only PM, SRM, CRA can view CR reports')
 ,(70,'Consumer Management','Allows users to access Consumer Management.')
 ,(71,'Send Panel Communication','Send panel emails to reviewers through Panel Managemenet.')
 ,(72,'Manage Application Reviewer Assignment and Expertise','Add, modify or remove a reviewers expertise raitng or assingment to an application.')
 ,(73,'View/Edit Application Reviewer Assignment and Expertise','View reviewers expertise levels and assignments to applications, and modify reviewers expertise/assignments.')
 ,(74,'Manage Panel Critiques','Submit, reset to edit, or edit critiques through panel management.')
 ,(75,'View Panel Critiques','View a panels critiques through panel management.')
 ,(76,'Manage Order of Discussion','Manage a panels discussion order through panel management.')
 ,(77,'Evaluate Reviewers','Provide ratings and comments regarding a panel reviewers performance through panel management.')
 ,(78,'View Panel Reviewers','View reviewers who are assigned to a particular panel.')
 ,(79,'Manage Unassigned Panel', 'Allows user to access panel management features to panels which they are not assigned.')
 ,(80,'Request Panel Application Transfer','Allows users to request transferring an application to a new panel.')
 ,(81,'Manage Consumers','Add, modify, or search for consumers')
 ,(82,'Access Library Any Program','Access the library section for any program without requiring an associated panel assignment.')
 ,(83,'Modify Contract Documents','Allows a user to modify a panel reviewers contract/pma.')
 ,(200,'Security Policy Management','Allows a user to manage security policy.')
 ,(201,'User Security Management','Allows a user to manage user security policy.')
 ,(202,'View Security Information','Allows a user to view security information.')
) AS Source ([SystemOperationId],[OperationName],[OperationDescription])
ON (Target.[SystemOperationId] = Source.[SystemOperationId])
WHEN MATCHED AND (
	NULLIF(Source.[OperationName], Target.[OperationName]) IS NOT NULL OR NULLIF(Target.[OperationName], Source.[OperationName]) IS NOT NULL OR 
	NULLIF(Source.[OperationDescription], Target.[OperationDescription]) IS NOT NULL OR NULLIF(Target.[OperationDescription], Source.[OperationDescription]) IS NOT NULL) THEN
 UPDATE SET
  [OperationName] = Source.[OperationName], 
 [OperationDescription] = Source.[OperationDescription]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([SystemOperationId],[OperationName],[OperationDescription])
 VALUES(Source.[SystemOperationId],Source.[OperationName],Source.[OperationDescription])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [SystemOperation]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[SystemOperation] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [SystemOperation] OFF
GO
SET IDENTITY_INSERT [SystemTemplateVersion] ON

MERGE INTO [SystemTemplateVersion] AS Target
USING (VALUES
  (1,1,'P2RMIS Username','Sending a UID to a new User',1,'Dear {^to-prefix^} {^to-first-name^} {^to-last-name^}:<br />

	<p>You have been granted access to P2RMIS. The P2RMIS Web site can be found at {^hostname^}.</p>

	<p>Your login username is {^UserLogin^}. For the security of your account your temporary password will be sent in a separate email.</p>

	<p>If you have questions regarding this email or your P2RMIS account, please contact our helpdesk at <a href="mailto:help@p2rmis.com">help@p2rmis.com</a>.</p>

	<p>Thank you.</p>',2,NULL,NULL,NULL,NULL,NULL,NULL)
 ,(2,2,'P2RMIS Password','Sending p/w to a new User',1,'<p>
		Dear {^to-prefix^} {^to-first-name^} {^to-last-name^}:<br />
		&nbsp;</p>
	<p>Your temporary password to access P2RMIS is {^temporary-password^}</p>

	<p>Please log in at {^hostname^}.</p>

	<p>Upon logging in, you will be prompted to change this password and set security questions and answers.</p>
	',2,NULL,NULL,NULL,NULL,NULL,NULL)
 ,(3,3,'P2RMIS Username','User account reset - Sending a UID',1,'<p>Dear {^to-prefix^} {^to-first-name^} {^to-last-name^}:</p>

	<p>As requested, you may reactivate your account by logging into the P2RMIS website and using the information below. Upon logging in, you will be required to change your password and select three security questions and answers.</p>

	<p>P2RMIS Web site:  <a href="{^hostname^}">{^hostname^}</a></p>

	<p>Your User ID is:  {^UserLogin^}</p>

	<p>Password: A temporary password will be sent in a separate email.</p>

	<p>Upon logging into P2RMIS, you will be required to set a new password.</p>

	<p>If you experience any difficulty accessing your P2RMIS account, please contact the Help Desk at <a href="mailto:help@p2rmis.com">help@p2rmis.com</a>.</p>

	<p>Thank you.</p>',2,NULL,NULL,NULL,NULL,NULL,NULL)
 ,(4,4,'P2RMIS Password','User account reset - Sending a PW',1,'<p>Dear {^to-prefix^} {^to-first-name^} {^to-last-name^}:</p>

	<p>P2RMIS Web site:  <a href="{^hostname^}">{^hostname^}</a></p>

	<p>Your temporary password is {^temporary-password^}</p>

	<p>Your temporary password will expire in five days.</p>

	<p>If you experience any difficulty accessing your P2RMIS account, please contact the Help Desk at <a href="mailto:help@p2rmis.com">help@p2rmis.com</a>.</p>',2,NULL,NULL,NULL,NULL,NULL,NULL)
 ,(5,5,'P2RMIS Application Transfer Request','Sent to helpdesk to request an application be transferred to a different panel',1,'<p>{0} has requested to transfer <span style="font-weight: bold">{1}</span> from <span style="font-weight: bold">{2}</span> to <span style="font-weight: bold">{3}</span> in <span style="font-weight: bold">{4}</span></p>Reason for transferring:</br><span style="font-weight: bold">{5}</span><p>Comments for requesting transfer:</br><span style="font-weight: bold">{6}</span></p><p>Please go to <span style="font-weight: bold">{7}</span> to take the respective action.</p>Thank you.',2,NULL,NULL,NULL,NULL,NULL,NULL)
 ,(6,6,'Request Transferring Reviewer {0} from {1} to {2}','Sent to helpdesk to request a reviewer be transferred to a different panel',1,'<p>SRO {0} has requested to transfer reviewer <span style="font-weight: bold">{1}</span> from current panel <span style="font-weight: bold">{2}</span> to <span style="font-weight: bold">{3}</span> in <span style="font-weight: bold">{4} {5}</span></p><p>Comments for requesting transfer:</br><span style="font-weight: bold">{6}</span></p><p>Please go to this panel to take corresponding action.</p>Thanks.',2,NULL,NULL,NULL,NULL,NULL,NULL)
 ,(7,7,'P2RMIS Access for {0} {1}','Sent to a User to inform them of assignment to a panel as a reviewer',1,'TO: {^to-last-name^}, {^to-first-name^}<br />
 FROM: {^CompanyName^} <br /> Phone: {^CompanyPhone^} <br /> 
 <p>Thank you for agreeing to participate as a {^ParticipantType^} for the {^FY^} {^ProgramName^} on the {^panel-name^} ({^panel-abbrev^}) panel.</p>To access SRA''s Program and Peer Review Management Information System (P2RMIS), please follow the URL below.</p>  <p>URL: {^hostname^}</p> <p>User ID: {^UserLogin^}</p>  <p>If this is your first time using P2RMIS, your password will be sent to you in a separate email.<p/> <p>If you do not remember your password, please use the “Forgot Password?” link on the P2RMIS login page. You will be instructed to enter your email address and answer your security question. Your user ID and a temporary password will be sent in two separate emails. Once you have logged into P2RMIS, you will be asked to change the temporary password to a new personal password.</p> <p>Please complete the registration, which will allow you to access to your P2RMIS workspace.</p>  <p>This is an automatically generated email. Please do not reply.</p>',2,NULL,NULL,NULL,NULL,NULL,NULL)
 ,(8,8,'P2RMIS  Comment Added for {0} {1} {2} {3}','Sent to participants of a discussion board when a new comment is added.',1,'Dear {0} {1} {2},<br />                                          
<p>This email is to inform you that a comment has been entered by {3} on {4} regarding application {5} -{6} on panel {7} {8} {9}.  To view this comment, please log into P2RMIS (<a href="{10}">{10}</a>), select My Workspace, select the panel, and click the "Discussion Board" icon in the Action column for this application.  The discussion will end on {11}.</p>                                             
<p>Thank you.</p><br />
<p>P2RMIS Technical Support</p>
<p><a href="mailto:{12}">{12}</a></p>
<p>{13}</p>',2,NULL,NULL,NULL,NULL,NULL,NULL)
 ,(12,10,'Request Releasing Reviewer {0} from {1}','Sent to helpdesk to request a reviewer be released from a different panel.',1,'<p>{5} {0} has requested to remove reviewer <span style="font-weight: bold">{1}</span> from current panel <span style="font-weight: bold">{2}</span> in <span style="font-weight: bold">{3}</span></p><p>Comments for requesting removal:</br><span style="font-weight: bold">{4}</span></p><p>Please go to this panel to take the corresponding action.</p>Thank you.',2,NULL,NULL,NULL,NULL,NULL,NULL)
 ,(13,11,'P2RMIS Critique Reset','Sent to reviewers to notify their critique has been reset back to edit',1,'<p>TO: {0}, {1} {2}, {3}, {4}</p><p>FROM: SRA International, Inc., a CSRA Company<br />110 Thomas Johnson Drive<br />Suite 200<br />Frederick, MD 21702<br />Phone: 1-866-330-9752</p><p>Your Scientific Review Officer has made the following critique available for modifications:<br /><br />{5}</p><p>If you have any questions, please contact your Scientific Review Officer at {6}</p>',2,NULL,NULL,NULL,NULL,NULL,NULL)
 ,(1015,12,'P2RMIS Ticket {0} has been created','Sent to users after submitting a ticket.',1,'<p>Your request has been received and ticket {0} has been created.</p>
<p>You can check on the status of your ticket, as well as add additional comments or attachments by visiting the following URL (requires logon):</p>
<p><a href="{1}">{1}</a></p>                                             
<p>Thank you.</p><br />
<p>P2RMIS IT Team</p>',2,NULL,NULL,NULL,NULL,NULL,NULL)
,(1017,1012,'P2RMIS Online Discussion started for {0} {1} {2} {3}', 'Sent to participants of a discussion board when an online discussion is started.',1,'Dear {0} {1} {2},<br />                                          
<p>This email is to inform you that an online discussion has been started for application {5} -{6} on panel {7} {8} {9}.  To view the discussion, please log into P2RMIS (<a href="{10}">{10}</a>), select My Workspace, select the panel, and click the "Discussion Board" icon in the Action column for this application.  The discussion will end on {11}.</p>                                             
<p>Thank you.</p><br />
<p>P2RMIS Technical Support</p>
<p><a href="mailto:{12}">{12}</a></p>
<p>{13}</p>',2,NULL,NULL,NULL,NULL,NULL,NULL)
,(1018,1013,'P2RMIS Password Successfully Changed', 'Sent to user after changing password.',1,
'<p>Dear {^to-prefix^} {^to-first-name^} {^to-last-name^}:</p> </br>                                          
<p>Your new P<span style="vertical-align: super;">2</span>RMIS password has been changed successfully on {^current-date-time^}. Your new password will expire on {^password-expiration-date^}.</p>  
<p>If this change was intended, no further action is needed. If you did not request this change or if you are unable to access your P<span style="vertical-align: super;">2</span>RMIS account, please contact the Help Desk at <a href="mailto:{^help-desk-email^}">{^help-desk-email^}</a> or {^helpdesk-phone-number^} {^helpdesk-hours^}.</p>
<br/></br/>
<p>Thank you.</p>
<p>P<span style="vertical-align: super;">2</span>RMIS Help Desk</p>
',2,NULL,NULL,NULL,NULL,NULL,NULL)
) AS Source ([VersionId],[TemplateId],[Subject],[Description],[VersionNumber],[Body],[TemplateStageID],[SentBy],[SentDate],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
ON (Target.[VersionId] = Source.[VersionId])
WHEN MATCHED AND (
	NULLIF(Source.[TemplateId], Target.[TemplateId]) IS NOT NULL OR NULLIF(Target.[TemplateId], Source.[TemplateId]) IS NOT NULL OR 
	NULLIF(Source.[Subject], Target.[Subject]) IS NOT NULL OR NULLIF(Target.[Subject], Source.[Subject]) IS NOT NULL OR 
	NULLIF(Source.[Description], Target.[Description]) IS NOT NULL OR NULLIF(Target.[Description], Source.[Description]) IS NOT NULL OR 
	NULLIF(Source.[VersionNumber], Target.[VersionNumber]) IS NOT NULL OR NULLIF(Target.[VersionNumber], Source.[VersionNumber]) IS NOT NULL OR 
	NULLIF(Source.[Body], Target.[Body]) IS NOT NULL OR NULLIF(Target.[Body], Source.[Body]) IS NOT NULL OR 
	NULLIF(Source.[TemplateStageID], Target.[TemplateStageID]) IS NOT NULL OR NULLIF(Target.[TemplateStageID], Source.[TemplateStageID]) IS NOT NULL OR 
	NULLIF(Source.[SentBy], Target.[SentBy]) IS NOT NULL OR NULLIF(Target.[SentBy], Source.[SentBy]) IS NOT NULL OR 
	NULLIF(Source.[SentDate], Target.[SentDate]) IS NOT NULL OR NULLIF(Target.[SentDate], Source.[SentDate]) IS NOT NULL OR 
	NULLIF(Source.[CreatedBy], Target.[CreatedBy]) IS NOT NULL OR NULLIF(Target.[CreatedBy], Source.[CreatedBy]) IS NOT NULL OR 
	NULLIF(Source.[CreatedDate], Target.[CreatedDate]) IS NOT NULL OR NULLIF(Target.[CreatedDate], Source.[CreatedDate]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedBy], Target.[ModifiedBy]) IS NOT NULL OR NULLIF(Target.[ModifiedBy], Source.[ModifiedBy]) IS NOT NULL OR 
	NULLIF(Source.[ModifiedDate], Target.[ModifiedDate]) IS NOT NULL OR NULLIF(Target.[ModifiedDate], Source.[ModifiedDate]) IS NOT NULL) THEN
 UPDATE SET
  [TemplateId] = Source.[TemplateId], 
  [Subject] = Source.[Subject], 
  [Description] = Source.[Description], 
  [VersionNumber] = Source.[VersionNumber], 
  [Body] = Source.[Body], 
  [TemplateStageID] = Source.[TemplateStageID], 
  [SentBy] = Source.[SentBy], 
  [SentDate] = Source.[SentDate], 
  [CreatedBy] = Source.[CreatedBy], 
  [CreatedDate] = Source.[CreatedDate], 
  [ModifiedBy] = Source.[ModifiedBy], 
  [ModifiedDate] = Source.[ModifiedDate]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([VersionId],[TemplateId],[Subject],[Description],[VersionNumber],[Body],[TemplateStageID],[SentBy],[SentDate],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
 VALUES(Source.[VersionId],Source.[TemplateId],Source.[Subject],Source.[Description],Source.[VersionNumber],Source.[Body],Source.[TemplateStageID],Source.[SentBy],Source.[SentDate],Source.[CreatedBy],Source.[CreatedDate],Source.[ModifiedBy],Source.[ModifiedDate])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [SystemTemplateVersion]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[SystemTemplateVersion] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [SystemTemplateVersion] OFF
GO
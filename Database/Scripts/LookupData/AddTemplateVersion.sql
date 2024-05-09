INSERT INTO [dbo].[SystemTemplateVersion]
           ([TemplateId]
           ,[Subject]
           ,[Description]
           ,[VersionNumber]
           ,[Body]
           ,[TemplateStageID])
     VALUES
           (1012,'P2RMIS Online Discussion started for {0} {1} {2} {3}', 'Sent to participants of a discussion board when an online discussion is started.',1,'Dear {0} {1} {2},<br />                                          
<p>This email is to inform you that an online discussion has been started for application {5} -{6} on panel {7} {8} {9}.  To view the discussion, please log into P2RMIS (<a href="{10}">{10}</a>), select My Workspace, select the panel, and click the "Discussion Board" icon in the Action column for this application.  The discussion will end on {11}.</p>                                             
<p>Thank you.</p><br />
<p>P2RMIS Technical Support</p>
<p><a href="mailto:{12}">{12}</a></p>
<p>{13}</p>',2)
GO



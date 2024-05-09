/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/ 

/*
	Data Update scripts that must run prior to deployment for data fixes, etc.
*/
 EXEC sp_msforeachtable 'ALTER TABLE ? DISABLE TRIGGER all'
 GO

IF EXISTS (SELECT  'X'
FROM         dbo.PanelApplicationReviewerExpertise
GROUP BY PanelApplicationId, PanelUserAssignmentId, DeletedDate
HAVING      (COUNT(*) > 1))
BEGIN
PRINT 'Cleaning up duplicate expertise';
:r ./SchemaDataUpdate/RemoveDuplicateExpertise.sql
END

IF COL_LENGTH('dbo.UserApplicationComment', 'ApplicationID') IS NOT NULL
BEGIN
PRINT 'Cleaning up UserApplicationComment bad data';
:r ./SchemaDataUpdate/UserApplicationCommentApplicationIdRemoval.sql
END
GO
 EXEC sp_msforeachtable 'ALTER TABLE ? ENABLE TRIGGER all'
 GO
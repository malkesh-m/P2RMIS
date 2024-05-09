CREATE VIEW [dbo].ViewApplicationTemplateElement AS
SELECT [ApplicationTemplateElementId]
      ,[ApplicationTemplateId]
      ,[MechanismTemplateElementId]
      ,[PanelApplicationReviewerAssignmentId]
      ,[DiscussionNoteFlag]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationTemplateElement]
WHERE [DeletedFlag] = 0


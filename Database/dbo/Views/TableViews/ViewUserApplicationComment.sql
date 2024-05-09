CREATE VIEW [dbo].ViewUserApplicationComment AS
SELECT [UserApplicationCommentID]
      ,[UserID]
      ,[PanelApplicationId]
      ,[Comments]
      ,[CommentTypeID]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserApplicationComment]
WHERE [DeletedFlag] = 0


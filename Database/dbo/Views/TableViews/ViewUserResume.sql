CREATE VIEW [dbo].ViewUserResume AS
SELECT [UserResumeId]
      ,[UserInfoId]
      ,[LegacyCvId]
      ,[DocType]
      ,[FileName]
      ,[Version]
      ,[ReceivedDate]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserResume]
WHERE [DeletedFlag] = 0


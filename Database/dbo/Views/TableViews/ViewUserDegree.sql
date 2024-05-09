CREATE VIEW [dbo].ViewUserDegree AS
SELECT [UserDegreeId]
      ,[UserInfoId]
      ,[DegreeId]
      ,[LegacyDegreeId]
      ,[Major]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserDegree]
WHERE [DeletedFlag] = 0




CREATE VIEW [dbo].[ViewUserInfo] AS
SELECT [UserInfoID]
      ,[UserID]
      ,[MilitaryRankId]
      ,[MilitaryStatusTypeId]
      ,[FirstName]
      ,[MiddleName]
      ,[LastName]
      ,[NickName]
      ,[VendorID]
	  ,[VendorName]
      ,[Institution]
      ,[Department]
      ,[Position]
      ,[BadgeName]
      ,[PrefixId]
      ,[SuffixText]
      ,[GenderId]
      ,[EthnicityId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
	  ,[ProfessionalAffiliationId]
  FROM [dbo].[UserInfo]
WHERE [DeletedFlag] = 0

GO


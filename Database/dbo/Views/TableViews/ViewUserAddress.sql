CREATE VIEW [dbo].ViewUserAddress AS
SELECT [AddressID]
      ,[UserInfoID]
      ,[LegacyAddressID]
      ,[AddressTypeId]
      ,[PrimaryFlag]
      ,[Address1]
      ,[Address2]
      ,[Address3]
      ,[Address4]
      ,[City]
      ,[StateId]
      ,[StateOther]
      ,[Zip]
      ,[CountryId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserAddress]
WHERE [DeletedFlag] = 0


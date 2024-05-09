INSERT INTO [dbo].[UserVendor]
           ([UserInfoId]
           ,[VendorId]
           ,[VendorName]
           ,[ActiveFlag]
           ,[VendorTypeId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT UserInfoId, VendorId, ISNULL(VendorName, ''), 1, 1, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
FROM ViewUserInfo
WHERE VendorId IS NOT NULL
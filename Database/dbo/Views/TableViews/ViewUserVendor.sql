CREATE VIEW [dbo].[ViewUserVendor]
	AS SELECT [UserVendorId], 
	[UserInfoId],
    [VendorId], 
    [VendorName], 
    [ActiveFlag], 
    [VendorTypeId],
	[CreatedBy],
	[CreatedDate],
	[ModifiedBy],
	[ModifiedDate]  
FROM [UserVendor]
WHERE DeletedFlag = 0

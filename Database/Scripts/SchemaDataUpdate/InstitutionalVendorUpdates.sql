UPDATE UserVendor SET VendorTypeId = CASE PPL_Addresses.Inst_Vendor_ID WHEN 0 THEN 1 ELSE 2 END 
FROM [$(DatabaseName)].[dbo].ViewUserInfo UserInfo INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUser] [User] ON UserInfo.UserID = [User].UserID INNER JOIN
	[$(DatabaseName)].[dbo].UserVendor UserVendor ON UserInfo.UserInfoId = UserVendor.UserInfoId INNER JOIN
	[$(P2RMIS)].dbo.PPL_Addresses PPL_Addresses ON [User].PersonID = PPL_Addresses.Person_ID
WHERE PPL_Addresses.Inst_Vendor_Id = 1 AND PPL_Addresses.Address_Type = 'W9 Address' AND UserVendor.ActiveFlag = 1 AND UserVendor.DeletedFlag = 0